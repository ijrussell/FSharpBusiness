// Simple function composition

// Record type
type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

// Tuple - decomposition
let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100M then { customer with IsVip = true }
    else customer

let getPurchases customer =
    if customer.Id % 2 = 0 then (customer, 120M)
    else (customer, 80M)

// Currying
let increaseCredit condition customer =
    if condition customer then { customer with Credit = customer.Credit + 100M }
    else { customer with Credit = customer.Credit + 50M }

// Partial application
let increaseCreditUsingVip = increaseCredit (fun c -> c.IsVip)

// Different styles of composing functions
let upgradeCustomer customer =
    let customerWithPurchases = getPurchases customer
    let promotedCustomer = tryPromoteToVip customerWithPurchases
    increaseCreditUsingVip promotedCustomer

let upgradeCustomerNested customer =
    increaseCreditUsingVip(tryPromoteToVip(getPurchases customer))

let upgradeCustomerPiped customer =
    customer 
    |> getPurchases 
    |> tryPromoteToVip 
    |> increaseCreditUsingVip

let upgradeCustomerComposed =
    getPurchases >> tryPromoteToVip >> increaseCreditUsingVip
