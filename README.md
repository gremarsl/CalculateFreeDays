# CalcFreeDays

This is a small program to calculate the number of free days as an employee in Baden-Württemberg based on your 
- number of vacation days
- the number of holidays falling on a working day 
- days which are Saturdays and Sundays 

# Usage 

Call the CalcFreeDays.exe and pass two arguments. 
1. Year
2. Number of your vacation days you have at the beginning of the year

e.g. 
CalcFreeDays 2023 30
"Calculate the number of free days for the year 2023 with 30 vacation days on 01.01.2023)

# Build 
1. To build a standalone .exe run this command 
   dotnet publish -r win10-x64 -p:PublishSingleFile=true
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

