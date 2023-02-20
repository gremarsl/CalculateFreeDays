using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Transactions;

internal class main

{
    enum FederalState : int
    {
        BadenWuerttemberg = 0,
        Bayern = 1,
        Berlin = 2,
        Brandenburg = 3,
        Bremen = 4,
        Hamburg = 5,
        Hessen = 6,
        MecklenburgVorpommern = 7,
        Niedersachsen = 8,
        NordrheinWestfalen = 9,
        RheinlandPfalz = 10,
        Saaland = 11,
        Sachsen = 12,
        SachsenAnhalt = 13,
        SchleswigHolstein = 14,
        Thueringen = 15
    }

    private static int federalState;

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
        char option = Char.Parse(args[0]);
        int year = Int32.Parse(args[1]);
        int numOfVacDays = Int32.Parse(args[2]);
        federalState = Int32.Parse(args[3]);

        if (option == 'h')
        {
            int month = Int32.Parse(args[4]);
            int office_workdays = Int32.Parse(args[5]);

            // Calculate 
        }

        Console.WriteLine($"Program executed with:" +
            $"\noption:                  {option}" +
            $"\nyear:                    {year}" +
            $"\nnumber of vacation days: {numOfVacDays}" +
            $"\nfederal state:           {federalState}");
        
        // Necessary basic calculations option independent.
        var isLeapYear = DateTime.IsLeapYear(year);
        DateTime new_year = new DateTime(year, 1, 1);
        DayOfWeek dayOne = new_year.DayOfWeek;
        DateTime lastDay = new DateTime(year - 1, 12, 31);
        var dayZero = lastDay.DayOfWeek;

