namespace BddSample

module CustomerFunctions =

    open BddSample.CustomerTypes

    // calculateDiscount = customer -> spend -> discount
    // calculateTotal = customer -> spend -> total

    let calculateTotal : CalculateTotal =
        fun customer spend -> 
            let discount = 
                match customer with
                | EligibleRegisteredCustomer _ when spend >= 100M -> spend * 0.1M
                | _ -> 0.0M
            spend - discount
