// Result vs Exceptions

open System


//type Result<'TSuccess,'TFailure> = 
//    | Success of 'TSuccess
//    | Failure of 'TFailure

let tryDivide (x:decimal) (y:decimal) =
    try
        Ok (x/y) 
    with
    | :? DivideByZeroException as ex -> Error ex

let baddivide = tryDivide 1M 0M
let gooddivide = tryDivide 1M 1M

type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x,f) = Result.bind f x

let result = ResultBuilder()

let divide x y = result {
    let! result = tryDivide x y
    return result
}

let baddivide' = divide 1M 0M
let gooddivide' = divide 1M 1M
