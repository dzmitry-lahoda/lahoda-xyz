module generating_data

open NodaTime
open FsCheck
open Swensen.Unquote
open System


type Amount = Amount of decimal

[<ReflectedDefinition>]
type Currency = XRP | ETH | BTC

let possibilities = [
    Currency.XRP
    Currency.ETH
    Currency.BTC
    ]

let genn = 
    let rnd = new Random()
    gen { 
        let v = rnd.Next(0,100) |> decimal 
        let result = Amount v
        return result
   }

type Bid = {currency: Currency; amount:Amount; instant:Instant}

let bid currency amount instant = {currency = currency;amount = amount; instant = instant}



let generatorTests() =
  let minTime = NodaTime.Instant.FromUtc(2018,01,01,01,01)
  let maxTime = NodaTime.Instant.FromUtc(2019,01,01,01,01)


  let generateRandomNumber = FsCheck.Gen.choose (minTime.ToUnixTimeSeconds() |> Convert.ToInt32, maxTime.ToUnixTimeSeconds() |> Convert.ToInt32)
  let genMapp = Gen.map (fun x-> Instant.FromUnixTimeSeconds(int64 x))  generateRandomNumber
  let chooseOfElements = Gen.elements [Currency.XRP;Currency.ETH;Currency.BTC]
  let comb a b = sprintf "%A%i" a b 
  let gen2 = Gen.zip generateRandomNumber chooseOfElements
  let gen3 = Gen.map2 comb chooseOfElements generateRandomNumber 
  let random = generateRandomNumber.Sample(1).[0]
  random >=! int32(minTime.ToUnixTimeSeconds())
  random <=! int32(maxTime.ToUnixTimeSeconds())
  let doAll = 
    bid
    <!> chooseOfElements
    <*> genn
    <*> genMapp
  Gen.sample 10 doAll |> Seq.iter (fun x -> printfn "%A" x)

