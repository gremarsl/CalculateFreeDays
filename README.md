# CalcFreeDays

This is a small program to calculate the number of free days as an employee in Baden-Württemberg based on your 
- number of vacation days
- the number of holidays falling on a working day 
- days which are Saturdays and Sundays 

# Usage 

Call the CalcFreeDays.exe and pass two arguments. 
1. Option 
  'm' -> Calculate and show maximum number of consecutive free days in the calendar year
  'f' -> Calculate and show total free days in the calendar year
  'a' -> Calculate and show both options: f and m
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

