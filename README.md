# Countrush - A visitor counter for your GitHub Readme

![](https://countrush-prod.azurewebsites.net/l/badge/?repository=kasuken.countrush)

## Usage

Simply add `![](https://countrush-prod.azurewebsites.net/l/badge/?repository=YourUsername.RepoName)` inside your README.md file and that's all.

## Customisation

There are a few customisations that you can apply.

| Query param name | Possible values                      | Example value      | Description                                         |
|------------------|--------------------------------------|--------------------|-----------------------------------------------------|
| label            | label                                | Strangers          | Overrides the default "visitors" text               |

## Disclaimer

This app doesn't track anything from the users browsers, ip address or stuff like that. The requests come from GitHub CDN and it doesn't send any additional information.

I am using a free tier of Azure Web Apps. It means that sometimes the API takes too much time to start and doesn't display anything on the Readme file.
