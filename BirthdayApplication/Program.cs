using System;
using System.Globalization;

namespace BirthdayApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your birthday in M/DD/YY format");
            string birthdayString = Console.ReadLine();
            DateTime birthday = StringToDateConverter(birthdayString);
            HowManyYearsOld(birthday);
            HowManyMonthsOld(birthday);
            HowManyDaysOld(birthday);
            CalculateNextBirthdayDays(birthday);
            Console.ReadLine();
        }

        private static DateTime StringToDateConverter (string str)
        {
            if(DateTime.TryParseExact(str, "M/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthday))
            {
                return birthday;
            }
            else
            {
                throw new Exception("Please enter a valid date format (M/dd/yy).");
            }
        }
        private static void HowManyYearsOld(DateTime birthday)
        {
            DateTime now = DateTime.Now;
            // calculate the raw number of years as difference between now and birth year
            int years = now.Year - birthday.Year;
            // if the person has not had a birthday this year, subtract one year
            if (now.Month < birthday.Month)
            {
                years -= 1;
            }
            else
            {
                // if April 1 and birthday April 21, not birthday yet
                if (now.Month == birthday.Month && now.Day < birthday.Day)
                {
                    years -= 1;
                }
            }
            Console.WriteLine($"You are {years} years old.");
        }

        private static void HowManyMonthsOld(DateTime birthday)
        {
            DateTime now = DateTime.Now;
            int months = (now.Year - birthday.Year) * 12;
            months += now.Month - birthday.Month; // add the difference in months

            // if the birthday hasn't occurred yet this month, subtract one month
            if (now.Day < birthday.Day)
            {
                months--;
            }
            Console.WriteLine($"You are {months} months old.");
        }

        private static void HowManyDaysOld(DateTime birthday)
        {
            DateTime now = DateTime.Now;
            // subtract one dateTime from another gives a timespan
            TimeSpan difference = now - birthday; 
            int days = difference.Days;
            Console.WriteLine($"You are {days} days old.");
        }

        private static void CalculateNextBirthdayDays(DateTime birthday)
        {
            DateTime now = DateTime.Now;
            // e.g., 2023, January, 2
            DateTime nextBirthday = new DateTime(now.Year, birthday.Month, birthday.Day);

            // If the birthday has already occurred this year, add one year to get the next
            // if 2023, January, 2 < 2023, August, 10, add a year so 2024 is next birthday year
            if (nextBirthday < now)
            {
                nextBirthday = nextBirthday.AddYears(1);
            }

            // Subtract the current date from the next birthday date
            // e.g., difference = 2024, January, 2 - 2023, August, 10
            TimeSpan difference = nextBirthday - now;
            int days = difference.Days;
            Console.WriteLine($"It will be {days} days until your next birthday!");
        }

    }
}