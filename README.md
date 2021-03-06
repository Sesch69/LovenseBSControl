# LovenseBSControl

Small private project to add the posibility to control Toys from the Lovense company with Miss or Hits or both, direct on playing Beat Saber with the API from Lovense.

## What is this?
This Mod adds the posibility to control the vibrations of Lovense Toys with direct playing the VR game Beat Saber. 
You can configure the intense and duration of the toys. Also it is possible to set up the vibration on miss or/and hit.

## Prerequisites

- Lovense Connect from www.lovense.com must be installed on PC (Default Connection setting in the mod settings)
- OR Lovense Connect for Android/IPhone and using the IP adress + port in the extended connection configuration (Click on the green shield in the app)
- Toys has to be connected with Lovense Connect App
- Mod BSIPA is required
- Mod is based on Lib Harmony

## Installation

- Unpack Zip file into the Plugin folder of the main directory of Beat Saber
- Goto to Settings -> Mod Settings -> Check Lovense BS Control settings

## Setting Options

Settings:
* Enable Mod
* Modus: Select a modus to play
* Vibrate on miss: Vibrate toys on miss
* Vibrate on hit: Vibrate toys on hitting boxes
* Preset on bomb hit: Vibrate toys with a preset on hitting a bomb
* Random Intense: Random intense between 1 and 20
* Intense: Fix intense on miss/hit block
* Duration: Duration of vibration in milliseconds (more or less exact)

* (Rotate Intense (Nora): Work in progress)
* (Air Intense (Max): Work in progress)
* Toys: Shows connected toy in a list, allows to refresh list and test the connected toys. Also possible to select which hand controls the toy
* Extended Connection: Allows to config the connection in details for Lovense Connect on smartphone or with local host 127.0.0.1 for Lovense Connect PC App

## Modus

- Default Modus: Use the configuration for hit/miss/intense/duration
- Challenge 1: With each miss, the vibration inreases, after 15 correct hits, it is reducing by 1 intense level
- Preset: Vibrate on Miss with a defined preset
