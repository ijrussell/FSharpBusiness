#load "Cache.fs"

let cache = Caching.SimpleCache()

let read id = cache.Get id |> Async.RunSynchronously
    
let item1 = read 1
let item2 = read 1
cache.Clear
let item3 = read 1
let item4 = read 1
let item5 = read 10