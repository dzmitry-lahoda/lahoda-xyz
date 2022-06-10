module producer.App

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Hosting.Builder
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Prometheus
open Prometheus.HttpMetrics
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.AspNetCore.Http

type Message =
    {
        Text : string
    }

module Views =
    open GiraffeViewEngine

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title []  [ encodedText "producer" ]
                link [ _rel  "stylesheet"
                       _type "text/css"
                       _href "/main.css" ]
            ]
            body [] content
        ]

    let partial () =
        h1 [] [ encodedText "producer" ]

    let index (model : Message) =
        [
            partial()
            p [] [ encodedText model.Text ]
        ] |> layout

let metrics_tests_requests_total = 
  Metrics.CreateCounter(
      "metrics_tests_requests_total", 
      "Increments with each metric requests"
      //CounterConfiguration(LabelNames = [||])
      )

let metrics_tests_requests_AB_total = 
  Metrics.CreateCounter(
      "metrics_tests_requests_AB_total", 
      "Increments with each metric requests",
      CounterConfiguration(LabelNames = [|"A"; "B";|])
      )


let metrics_tests_15_seconds_total = 
  Metrics.CreateCounter(
      "metrics_tests_15_seconds_total", 
      ""
      )

let metrics_tests_decrement = 
    Metrics.CreateGauge(
        "metrics_tests_decrement", 
        ""
        )


let metrics_tests_average_five = 
    Metrics.CreateGauge(
        "metrics_tests_average_five", 
        "Spreads +-2 around 5"
        )

let metrics_tests_average_five_BC = 
  Metrics.CreateGauge(
      "metrics_tests_average_five_BC", 
      "Increments with each metric requests",
      [|"B"; "C"|]
      )

let indexHandler (name : string) =
    let greetings = sprintf "Hello %s, from Giraffe!" name
    let model     = { Text = greetings }
    let view      = Views.index model
    htmlView view

let metric = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            metrics_tests_requests_total.Inc()
            metrics_tests_requests_AB_total.Labels("a", "b").Inc()
            metrics_tests_requests_AB_total.Labels("aa", "bb").Inc()
            
            let now = Environment.TickCount64
            if now  |> float > metrics_tests_15_seconds_total.Value + 15_000.0 then
                metrics_tests_15_seconds_total.IncTo(float now)
            let rnd = Random().Next(3,8)            
            metrics_tests_average_five.Set(float rnd)
            metrics_tests_average_five_BC.Labels("b", "c").Set(float rnd)
            metrics_tests_average_five_BC.Labels("bb", "cc").Set(float rnd)
            
            metrics_tests_decrement.Dec()

            use stream = new System.IO.MemoryStream()
            do! Metrics.DefaultRegistry.CollectAndExportAsTextAsync(stream) 
            stream.Position <- 0L
            use reader = new StreamReader(stream)
            return! text (reader.ReadToEnd()) next ctx
        }
    
        

let webApp =
    choose [
        GET >=>
            choose [
                route "/" >=> indexHandler "world"
                routef "/hello/%s" indexHandler
                route "/metrics" >=> metric
            ]
        setStatusCode 404 >=> text "Not Found" ]

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureCors (builder : CorsPolicyBuilder) =
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyMethod()
           .AllowAnyHeader()
           |> ignore

let configureApp (app : IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IWebHostEnvironment>()
    let isDevelopment (x:string) = x = "Development" || String.IsNullOrWhiteSpace(x)
    (match isDevelopment(env.EnvironmentName) with
    | true  -> app.UseDeveloperExceptionPage()
    | false -> app.UseGiraffeErrorHandler errorHandler)
        .UseCors(configureCors)
        .UseStaticFiles()
        .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    builder.AddFilter(fun l -> l.Equals LogLevel.Error)
           .AddConsole()
           .AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")
    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentRoot)
        .UseWebRoot(webRoot)
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
    0