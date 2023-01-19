# CalcFreeDays

This is a small program to calculate the number of free days as well as maximum number of consecutive free days for employees in Germany. 
This distinguishes between the federal state the person is employed.
Based on your inputs the program outputs the result within your console.

formal Inputs:
- number of vacation days at the beginning of the year
- the year to be calculated
- federal state where the person is employed 

# Usage 

Call the CalcFreeDays.exe and pass four arguments. 

1. Program execution option 
  'm' -> Calculate maximum number of consecutive free days in the calendar year.
  'f' -> Calculate total free days in the calendar year.
  'a' -> Calculate both options: f and m.
2. Year
3. Number of your vacation days you have at the beginning of the year
4. Federal State in Germany. Please pass the corresponding number as an argument 
      BadenWuerttemberg     = 0
      Bayern                = 1
      Berlin                = 2
      Brandenburg           = 3
      Bremen                = 4
      Hamburg               = 5
      Hessen                = 6
      MecklenburgVorpommern = 7
      Niedersachsen         = 8
      NordrheinWestfalen    = 9
      RheinlandPfalz        = 10
      Saaland               = 11
      Sachsen               = 12
      SachsenAnhalt         = 13
      SchleswigHolstein     = 14
      Thueringen            = 15


e.g. 
>'CalcFreeDays.exe f 2023 30 0'
"Calculate and show the number of free days for the year 2023 with 30 vacation days on 01.01.2023 if I am employed in Baden Württemberg)

# Build 
1. To build a standalone .exe run this command 
   'dotnet publish -r win10-x64 -p:PublishSingleFile=true'
2. Publish .exe and .pdb 

# Possible Improvements
- Implement this calculation for other federal states of germany

## Contribution
If you like and use this program, feel free to donate here: 
[Donate this program](https://www.paypal.com/donate/?hosted_button_id=FR84QT6MVPKFS)


## Licence
This program is licensed unter the MIT License

## Get in contact 

Github - [gremarsl](https://github.com/gremarsl)\
E-Mail:  [startwitharduino@gmail.com ](startwitharduino@gmail.com)

