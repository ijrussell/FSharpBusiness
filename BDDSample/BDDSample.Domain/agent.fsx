#load "Agent.fs"

let agent = Agent.createThrottler 1000

let tryProcess id = async {
    let! response = agent id
    return response 
}

let run id =
    id |> tryProcess |> Async.RunSynchronously

[1..10] |> List.iter (fun i -> run i |> printfn "%s")