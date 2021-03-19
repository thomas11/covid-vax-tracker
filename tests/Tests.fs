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
            for p in 0 .. 100 -> (progressBar l (decimal p), l, p)
    }
    |> Seq.iter (fun (bar, l, p) -> (String.length bar, p) |> should equal (l, p))

let firstNCharactersShouldBeFilled n bar =
    bar
    |> Seq.take n
    |> Seq.forall (fun c -> c = filled)
    |> should be True

let charactersFromNShouldBeEmpty n bar =
    bar
    |> Seq.skip (n - 1)
    |> Seq.forall (fun c -> c = empty)
    |> should be True

let shouldBeFilledUntil n bar =
    firstNCharactersShouldBeFilled n bar
    charactersFromNShouldBeEmpty (n + 1) bar

[<Fact>]
let ``progress bar with 0%`` () =
    progressBar 50 0.0m
    |> shouldBeFilledUntil 0

[<Fact>]
let ``progress bar is empty if not enough completed`` () =
    progressBar 50 1.99m
    |> shouldBeFilledUntil 0

[<Fact>]
let ``progress bar of length 20 empty`` () =
    progressBar 20 4.8m
    |> shouldBeFilledUntil 0

[<Fact>]
let ``progress bar of length 20 1 completed`` () =
    progressBar 20 5.0m
    |> shouldBeFilledUntil 1

[<Fact>]
let ``progress bar of length 20 2 completed`` () =
    progressBar 20 10m
    |> shouldBeFilledUntil 2

[<Fact>]
let ``progress bar shows first chunk if just enough completed`` () =
    progressBar 50 2.0m
    |> shouldBeFilledUntil 1

[<Fact>]
let ``progress bar shows half completed`` () =
    progressBar 50 50.0m
    |> shouldBeFilledUntil 25

[<Fact>]
let ``complete Tweet should look as expected`` () =
    tweetContent 13.5m "WA"
    |> should equal "In WA, 13.5% of the population are fully vaccinated (two doses of Pfizer or Moderna or one dose of J&J).\n▓▓░░░░░░░░░░░░░░░░░░"