module Data 

open FSharp.Data

type Csv = CsvProvider<"rec-crime-pfa.csv", ",">

let data = Csv.Load "rec-crime-pfa.csv"

let test = data.Rows
        |> Seq.take 10
        |> Seq.iter (fun row -> printfn "%s" row.Region)