        if (option == 'f' || option == 'a')
        {
            int numOfHolidaysAreWeekdays = 0;
            int numOfHolidaysAreWeekend = 0;

            CalcNumFixHolidays(year, ref numOfHolidaysAreWeekdays, ref numOfHolidaysAreWeekend);

            CalcNumVariableHolidays(in year, ref numOfHolidaysAreWeekdays, ref numOfHolidaysAreWeekend);

            int numOfWeekendDays = CalcWeekendDays(in dayOne);

            // calculate total free days.
            int totalFreeDays = numOfHolidaysAreWeekdays + numOfVacDays + numOfWeekendDays;

            Console.WriteLine("\nSince you have: \n" +
                              $"{numOfVacDays} vacationdays, \n" +
                              $"{numOfHolidaysAreWeekdays} holidays on workingdays and \n" +
                              $"{numOfWeekendDays} days which are Saturday or Sunday \n" +
                              $"-> In total you have {totalFreeDays} free days in {year}");



            // Feature to calc number of free days in a row 
        }
        if (option == 'm' || option == 'a')
        {
            calcMaxNumOfFreeDaysRow(year, numOfVacDays);
        }
    }

    private static void calcMaxNumOfFreeDaysRow(int year,int vacDays)
    {
        Console.WriteLine($"\nStart calculation of maximum possible frees day in a row");

        int maximumDaysInARow = 0; 
        int daysInARow;
        DateTime[] fixHolidays = GetFixHolidays(year);
        DateTime[] varHolidays = GetVariableHolidays(year);

        //Concat two arrays
        DateTime[] totalHolidays = new DateTime[fixHolidays.Length + varHolidays.Length];
        fixHolidays.CopyTo(totalHolidays, 0);
        varHolidays.CopyTo(totalHolidays, fixHolidays.Length);

        int[] dayNumOfYear = totalHolidays.Select(x => x.DayOfYear).ToArray();

        for (int j = 0; j < totalHolidays.Length; j++)
        {
            int loc_vacDays = vacDays;

            DateTime vacStart = totalHolidays[j];

            int numOfDayStart = vacStart.DayOfYear;
            int end = numOfDayStart + loc_vacDays;
            DateTime vacEnd = new DateTime(year, 1, 1).AddDays(end - 1);

            int holiday_cnt = 0;
            for (int i = 0; i < totalHolidays.Length; i++)
            {
                if (vacStart.DayOfYear <= dayNumOfYear[i] && dayNumOfYear[i] <= vacEnd.DayOfYear)
                {
                    if (totalHolidays[i].DayOfWeek == DayOfWeek.Sunday || totalHolidays[i].DayOfWeek == DayOfWeek.Saturday)
                    {
                        continue;
                    }
                    else
                    {
                        loc_vacDays++;
                        holiday_cnt++;
                    }
                }
                
            }

            if (vacDays < 5)
            {
                daysInARow = 2 + vacDays;
                
                if (daysInARow > maximumDaysInARow)
                {
                    maximumDaysInARow = daysInARow;
                }
            }

            else
            {
                int numOfFullWeeks = (int)loc_vacDays / 5;
                int rest = loc_vacDays % 5;

                daysInARow = numOfFullWeeks * 7;

                if (rest == 0) 
                {
                    // +2 for the next upcomming weekend
                    daysInARow = daysInARow + 2;
                }
                else
                {
                    daysInARow = daysInARow + 2 + rest;

                }

                if (daysInARow >= maximumDaysInARow)
                {
                    maximumDaysInARow = daysInARow;
                    
                    int new_end = numOfDayStart + maximumDaysInARow;
                    DateTime new_vacEnd = new DateTime(year, 1, 1).AddDays(new_end - 1);
                    Console.WriteLine($"\nNew Maximum:");
                    Console.WriteLine($"\nMaximum free days in a row: {maximumDaysInARow}. " +
                                      $"\nStart would be: {vacStart} and would finish on {new_vacEnd}." +
                                      $"###");
              
                }
                //TODO: Improve with recursive function call
            }
        }
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

    private static DateTime[] GetFixHolidays(int year)
    {
        List<DateTime> fixHolidays = new List<DateTime>();

        // fixHolidays national wide
        fixHolidays.Add(new DateTime(year, 01, 01));
        fixHolidays.Add(new DateTime(year, 05, 01));
        fixHolidays.Add(new DateTime(year, 10, 03));
        fixHolidays.Add(new DateTime(year, 12, 25));
        fixHolidays.Add(new DateTime(year, 12, 26));

        switch ((FederalState)federalState)
        {
            case FederalState.BadenWuerttemberg:
                fixHolidays.Add(new DateTime(year, 01, 06));
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.Bayern:
                fixHolidays.Add(new DateTime(year, 01, 06));
                fixHolidays.Add(new DateTime(year, 11, 01));
                fixHolidays.Add(new DateTime(year, 08, 15)); // TODO not in all kommunen - maria himmelfahrt
                break;

            case FederalState.Berlin:
                fixHolidays.Add(new DateTime(year, 03, 08)); // Frauentag
                break;

            case FederalState.Brandenburg:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Bremen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Hamburg:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Hessen:
                break;

            case FederalState.MecklenburgVorpommern:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Niedersachsen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.NordrheinWestfalen:
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.RheinlandPfalz:
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.Saaland:
                fixHolidays.Add(new DateTime(year, 08, 15));
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.Sachsen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.SachsenAnhalt:
                fixHolidays.Add(new DateTime(year, 01, 06));
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.SchleswigHolstein:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Thueringen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            default:
                Console.WriteLine("Unkown federal state. Please check your arguments.");
                break;
        }

        return fixHolidays.ToArray();
    }

    private static DateTime[] GetVariableHolidays(int year)
    {
        List<DateTime> variableHolidays = new List<DateTime>();

        DateTime easterSunday = CalcEasterSunday(year);
        int numberOfDayEasterSunday = easterSunday.DayOfYear;

        int karfriday = numberOfDayEasterSunday + -2;
        DateTime karFriday = new DateTime(year, 1, 1).AddDays(karfriday - 1);

        int eastermonday = numberOfDayEasterSunday + 1;
        DateTime easterMonday = new DateTime(year, 1, 1).AddDays(eastermonday - 1);

        // Calculate Christ Ascension
        int numberOfDayChristAscension = numberOfDayEasterSunday + 39;
        DateTime christAscension = new DateTime(year, 1, 1).AddDays(numberOfDayChristAscension - 1);

        // Calculate Pfingstmontag 
        int numberOfDayWhitMonday = numberOfDayEasterSunday + 50;
        DateTime whitmonday = new DateTime(year, 1, 1).AddDays(numberOfDayWhitMonday - 1);

        // Calculate Corpus Christi
        int numberOfDayCorpusChristi = numberOfDayEasterSunday + 60;
        DateTime corpusChristi = new DateTime(year, 1, 1).AddDays(numberOfDayCorpusChristi - 1);

        // Calculate Buss und Bettag
        DateTime endOfChurchCalendarYear = new DateTime(year, 11, 23);
        DayOfWeek endOfChurchCalendarYearDay = endOfChurchCalendarYear.DayOfWeek;
        
        int numberOfDayEndOfChurchCalendarYearDay = endOfChurchCalendarYear.DayOfYear;

        DateTime bussAndBettag = new DateTime(year, 1, 1);
        switch (endOfChurchCalendarYearDay)
        {
            case DayOfWeek.Thursday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 1);
                break;
            case DayOfWeek.Friday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 2);
                break;
            case DayOfWeek.Saturday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 3);
                break;
            case DayOfWeek.Sunday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 4);
                break;
            case DayOfWeek.Monday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 5);

                break;
            case DayOfWeek.Tuesday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 6);

                break;
            case DayOfWeek.Wednesday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 7);

                break;
            default:
                Console.WriteLine("Unkown day for Buss and Bettag.");
                break;

        }
        // national wide
        variableHolidays.Add(karFriday);
        variableHolidays.Add(easterSunday);
        variableHolidays.Add(easterMonday);
        variableHolidays.Add(christAscension);
        variableHolidays.Add(whitmonday);
        


        switch ((FederalState)federalState)
        {
            case FederalState.BadenWuerttemberg:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Bayern:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Berlin:

                break;
            case FederalState.Brandenburg:

                break;
            case FederalState.Bremen:

                break;
            case FederalState.Hamburg:

                break;
            case FederalState.Hessen:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.MecklenburgVorpommern:
                break;
            case FederalState.Niedersachsen:

                break;
            case FederalState.NordrheinWestfalen:
                variableHolidays.Add(corpusChristi);


                break;
            case FederalState.RheinlandPfalz:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Saaland:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Sachsen:
                variableHolidays.Add(bussAndBettag);

                break;
            case FederalState.SachsenAnhalt:

                break;
            case FederalState.SchleswigHolstein:

                break;
            case FederalState.Thueringen:

                break;

            default:
                Console.WriteLine("Unkown federal state. Please check your arguments.");
                break;
        }

        
        return variableHolidays.ToArray();
    }

    private static void CalcNumFixHolidays(in int year,ref int numOfHolidaysAreWeekdays, ref int numOfHolidaysAreWeekend)
    {

        DateTime[] fixHolidays = GetFixHolidays(year);

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

    private static void CalcNumVariableHolidays(in int year, ref int numOfHolidaysAreWeekdays, ref int numOfHolidaysAreWeekend)
    {
        DateTime[] variableholidays = GetVariableHolidays(year);


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