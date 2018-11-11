# Expense Tracker [![Build Status](https://travis-ci.com/mscerri/ExpenseTracker.svg?branch=master)](https://travis-ci.com/mscerri/ExpenseTracker)
Personal expense tracker

## Development Environment
- Sql Server Express 2017 & Sql Server Management Studio 2017
- Runs in both Visual Studio 2017 & Visual Studio Code
- Node 8.9.4 & NPM 5.6.0
- .NET Core 2.0 sdk
- Angular CLI -> `npm install -g @angular/cli` https://github.com/angular/angular-cli
 

## Setup
To build and run the project using the command line:
1. Restore nuget packages with `dotnet restore` in the `src` directory.
2. Install npm packages with `npm install` in the `src>ExpenseTracker.App>ClientApp` directory.
3. Create the database with `dotnet ef database update` in the `src>ExpenseTracker.Api` directory.
4. Run the API with `dotnet run` in the `src>ExpenseTracker.Api` directory.
4. Run the App with `dotnet run` in the `src>ExpenseTracker.App` directory.
5. Point your browser to **http://localhost:56303**.

Of course, you can also run it from either Visual Studio 2017 or Visual Studio Code with the IDE handling most of the steps above. If you have issues, try running the above steps from the command line to ensure things are setup properly.
