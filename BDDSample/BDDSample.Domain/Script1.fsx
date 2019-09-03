#load "Customer.Types.fs"
#load "Customer.Functions.fs"

open BddSample.CustomerTypes
open BddSample.CustomerFunctions

let assertEquals expected actual =
    expected = actual

let guest = Guest { Name = "Ian" }
let spend = 100M
assertEquals spend (calculateTotal guest spend)

let eligibleUnderLimit = EligibleRegisteredCustomer { CustomerId = CustomerId "Ian"; }
let spend' = 80M
assertEquals spend (calculateTotal eligibleUnderLimit spend')

let eligibleOverLimit = EligibleRegisteredCustomer { CustomerId = CustomerId "Ian"; }

let total2 = calculateTotal eligibleOverLimit 120M

let ineligible = RegisteredCustomer { CustomerId = CustomerId "Ian"; }

let total3 = calculateTotal ineligible 120M
