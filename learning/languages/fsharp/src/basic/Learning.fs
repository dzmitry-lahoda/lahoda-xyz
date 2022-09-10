module learning

(*open System.Drawing
let remap (r1 : Rectangle) (r2 : Rectangle) =
  let scalex = float r2.Width / float r1.Width
  let scaley = float r2.Height / float r1.Height
  let mapx x = int (float r2.Left + truncate( float (x - r1.Left) * scalex))
  let mapy y = int (float r2.Top + truncate( float (y - r1.Top) * scaley))
  let mapp (p:Point) = new Point(mapx p.X, mapy p.Y)
  mapp
 
let mapp = remap (Rectangle(100,100,100,100)) (Rectangle(50,50,200,200));;
 
open System.IO
open System.Net
let http(url: string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html
 
let delimiters = [ ' '; '\n'; '\t'; '<'; '>'; '=' ]
let getWords s = String.split delimiters s
let getStats site =
    let url = "http://" + site
    let html = http url
    let hwords = getWords html;
    let hrefs = html |> getWords |> List.filter (fun s -> s = "href")
    (site,html.Length, hwords.Length, hrefs.Length)
    
    
let shift (dx,dy) (x,y) = (x+dx, y+dy)
let shiftLeft = shift (0,0)
 
 
 
let qweqwe = [ (1, None);
                 (2 , None);
                 (3, Some(1,2));
                 (4, Some(2,1)) ]
                 
let qwe (name,parents) =
    match name with
    | Some(m,p) -> printfn "Ia %d" m 
    | None -> printfn "Sirota %d" 1
 
let asdasd = [ (1, None);
                 (2 , None);
                 (3, Some(1,2));
                 (4, Some(2,1)) ]
                 
let asd (name,parents) =
    match parents with
    | Some(m,p) -> printfn "Ia %d pap %d mam %d" name m p
    | None -> printfn "Sirota %d" name
    
let (gaga,haha) = asdasd.[2]
let iss = Option.is_some haha
 
let ikiki zero =
    let pipa =1/zero
    (pipa)
 
let fetr zero =
    try Some(ikiki zero)
    with :? System.DivideByZeroException -> None
match (fetr 1) with
    | Some(n) -> Console.WriteLine n
    | None -> printfn "HERNAI"
let round (x) =
    match x with 
    |_ when x > 100 -> 1
    |_ when x < 1 -> -1
    |_ -> 123
    
let rec faca (n: bigint) =
    if n=1I then 1I
    else n * faca (n-1I)
    
let sites = [ "http://www.live.com";
                "http://www.google.com" ];;
let http url = "<html>"+url+"</http>" 
let fetch url = (url, http url);;
 
let qqqqq = ("haha", "hihi")
let aaaaa (_,p) = 
    match p with
    |_ when p="haha" -> printfn "haha"
    |_ when p="hihi" -> printfn "hihi"
    |_ -> unit
 
 
@"E:\BSU\RFE\"
 
module ASD =      Microsoft.FSharp.Core.Operators.Checked
type SSS = System.String
 
let save = get_ends ( fst get_directories , snd get_directories)
 
let zaza (a , b) =
    let c = 1;
    c
 
open System.Windows; 
let create =  
    let application = new Application();
    let window = new Window();
    application.Run(window);
 
open System.Windows.Forms;
 
let form = new Form();
form.Show()
let textB = new RichTextBox(Dock=DockStyle.Fill, Text="Here is some initial text")
form.Controls.Add(textB)
 
open System.IO
open System.Net
let http(url: string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html
 
let delimiters = [ ' '; '\n'; '\t'; '<'; '>'; '=' ]
let getWords s = String.split delimiters s
let getStats site =
    let url = "http://" + site
    let html = http url
    let hwords = html |> getWords
    let hrefs = html |> getWords |> List.filter (fun s -> s = "href")
    (site,html.Length, hwords.Length, hrefs.Length)
    
let google = http("http://www.google.com")
textB.Text <- http("http://news.bbc.co.uk")
open Microsoft.FSharp.Core.Operators.Checked
let hah a b =
    let maz = byte 23
    if a=b then printfn "1"
    else if a<>b then printfn "2"
    
    
let haha prima =
    match prima with
    | h :: t -> printfn "III %c" h
    | [] -> printfn "emptia" 
 *)
