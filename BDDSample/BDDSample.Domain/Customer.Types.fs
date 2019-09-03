namespace BddSample

module CustomerTypes =

    // Algebraic Type System
    // Value Types - Single Case Discriminated Union
    type CustomerId = CustomerId of string
    type Spend = decimal
    type Total = decimal

    // Entities - Record Type [And]
    type RegisteredCustomer = {
        CustomerId : CustomerId
    }

    type UnregisteredCustomer = {
        Name : string
    }  

    // Discriminated Union (Choice Type [Or])
    type Customer =
        | EligibleRegisteredCustomer of RegisteredCustomer
        | RegisteredCustomer of RegisteredCustomer
        | Guest of UnregisteredCustomer

    type CalculateTotal = Customer -> Spend -> Total
