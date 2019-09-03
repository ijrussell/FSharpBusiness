module Caching

open System
open System.Collections.Generic

type Agent<'T> = MailboxProcessor<'T>

type Source = 
    | Cache
    | Remote

type CacheResponse = {
    Id : int
    Data : string option
    Source : Source
}

type CachingMessage =
    | Get of int * AsyncReplyChannel<CacheResponse>
    | Clear

type SimpleCache () =
    let cache = Agent<CachingMessage>.Start(fun agent -> async {
        let table = Dictionary<int, string>()
        while true do
            let! msg = agent.Receive()
            match msg with
            | Get(id, replyChannel) -> 
                let defaultResponse = { Id = id; Data = None; Source = Remote }
                let response =
                    if table.ContainsKey(id) then // Found in cache
                        let value = table.[id]
                        { defaultResponse with Data = Some value; Source = Cache }
                    else if id % 10 = 0 then // Simulate can't find
                        defaultResponse
                    else // Add to cache
                        let data = sprintf "Requested at %s" (DateTime.Now.ToString("s"))
                        table.Add(id, data)
                        { defaultResponse with Data = Some data }
                replyChannel.Reply(response)
            | Clear ->
                table.Clear() 
        })

    member __.Get(id) = cache.PostAndAsyncReply(fun reply -> Get(id, reply))
    member __.Clear = cache.Post(Clear)
