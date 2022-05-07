# Pokemon API

## Requirements
* Written using Visual Studio (VS) 2022 with .NET (Core) 5 installed. 
* You will need the ASP.NET and Web Development module installed if running from VS


## Usage
* Make a GET request to `[domain]/api/pokemon/{pokemonName}`
* NB: Default host running from VS is `https://localhost:44353`. When using EXE, you will be given the host in the console.
* When running, you can call from swagger at the URL: `https://localhost:44353/swagger/index.html`

## Running
From Visual Studio:
* Run `RafeW.TrueLayer.Pokemon.Api` to access the endpoint.

Via exe:
* Unzip PokemonApiExe.zip
* Open `RafeW.TrueLayer.Pokemon.Api.exe`
* Console window will open, leave open whilst using API.

> Built for win64, please let me know if it's needed on another platform.

## Usage notes
* Translation API is rate limited to 5 requests. Appropriate response will be given when this limit is reached.
* Requests to PokeAPI and Translation API are cached for 10 minutes each.

## Project notes
* `Api` contains the web API code, which essentially is just the endpoint with some error handling.
* `Api` also contains the appsettings.json file which configures the paths for the APIs
* `Engine` is the main library, which is using Repository pattern and configures dependency injection requirements.
* `Engine.Tests` contains tests for Engine library. Containers are used to provide services with mocked entities for easy usage in tests. Test project isn't "fully" done, but I've covered the different types of tests, and in the interest of time not done areas where the tests are very similar to existing tests or trivial.

## Areas for improvement
* Add unit tests for fuller coverage
* Add more specific error messages to capture scenarios where the external APIs may be uncontactable etc.
* Possibly can remove some NuGet packages that were unused (making the zip file very large!)
* Translations API - Not sure if I misunderstood the documentation but couldn't get a POST to work using a JSON object, so went with the URL param pattern instead as the rate limiting wasn't very helpful
* PokeAPI - Noticed there was a library for this whilst browsing the documentation. Probably would be better to use that on a "real" project but for this demo wanted to stick to my own code. 
