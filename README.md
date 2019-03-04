# A3 Motion Simulator applications for Project CARS 2
This repository contains a number of applications for usage with an Atomic A3 motion simulator ([link here](https://www.atomicmotionsystems.com/)) and SIMPYHNITY, Atomic's software for controlling the motion output. 

The applications found in this repository are only designed to work with Project CARS 2 only. SIMPHYNITY already has a Project CARS 2 plugin which is shipped with the software. This communicates with the simulator and provides the standard range of motion cues.

However, we wanted to alter the range of motion given to users to study its effects on participants. This is the motivation behind the development of this repository.

## Contents
There are four software applications contained within this project:


### 1. Motion Range Configuration Utility (`/chair-inverter`)
An application which is capable of providing normal `(x, y)`, negated `(-x, -y)` and null `(0, 0)` ranges of motion for Project CARS 2. This is done via SIMPHYNITY's UDP mode, with the application just serving as a basic UDP client. 

### 2. Project CARS 2 Telemetry Logger (`/project-cars-telemetry-writer`)
An application that when run, will log telemetry data to a file whilst Project CARS 2 is running. This application will only log data whilst the player is racing. 

Log files contain a variety of different telemetric statistics regarding the player's lap. A snapshot of these are taken every `x` interval, and saved to the log. This enables a full analysis of their lap at a later date, using a data visualisation tool.

### 3. WIP: Telemetry Visualisation Tool (`/chair-telemetry-visualiser`)
The Telemetry Visualisation Tool is currently a work in progress. When completed, this tool aims to visualise the telemetry data saved in the generated log files.

### 4. Motion Platform GUI system (`/chair-gui`)
A GUI front-end for the the Motion Range Configuration Utility. It is worth noting that this application is used currently for some studies being ran at the university. 

Consequently, the GUI has been developed with the management of participants in mind. So this may not be useful, however it is included in this repository for the sake of completeness.






