// Actor model
module Agent

open System

type Agent<'T> = MailboxProcessor<'T>

type ThrottleMessage = { 
    Id : int
    ReplyChannel : AsyncReplyChannel<string> 
}

let createThrottler delay =
    let agent = MailboxProcessor<ThrottleMessage>.Start(fun inbox ->
        async {
            while true do
                // When we receive a request, we count how long the 
                // processing took and then we sleep the agent for the
                // specified time to limit the request rate
                let! req = inbox.Receive()
                let sw = System.Diagnostics.Stopwatch.StartNew()
                let now = DateTime.UtcNow.ToString("s")
                let res = sprintf "Msg %i was processed at %s" req.Id now
          
                req.ReplyChannel.Reply(res)

                let sleep = delay - (int sw.ElapsedMilliseconds)
                if sleep > 0 then do! Async.Sleep(sleep)
        })

    /// This creates a new message and sends it to the agent.
    /// The result is a workflow that waits until the message
    /// is processed and then it returns the downloaded string.
    let download id = 
        agent.PostAndAsyncReply(fun reply ->
            { Id = id; ReplyChannel = reply })

    download
