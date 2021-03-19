module CdcVaxTracker.CDC

open System.Net.Http

let httpClient = new HttpClient()

open FSharp.Data

type CdcData = JsonProvider<Sample="sampleVaxData.json">


let retrieveRawCdcData () =
    let url =
        "https://covid.cdc.gov/covid-data-tracker/COVIDData/getAjaxData?id=vaccination_data"

    let request =
        new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url)

    async {
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask
        response.EnsureSuccessStatusCode() |> ignore

        let! body =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        return body
    }

let parse cdcData = CdcData.Parse(cdcData).VaccinationData

let getCdcData () =
    async {
        let! rawData = retrieveRawCdcData ()
        return parse rawData
    }

let getFullyVaccinatedPercentage (vaxData: CdcData.VaccinationData array) location =
    let locationRow =
        vaxData
        |> Array.where (fun row -> row.Location = location)
        |> Seq.exactlyOne

    locationRow.SeriesCompletePopPct
