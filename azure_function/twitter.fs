module CdcVaxTracker.Twitter

open System
open Tweetinvi

// Math.Round rounds .5 down, not up!
let roundToInt (f: float) = Convert.ToInt32(Math.Round(f))

let progressBar numChars (percentCompleted: float) =
    let scaledown = 100.0 / (float numChars)

    let filledChars = roundToInt (percentCompleted / scaledown)
    let emptyChars = numChars - filledChars

    let completed = String.replicate filledChars "▓"
    let remaining = String.replicate emptyChars "░"

    completed + remaining

let tweetContent numPer100K location =
    let percentage = (float (numPer100K) / 1000.0)
    sprintf
        "In %s, %i per 100k = %.1f%% received both doses of either the Pfizer or the Moderna vaccine.\n%s"
        location
        numPer100K
        percentage
        (progressBar 20 percentage)

let tweet (client: TwitterClient) (content: string) =
    client.Tweets.PublishTweetAsync(content)
    |> Async.AwaitTask
