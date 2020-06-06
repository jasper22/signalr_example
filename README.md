# SignalR example

This is simple example to show how to use SignalR with ASP .NET Core 3.1 and Angular 9

## Server 

Server actually implement only 1 endpoint: GET -> https://localhost:5001/api/Signal. Calling this endpoint will trigger SignalR and it strat sending random messages for 1 minute

## Client

Angular 9 client just subscribe to server SignalR and then send GET request to server to trigger SignalR. Nothing fancy - all results will be output to screen with simple `<li>` tag



Code contains a lot more comments to explain the mechanics

## Requirements

* Server side was written in .NET Core 3.1 in Visual Studio 2019
* Client side was written in Angular 9 in VS Code

Please download/install if needed

## Usage

1. Clone the repo: `git clone https://github.com/jasper22/signalr_example.git`
2. Enter `signalr_example` folder
3. Restore all dotnet packages: `dotnet restore`
4. Enter to server folder: `cd .\src\WebServer`
5. Start server side: `dotnet run`
6. Enter to client folder: `cd .\src\AngularClient\`
7. Restore NPM packages: `npm install`
8. Start the Angular client: `ng server`

It should automativally redirect to main site: http://localhost:4200/  Once you enter this site the messages from server side SignalR should appear on the screen

Enjoy :wink:


#### TODO
* Add Docker support