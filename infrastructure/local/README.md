To run the whole application, one may use the docker compose.

Before running the compose itself, one have to prepare .env file here. It's content should be as follows (obviously, replace values in [...]):
```
USE_IN_MEMORY_DB=[true/false]
CREATE_AND_DROP_DB=[true/false]
DbPassword=[passwd]
```
**.env file, as well below commands should be run withing this directory.**

Then one have to run the `docker compose build` for the first time, and later on each time the code of app is changed.

To run the app, one have to execute `docker compose up --env-file .env` command.