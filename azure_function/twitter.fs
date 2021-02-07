module CdcVaxTracker.Twitter

open Tweetinvi

let progressBar numChars (percentCompleted: float) =
    let scale = 100.0 / (float numChars)

    // We want straight int conversion that chops off everything after the decimal point.
    // The progrss bar should only show a segment as completed once it's fully completed.
    let filledChars = int(percentCompleted / scale)
    let emptyChars = numChars - filledChars

    let completed = String.replicate filledChars "▓"
    let remaining = String.replicate emptyChars "░"

    completed + remaining

let tweetContent numPer100K location =
    let percentage = (float (numPer100K) / 1000.0)
    sprintf
        "In %s, %.1f%% of the population (%i per 100k) received both doses of either the Pfizer or the Moderna vaccine.\n%s"
        location
        percentage
        numPer100K
        (progressBar 20 percentage)

let tweet (client: TwitterClient) (content: string) =
    client.Tweets.PublishTweetAsync(content)
    |> Async.AwaitTask
