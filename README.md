# WILLIAMS_AGBO-ISL21-REQ117214-Written

This project is a **Rules Engine** that determines client eligibility for the **Winter Supplement** and calculates the corresponding benefit amount based on predefined rules. It integrates with an **MQTT broker** to receive input data and publish the processed results.

---

## Table of Contents
- [WILLIAMS\_AGBO-ISL21-REQ117214-Written](#williams_agbo-isl21-req117214-written)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [Features](#features)
  - [Setup Instructions](#setup-instructions)
    - [Prerequisites](#prerequisites)
    - [Packages Used](#packages-used)
    - [Technical Details](#technical-details)
    - [MQTT Topics](#mqtt-topics)
    - [Project Structure](#project-structure)
    - [Note](#note)

---

## Overview

The Rules Engine processes eligibility and calculates the Winter Supplement amount for various family types. The calculation rules are as follows:

| **Family Composition**                | **Base Amount** | **Children Amount** |
|---------------------------------------|-----------------|---------------------|
| Single (no children)                  | $60/year        | $0/child           |
| Couple (no children)                  | $120/year       | $0/child           |
| Any family type with children         | Base Amount + $20/child |

The **Rules Engine**:
1. Listens for input data on a specific MQTT topic.
2. Processes the data and determines eligibility and benefit amounts.
3. Publishes the processed results to an MQTT output topic.

---

## Features

- **Event-driven architecture** with MQTT for communication.
- Rules-based processing for eligibility and benefit calculation.
- JSON-based input and output data formats.
- Unit tests for ensuring code reliability.

---

## Setup Instructions

### Prerequisites
- **.NET SDK** (version 7.0 or later)
- **MQTT Explorer** (for testing MQTT topics)
- A free MQTT broker (default: `test.mosquitto.org`)
- **Visual Studio Code** or any other code editor
- **Git** for cloning the repository

---

### Packages Used

The project uses the following NuGet packages:

1. **MQTTnet**: For MQTT client operations
   ```bash
   dotnet add package MQTTnet --version 4.1.1
2. **System.Text.Json**: For JSON serialization and deserialization
    ```bash
    dotnet add package System.Text.Json --version 7.0.2
3. ***MSTest: For unit testing
    ```bash
    dotnet add package MSTest.TestFramework --version 3.6.3
    dotnet add package MSTest.TestAdapter --version 3.6.3

### How to Run Code

1. **Update the .env File**
   
    USERNAME=user
    PASSWORD=r44UKbfSeIn9AZjI4Ed24xr6
    TOPIC_ID=Your MQTT Topic ID

2. **Build The project**
   
   Navigate to src folder in terminal
   ```bash
   dotnet build
    ```
3. **Run the application**

    The console will display messages as it connects to the MQTT broker, subscribes to topics, and processes messages.

    ```bash
    dotnet run

4. **Test with MQTT Explorer**

    a. Subscribe to the input topic:
    ```
    BRE/calculateWinterSupplementInput/<TOPIC_ID> # Put in you MQTT ID from web calculator/.env
    ``` 

    b. Publish test Json in the Mqtt Explorer
    ```json
    {
    "id": "123",
    "numberOfChildren": 2,
    "familyComposition": "couple",
    "familyUnitInPayForDecember": true
    }   
    ```

    c. Check the Output Topic: 
    ```
        BRE/calculateWinterSupplementOutput/<TOPIC_ID>

    ```
    Example (from visual studio terminal to cmd mosquitto):
    ```
        Rules Engine Project Running...
        Connected to MQTT broker.
        Subscribed to topic: BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238
        Listening for MQTT messages. Press Enter to exit...
        Received message on BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {"numberOfChildren": 15, "familyComposition": "couple", "familyUnitInPayForDecember": true}
        Published message to topic: BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238
        Published result to BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {"id":"","isEligible":true,"baseAmount":120,"childrenAmount":300,"supplementAmount":420}
    ```
        Example (from visual studio terminal to MQTT Explorer):
    ```
        Rules Engine Project Running...
        Connected to MQTT broker.
        Subscribed to topic: BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238
        Listening for MQTT messages. Press Enter to exit...
        Received message on BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {
        "id": "1288",
        "numberOfChildren": 12,
        "familyComposition": "Single",
        "familyUnitInPayForDecember": true
        }
        Published message to topic: BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238
        Published result to BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {"id":"1288","isEligible":true,"baseAmount":0,"childrenAmount":240,"supplementAmount":240}
        Received message on BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {
        "id": "1288",
        "numberOfChildren": 12,
        "familyComposition": "couple",
        "familyUnitInPayForDecember": true
        }
        Published message to topic: BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238
        Published result to BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {"id":"1288","isEligible":true,"baseAmount":120,"childrenAmount":240,"supplementAmount":360}
        Received message on BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {
        "id": "1288",
        "numberOfChildren": 2,
        "familyComposition": "couple",
        "familyUnitInPayForDecember": true
        }
        Published message to topic: BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238
        Published result to BRE/calculateWinterSupplementOutput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238: {"id":"1288","isEligible":true,"baseAmount":120,"childrenAmount":40,"supplementAmount":160}
    ```

## Testing

### Run Unit Tests
Navigate to Test directory in terminal. To execute the tests, run:

```bash
    dotnet test
```
The output will indicate whether all test cases passed or failed.

Example of test Output:
```
    Microsoft (R) Test Execution Command Line Tool Version 17.4.0 (x64)
    Copyright (c) Microsoft Corporation.  All rights reserved.

    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.

    Passed!  - Failed:     0, Passed:     6, Skipped:     0, Total:     6, Duration: 70 ms  - Tests.dll (net7.0)
```

### Technical Details
1. Input Data Format:
    
    The input data is JSON and must conform to the following schema:

```json
    {
        "id": "string",
        "numberOfChildren": "int",
        "familyComposition": "string",
        "familyUnitInPayForDecember": "bool"
    }
```
2. Output Data Format:
   
The output data is JSON and conforms to the following schema:

```json
    {
        "id": "string",
        "isEligible": "bool",
        "baseAmount": "float",
        "childrenAmount": "float",
        "supplementAmount": "float"
    }
```
### MQTT Topics
1. Input Topic: BRE/calculateWinterSupplementInput/<TOPIC_ID>
2. Output Topic: BRE/calculateWinterSupplementOutput/<TOPIC_ID>

### Project Structure
```
-WILLIAMS_AGBO-ISL21-REQ117214-Written/
├── src/
│   ├── Models/
│   │   ├── InputData.cs       # Defines the input data structure
│   │   └── OutputData.cs      # Defines the output data structure
│   ├── MqtHandler.cs          # Handles MQTT operations (connect, subscribe, publish)
│   ├── RulesEngine.cs         # Processes eligibility and benefit calculations
│   ├── appsettings.json       # Contains configuration settings
│   ├── .env                   # Environment variables for MQTT credentials
│   └── program.cs             # Entry point of the application
├── Tests/
│   ├── Test_RulesEngine.cs    # Unit tests for RulesEngine
├── .gitignore                 # Specifies files to ignore in version control
├── README.md                  # Project documentation
```

### Note
To run on mosquitto on local temrminal(CMD) after using dotnet run on Visual studio Termianl:
```
    C:\Users\----\mosquitto>mosquitto_pub -h test.mosquitto.org -t BRE/calculateWinterSupplementInput/a4f3e43a-1277-4b2d-bfce-f345a5a5c238 -m "{\"numberOfChildren\": 15, \"familyComposition\": \"couple\", \"familyUnitInPayForDecember\": true}"
```