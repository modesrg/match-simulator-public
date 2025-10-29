# Mini Tournament Simulator

This project is in a tournament simulator with rounds and matches.
It consists of 3 parts:

- A REST API with an endpoint to trigger the tournament simulation (access via /swagger or /api/tournament/simulate)
- A Blazor page to trigger a simulation and view the results (access via /tournament)
- Test project

## How To Use

From debug:

- Option 1: Go to https://localhost:7207/tournament, each time the page is refreshed it triggers a new simulation
- Option 2: Go to https://localhost:7207/swagger and try it out
- Option 3: Via Postman https://localhost:7207/api/tournament/simulate

From release:

- Unzip the file tournament_simulator_v1.zip in the folder "tournament_simulator/"
- Navigate to "publish/GB.MatchSimulator.exe"
- Open http://localhost:5000/tournament in the browser (http://localhost:5000/swagger also available)

## Decisions

- File Scoped Namespaces and Primary Constructors used for readability.
- Used manual mapping instead of libraries like AutoMapper for performance and to avoid issues in case they go commercial in the future.
- Comments kept to a minimum except for edge cases, TODOs or very niche algorithms.
- Options pattern for configuration (with some defaults based on the assessment requirements).
- Strongly typed HttpClients.
- Basic FE in Blazor.
- Unit tests in xUnit
- REST API + Blazor + Domain items in one project for simplicity's sake.
- Basic response error handling.
- Basic persistence layer (Proof Of Concept) using EF, InMemory and a team seeder.

## Going further (bonus)

Added the following features, implicitly considered out-of-scope to make it a bit more extensible:

- SecondStage Option to split the tournament into 2 Stages with teams switching home-away in the 2nd stage (default to false).
- RandomFixtures Option to create rounds with trully random fixtures (default to false).
- AverageScorePerMatch Option to set an average scores per match (default to 2.8, the average total goals in European Soccer matches).
- Stored tournament results and teams in DB (InMemory as a placeholder for proof-of-concept).
- Added an api/Tournament/history endpoint to retrieve the historic results

## Out-Of-Scope assumptions and Future Nice-To-Haves

The following items were considered out of scope and/or skipped to avoid over-engineering (YAGNI principle) but would be valuable as hypothetical future improvements:

- Nicer handling of HTTP responses (e.g. via Outcome pattern and Exception handlers).
- Implement home team advantage.
- Split FE, API and Core functionalities into different projects or solutions.
- Generic base classes and interfaces
- More Unit Tests cases and scenarios, perhaps with the help of QA.
- Simulate individual Rounds or Matches.
- Persist data to a permanent DB (Sqlite, SQL Server, Elastic Search...)
- View for historic data described in the "bonus" section.

## AI disclosure

LLM was used with human oversight to:

- Implement a suitable Poisson algorithm to simulate realistic scores (funnily enough I later found the original source anyway).
- Implement Head to Head calculations.
- Mapping boilerplate.
- Front-End tweaking and CSS.
