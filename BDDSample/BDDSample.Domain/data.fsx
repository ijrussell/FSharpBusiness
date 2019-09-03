//#r "netstandard"
#r @"C:\Users\Ian\.nuget\packages\fsharp.data\3.1.1\lib\net45\FSharp.Data.dll"

open System
open FSharp.Data

type Csv = CsvProvider<"rec-crime-pfa.csv", ",">

let data = Csv.Load "rec-crime-pfa.csv"

data.Rows
|> Seq.take 10
|> Seq.iter (fun row -> printfn "%A" row)

data.Rows
|> Seq.filter (fun row -> row.Region = "East Midlands")
|> Seq.map (fun row -> (DateTime.Parse row.``12 months ending``), row.Region, row.PFA, row.``Rolling year total number of offences``)
|> Seq.filter (fun (a, _, _, _) -> a.Year = 2015)

