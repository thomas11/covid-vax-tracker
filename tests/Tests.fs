module Tests

open Xunit
open FsUnit.Xunit

open CdcVaxTracker.Twitter

let empty = '░'
let filled = '▓'

[<Fact>]
let ``all progress bars have the requested length`` () =
    seq {
        for l in 3 .. 200 do
            for p in 0 .. 100 -> (progressBar l (float p), l, p)
    }
    |> Seq.iter (fun (bar, l, p) -> (String.length bar, p) |> should equal (l, p))

let firstNCharactersShouldBeFilled n bar =
    bar
    |> Seq.take n
    |> Seq.forall (fun c -> c = filled)
    |> should be True

let charactersFromNShouldBeEmpty n bar =
    bar
    |> Seq.skip n
    |> Seq.forall (fun c -> c = empty)
    |> should be True

let shouldBeFilledUntil n bar =
    firstNCharactersShouldBeFilled n bar
    charactersFromNShouldBeEmpty n bar

[<Fact>]
let ``progress bar with 0%`` () =
    progressBar 50 0.0
    |> Seq.forall (fun c -> c = '░')
    |> should be True

[<Fact>]
let ``progress bar is empty if not enough completed`` () =
    progressBar 50 1.0
    |> shouldBeFilledUntil 0

[<Fact>]
let ``progress bar shows first chunk completed`` () =
    progressBar 50 2.0
    |> shouldBeFilledUntil 1

[<Fact>]
let ``progress bar shows half completed`` () =
    progressBar 50 50.0
    |> shouldBeFilledUntil 25

[<Fact>]
let ``rounds floats to nearest int`` () =
    roundToInt 0.499 |> should equal 0
    roundToInt 0.5 |> should equal 0
    roundToInt 0.501 |> should equal 1
    roundToInt 0.999 |> should equal 1

[<Fact>]
let ``complete Tweet should look as expected`` () =
    tweetContent 13472 "WA"
    |> should equal "In WA, 13.5% of the population (13472 per 100k) received both doses of either the Pfizer or the Moderna vaccine.\n▓▓▓░░░░░░░░░░░░░░░░░"