// Function composition with Result<'TResult,'TFailure>

open System

// Record type
type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x,f) = Result.bind f x

let result = ResultBuilder()

type UpgradeCustomerException =
    | RemoteConnectionException of Exception

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100M then { customer with IsVip = true }
    else customer

let getPurchases customer =
    try
        // Imagine this calls a remote service
        let purchases =
            if customer.Id % 2 = 0 then (customer, 120M)
            else (customer, 80M)
        Ok purchases
    with
    | ex -> Error (RemoteConnectionException ex)

// Currying
let increaseCredit condition customer =
    if condition customer then { customer with Credit = customer.Credit + 100M }
    else { customer with Credit = customer.Credit + 50M }

// Partial application
let increaseCreditUsingVip = increaseCredit (fun c -> c.IsVip)

let upgradeCustomer customer =
    customer
    |> getPurchases
    |> Result.map tryPromoteToVip
    |> Result.map increaseCreditUsingVip

let (>>-) x f = Result.map f x

let upgradeCustomer1 customer =
    customer
    |> getPurchases
    >>- tryPromoteToVip
    >>- increaseCreditUsingVip


let upgradeCustomer' customer = result {
    let! purchases = getPurchases customer
    let customer' = tryPromoteToVip purchases
    let customer'' = increaseCreditUsingVip customer'
    return customer''
}

let customer = upgradeCustomer' { Id = 1; IsVip = true; Credit = 50M }

// convert a normal function into a two-track function
let map oneTrackFunction twoTrackInput = 
    match twoTrackInput with
    | Ok s -> Ok (oneTrackFunction s)
    | Error f -> Error f

// convert a switch function into a two-track function
let bind switchFunction twoTrackInput = 
    match twoTrackInput with
    | Ok s -> switchFunction s
    | Error f -> Error f

// convert a unit function to a one track function
let tee f x = 
    f x |> ignore
    x

