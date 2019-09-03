namespace BddSample

module CustomerTests =

    open NUnit.Framework
    open FsUnit
    open Swensen.Unquote
    open BddSample.CustomerTypes
    open BddSample.CustomerFunctions

        [<TestFixture>]
        module ``Eligible Registered Customer`` =
            let eligibleCustomer = EligibleRegisteredCustomer { CustomerId = CustomerId "John" }

            [<Test>]
            let ``who spends at least 100 should receive a discount`` () =
                let spend = 100M
                let total = spend |> calculateTotal eligibleCustomer
                total |> should equal 90M

            [<Test>]
            let ``who spends under 100 should not receive a discount`` () =
                let spend = 99M
                let total = spend |> calculateTotal eligibleCustomer 
                total |> should equal spend
    
        [<TestFixture>]
        module ``Ineligible Registered Customer`` =
            let registeredCustomer = RegisteredCustomer { CustomerId = CustomerId "Richard" }
        
            [<Test>]
            let ``who spends at least 100 should not receive a discount`` () =
                let spend = 100M
                let total = spend |> calculateTotal registeredCustomer
                total |> should equal spend

        [<TestFixture>]
        module ``Unregistered Customer`` =
            let guest = Guest { Name = "Sarah" }
        
            [<Test>]
            let ``who spends at least 100 should not receive a discount`` () =
                let spend = 100M
                let total = spend |> calculateTotal guest 
                test <@ total = spend @>

