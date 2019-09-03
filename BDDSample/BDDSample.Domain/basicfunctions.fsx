open System

type Customer = {
    Id : int
    Name : string
    IsVip : bool
}

// Let binding bind a name to a value or function
let name = "Ian"

let customer = { Id = 1; Name = "Ian"; IsVip = false }

let isVip = (fun c -> c.IsVip)

let toString customer =
    sprintf "%i: %s (%A)" customer.Id customer.Name customer.IsVip

let output = toString customer

type ConvertToString = int -> string

type ConvertToBool = string -> bool

let convertToString (input:int) : string =
    string input

let convertToBool (input:string) : bool =
    String.IsNullOrWhiteSpace input

// Void? Unit
type NoArguments = unit -> int

type NoOutput = int -> unit