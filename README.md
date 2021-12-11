# selenium-dotnet-core
A test automation project built on xunit framework using .NET Core and C#.

## Sample Test
This repository contains a sample test automation project built on xunit framework using .NET Core and C#.
The test is supposed to:
- Navigate to Smartlockr website
- Change the language from default to English
- Using UI navigate to the careers page
- Check one vacancy name exists in the list and another one does not.

## Prerequisites:
- .NET Core SDK v2.0 or higher

## Development Environment:
On any terminal (I used Git Bash) move to the "Max.Tests" folder (the folder containing the "Max.Tests.csproj" file),
and execute the following commands:
- dotnet restore
- dotnet test

Alternatively, you can open the project in Visual Studio or VS Code IDE, build the project then execute the test.

## Requirenments:
As a demo version, this test will support only Chrome 96v. In case you use other brovsers/versions you will need to replace chromeDriver binary

