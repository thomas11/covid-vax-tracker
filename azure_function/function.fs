namespace CdcVaxTracker.Function

open System
open Microsoft.Azure.WebJobs
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Http
open Microsoft.Azure.WebJobs.Extensions.Http

open Tweetinvi

open CdcVaxTracker.CDC
open CdcVaxTracker.Twitter

module cdc_vax_function =

    let twitterApiKey =
        Environment.GetEnvironmentVariable("TwitterApiKey")

    let twitterApiSecret =
        Environment.GetEnvironmentVariable("TwitterApiSecret")

    let twitterAccessToken =
        Environment.GetEnvironmentVariable("TwitterAccessToken")

    let twitterTokenSecret =
        Environment.GetEnvironmentVariable("TwitterTokenSecret")

    let generateTweetForLocation cdcData location locationName =
        getTwoDosesPer100K cdcData location
        |> Option.map (fun doses -> tweetContent doses locationName)

    let tweetAndLog twitterClient (log: ILogger) content location =
        match content with
        | Some tw ->
            log.LogInformation tw
            tweet twitterClient tw |> ignore
        | None -> log.LogWarning(sprintf "No data found for location '%s'" location)

    let run (log: ILogger) =
        let data = getCdcData () |> Async.RunSynchronously
        let tweetGenerator = generateTweetForLocation data

        let waTweet = tweetGenerator "WA" "WA"
        let usTweet = tweetGenerator "US" "the United States"

        let twitterClient =
            TwitterClient(twitterApiKey, twitterApiSecret, twitterAccessToken, twitterTokenSecret)

        let tweeter = tweetAndLog twitterClient log

        async {
            tweeter waTweet "WA"
            tweeter usTweet "US"
        }
        |> Async.RunSynchronously

    [<FunctionName("TweetCdcDataTimer")>]
    // 8p EST = 1a UTC Mon-Fri
    let runTimer ([<TimerTrigger("0 0 1 * * 1-5")>] myTimer: TimerInfo, log: ILogger) =
        let msg =
            sprintf "Time trigger function executed at: %A" DateTime.Now

        log.LogInformation msg
        run log

    [<FunctionName("TweetCdcDataHttp")>]
    let runHttp ([<HttpTrigger(AuthorizationLevel.Anonymous, "post")>] req: HttpRequest, log: ILogger) = run log
