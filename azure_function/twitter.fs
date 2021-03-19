module CdcVaxTracker.Twitter

open System
open Tweetinvi

let progressBar numChars (percentCompleted: decimal) =
    let scale = 100m / (decimal numChars)

    // We want straight int conversion that chops off everything after the decimal point.
    // The progrss bar should only show a segment as completed once it's fully completed.
    let filledChars = int (percentCompleted / scale)
    let emptyChars = numChars - filledChars

    let completed = String.replicate filledChars "▓"
    let remaining = String.replicate emptyChars "░"

    completed + remaining

let tweetContent (percentage: decimal) location =
    let roundedPercentage = Math.Round(percentage, 1)

    sprintf
        "In %s, %.1f%% of the population are fully vaccinated (two doses of Pfizer or Moderna or one dose of J&J).\n%s"
        location
        roundedPercentage
        (progressBar 20 roundedPercentage)


let tweet (client: TwitterClient) (content: string) =
    client.Tweets.PublishTweetAsync(content)
    |> Async.AwaitTask
