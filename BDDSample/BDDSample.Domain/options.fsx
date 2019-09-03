// Options vs null

//type Option<'T> =
//    | Some of 'T
//    | None

open System

let tryParse (input:string) =
    let (success, result) = DateTime.TryParse input
    if success then Some result
    else None

let isdate = tryParse "2019-08-01"
let notdate = tryParse "Hello"

// Handling null/nullable from .Net with Option module
let isnull:string = null
let isnullable = Nullable<int>()

let isnullopt = Option.ofObj isnull
let isnullableopt = Option.ofNullable isnullable

let isnull' = Option.toObj isnullopt
let isnullable' = Option.toNullable isnullableopt

// Option type
type CustomerName = {
    FirstName : string
    MiddleName : string option
    LastName : string
}

let customer = { FirstName = "Ian"; MiddleName = None; LastName = "Russell" }

let customer' = { customer with MiddleName = Some "John" }