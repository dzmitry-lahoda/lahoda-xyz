
// client
type Token = {IssuedAt : System.DateTime; Value:string}
type BrowserId = {Id :string}
type MobileId = {Id:string}
type BrowserWithToken = {Token : Token; Id : BrowserId}
type Browser = {Id : BrowserId}
type ClientBrowser = Clean  of Browser | Infected of BrowserWithToken
type MobileWithToken = {Token : Token; Id : MobileId}
type Mobile = {Id : MobileId}
type ClientMobile = Clean of Mobile | Infected of MobileWithToken
type ClientDevice = Browser of ClientBrowser | Mobile of ClientMobile
type AnonymousUser = {GeneratedClientId:string}
type LoggedInUser = {MemberId:string; Of: AnonymousUser}
type ClientUser = Anon of  AnonymousUser | Logged of LoggedInUser
type RealUser = ClientUser * ClientDevice
type OptOutFormResults = {Email:string; Id:string}

// server
type ServerDevice = Browser of  BrowserId | Mobile of MobileId
type ServerNode = Token of Token | Device of  ServerDevice

//type Vertex<'a,'b> = 