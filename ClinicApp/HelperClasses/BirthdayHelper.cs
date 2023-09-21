using System;

namespace ClinicApp
{
    //checking if a patient birthday is correct
    //the date of birth in the future is not allowed
    //the maximum allowed age is set 
    static public class BirthdayHelper
    {
        private const int MAX_POSSIBLE_AGE = 110;

        public static bool IsBirthdayValid(this DateTime? userDateOfBirthday)
        {
            DateTime dateOfBirthday = Convert.ToDateTime(userDateOfBirthday);
            var currentDay = DateTime.Now;

            int ageOfPatient = currentDay.Year - dateOfBirthday.Year - 1 +
                ((currentDay.Month > dateOfBirthday.Month || currentDay.Month == dateOfBirthday.Month && currentDay.Day >= dateOfBirthday.Day)
                ? 1 : 0);

            return dateOfBirthday <= DateTime.Now && ageOfPatient <= MAX_POSSIBLE_AGE;
        }
    }
}
