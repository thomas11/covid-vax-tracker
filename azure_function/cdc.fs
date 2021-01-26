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

let getTwoDosesPer100K (vaxData: CdcData.VaccinationData array) location =
    let locationRow =
        vaxData
        |> Array.where (fun row -> row.Location = location)
        |> Seq.exactlyOne

    match locationRow.AdministeredDose2Per100k with
    | Some per100k -> Some per100k
    | None ->
        match (locationRow.Census2019, locationRow.AdministeredDose2) with
        | (Some population, Some dose2) -> Some(dose2 / (population / 100000))
        | (None, _)
        | (_, None) -> None
