using System.Runtime.InteropServices;

internal class main

{
    private static DateTime CalcEasterSunday(int year)
    {
        int day = 0;
        int month = 0;

        int g = year % 19;
        int c = year / 100;
        int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

        day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
        month = 3;

        if (day > 31)
        {
            month++;
            day -= 31;
        }

        return new DateTime(year, month, day);
    }

    private static void Main(string[] args)
    {
        // Inputs.
        int numberOfVacationDays = Int32.Parse(args[1]); 
        int year = Int32.Parse(args[0]);

        // Basic calculations.
        var isLeapYear = DateTime.IsLeapYear(year);
        DateTime new_year = new DateTime(year, 1, 1);
        DayOfWeek dayOne = new_year.DayOfWeek;
        DateTime lastDay = new DateTime(year - 1, 12, 31);
        var dayZero = lastDay.DayOfWeek;

        int numOfHolidaysAreWeekdays = 0;
        int numOfHolidaysAreWeekend = 0;

        CalcFixHolidays(year,ref numOfHolidaysAreWeekdays,ref numOfHolidaysAreWeekend);

        CalcVariableHolidays(in year, ref numOfHolidaysAreWeekdays, ref numOfHolidaysAreWeekend);

        int numOfWeekendDays = CalcWeekendDays(in dayOne);
        
        // calculate total free days.
        int totalFreeDays = numOfHolidaysAreWeekdays + numberOfVacationDays + numOfWeekendDays;

        Console.WriteLine("Since you have: \n" +
                          $"{numberOfVacationDays} vacationdays, \n" +
                          $"{numOfHolidaysAreWeekdays} holidays on workingdays and \n" +
                          $"{numOfWeekendDays} days which are Saturday or Sunday \n" +
                          $"-> In total you have {totalFreeDays} free days in {year}");

    }

    private static int CalcWeekendDays(in DayOfWeek dayOne)
    {
        var numOfWeekendDays = 52 * 2;

        switch (dayOne)
        {
            case DayOfWeek.Friday:
                numOfWeekendDays += 2;
                break;

            case DayOfWeek.Saturday:
                numOfWeekendDays += 2;
                break;
            case DayOfWeek.Sunday:
                numOfWeekendDays += 1;
                break;
        }

        return numOfWeekendDays;
    }

    private static void CalcFixHolidays(in int year,ref int numOfHolidaysAreWeekdays, ref int numOfHolidaysAreWeekend)
    {
        //TODO pass this array as reference and store the holidays in the array 
        DateTime[] fixHolidays = new DateTime[]
        {
            new DateTime(year, 01, 01),
            new DateTime(year, 01, 06),
            new DateTime(year, 05, 01),
            new DateTime(year, 10, 03),
            new DateTime(year, 11, 01),
            new DateTime(year, 12, 25),
            new DateTime(year, 12, 26),
        };


        foreach (DateTime holiday in fixHolidays)
        {
            if (holiday.DayOfWeek == DayOfWeek.Sunday || holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                numOfHolidaysAreWeekend++;
            }
            else
            {
                numOfHolidaysAreWeekdays++;
                Console.WriteLine("The holiday: {0} is a {1}", holiday, holiday.DayOfWeek);
            }
        }

    }

    private static void CalcVariableHolidays(in int year, ref int numOfHolidaysAreWeekdays, ref int numOfHolidaysAreWeekend)
    {
        DateTime[] variableholidays = new DateTime[6];

        variableholidays[0] = new DateTime(year, 04, 07);

        DateTime easterSunday = CalcEasterSunday(year);
        int friday = easterSunday.Day - 2;
        DateTime karFriday = new DateTime(year, easterSunday.Month, friday);
        int monday = easterSunday.Day + 1;
        DateTime easterMonday = new DateTime(year, easterSunday.Month, monday);

        variableholidays[0] = karFriday;
        variableholidays[1] = easterSunday;
        variableholidays[2] = easterMonday;

        int numberOfDayEasterSunday = easterSunday.DayOfYear;

        // Calculate Christ Ascension
        int numberOfDayChristAscension = numberOfDayEasterSunday + 39;
        DateTime christAscension = new DateTime(year, 1, 1).AddDays(numberOfDayChristAscension - 1);
        variableholidays[3] = christAscension;

        // Calculate Pfingstmontag 
        int numberOfDayWhitMonday = numberOfDayEasterSunday + 50;
        DateTime whitmonday = new DateTime(year, 1, 1).AddDays(numberOfDayWhitMonday - 1);
        variableholidays[4] = whitmonday;

        // Calculate Corpus Christi
        int numberOfDayCorpusChristi = numberOfDayEasterSunday + 60;
        DateTime corpusChristi = new DateTime(year, 1, 1).AddDays(numberOfDayCorpusChristi - 1);
        variableholidays[5] = corpusChristi;

        // NOW START TO CALCULATE

        foreach (DateTime holiday in variableholidays)
        {
            if (holiday.DayOfWeek == DayOfWeek.Sunday || holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                numOfHolidaysAreWeekend++;
            }
            else
            {
                numOfHolidaysAreWeekdays++;
                Console.WriteLine("The holiday: {0} is a {1}", holiday, holiday.DayOfWeek);
            }
        }
    }
}
