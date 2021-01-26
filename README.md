# About

This is a Twitter bot to track how far the United States, and Washington State specifically, are towards having most of the population vaccinated against COVID-19.

Once a weekday (or when I choose to run it), it retrieves data from the [CDC COVID Data Tracker](https://covid.cdc.gov/covid-data-tracker/#vaccinations). It then extracts and tweets the percentage of the population who have received both doses of either Pfizer or Moderna and are therefore fully vaccinated.

**This is a hobby project with no guarantees of accuracy. Not affiliated with the CDC.**


# Tech

The bot is written in [F#](https://fsharp.org/) and runs on [Azure Functions](https://azure.microsoft.com/en-us/services/functions/) using the timer trigger.

Many thanks to Scott Wlaschin for the invaluable https://fsharpforfunandprofit.com.
[F# |> I *heart*](./I_Heart_Fsharp_Long_Black_300x75.png)

Disclaimer: I work at Microsoft, but this a personal project with no relation to my job, which is unrelated to F# or Functions.

Progress bar style shamelessly stolen from [Year Progress](https://twitter.com/year_progress).