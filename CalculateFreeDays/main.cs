using System.Runtime.InteropServices;

internal class main

{
    private static DateTime EasterSunday(int year)
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


        //Input
        int numberOfVacationDays = Int32.Parse(args[1]);

        // TODO enter year 
        // parse input 
        int year = Int32.Parse(args[0]);

        var isLeapYear = DateTime.IsLeapYear(year);
        DateTime new_year = new DateTime(year, 1, 1);
        DayOfWeek dayOne = new_year.DayOfWeek;
        DateTime lastDay = new DateTime(year - 1, 12, 31);
        var dayZero = lastDay.DayOfWeek;

        int numberOfHolidaysAreWeekdays = 0;
        int numberOfHolidaysAreWeekend = 0;

        DateTime[] fixholidays = new DateTime[]
        {
            new DateTime(year, 01, 01),
            new DateTime(year, 01, 06),
            new DateTime(year, 05, 01),
            new DateTime(year, 10, 03),
            new DateTime(year, 11, 01),
            new DateTime(year, 12, 25),
            new DateTime(year, 12, 26),
        };


        foreach (DateTime holiday in fixholidays)
        {
            if (holiday.DayOfWeek == DayOfWeek.Sunday || holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                numberOfHolidaysAreWeekend++;
            }
            else
            {
                numberOfHolidaysAreWeekdays++;
                Console.WriteLine("The holiday: {0} is a {1}", holiday, holiday.DayOfWeek);

            }
        }

        DateTime easterSunday = EasterSunday(year);

        //TODO pass this array as reference and store the holidays in the array 
        DateTime[] variableholidays = new DateTime[6];

        GetVariableHolidays(year, ref variableholidays);

        foreach (DateTime holiday in variableholidays)
        {
            if (holiday.DayOfWeek == DayOfWeek.Sunday || holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                numberOfHolidaysAreWeekend++;
            }
            else
            {
                numberOfHolidaysAreWeekdays++;
                Console.WriteLine("The holiday: {0} is a {1}", holiday, holiday.DayOfWeek);
            }
        }

        var weekendDays = 52 * 2;
        switch (dayOne)
        {
            case DayOfWeek.Friday:
                weekendDays += 2;
                break;

            case DayOfWeek.Saturday:
                weekendDays += 2;
                break;
            case DayOfWeek.Sunday:
                weekendDays += 1;
                break;
        }

        int totalFreeDays = numberOfHolidaysAreWeekdays + numberOfVacationDays + weekendDays;

        Console.WriteLine("Since you have: \n" +
                          "{0} vacationdays, \n" +
                          "{1} holidays on workingdays and \n" +
                          "{2} days which are Saturday or Sunday \n" +
                          "You have in total {3} free days in {4}", numberOfVacationDays, numberOfHolidaysAreWeekdays, weekendDays, totalFreeDays, year);



    }

    private static void GetVariableHolidays(in int year, ref DateTime[] refArray)
    {
        refArray[0] = new DateTime(year, 04, 07);

        DateTime easterSunday = EasterSunday(year);
        int friday = easterSunday.Day - 2;
        DateTime karFriday = new DateTime(year, easterSunday.Month, friday);
        int monday = easterSunday.Day + 1;
        DateTime easterMonday = new DateTime(year, easterSunday.Month, monday);

        refArray[0] = karFriday;
        refArray[1] = easterSunday;
        refArray[2] = easterMonday;


        //// Calculate Christ Ascension
        int numberOfDayEasterSunday = easterSunday.DayOfYear;

        int numberOfDayChristAscension = numberOfDayEasterSunday + 39;
        DateTime christAscension = new DateTime(year, 1, 1).AddDays(numberOfDayChristAscension - 1);
        refArray[3] = christAscension;

        //Console.WriteLine("ChristAscension: {0}", christAscension);


        // Calculate Pfingstmontag 
        // genau 50 Tage nach Ostern 
        //Der Pfingstsonntag ist der 50. Tag der Osterzeit, also 49 Tage nach dem Ostersonntag
        // Pfingstmontag ist 50 Tage nach Ostersonntag
        int numberOfDayWhitMonday = numberOfDayEasterSunday + 50;
        DateTime whitmonday = new DateTime(year, 1, 1).AddDays(numberOfDayWhitMonday - 1);
        //Console.WriteLine("WhitMonday: {0}", whitmonday);
        refArray[4] = whitmonday;


        //Calculate Corpus Christi
        // genau 60 Tage nach dem Ostersonntag
        int numberOfDayCorpusChristi = numberOfDayEasterSunday + 60;
        DateTime corpusChristi = new DateTime(year, 1, 1).AddDays(numberOfDayCorpusChristi - 1);
        //Console.WriteLine("CorpusChristi: {0}", corpusChristi);
        refArray[5] = corpusChristi;

    }
}
