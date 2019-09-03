open System

// int -> bool
let isOdd input =
    input % 2 <> 0

let now () =
    DateTime.Now

let log (msg:string) =
    ()

let now' = now

now ()

// Types
type Address = {
    FirstLine : string
    City : string
    Postcode : string
}

type CustomerName = string

type Email = Email of string

// Customer (Name, Address, Email)
type Customer = CustomerName * Address * Email

type AddressType =
    | Commercial of Address
    | NonCommercial of Address
    | Unknown of Name:string