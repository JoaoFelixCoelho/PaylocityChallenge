namespace Api.Extensions;

public static class GenericUtil
{
    //https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-based-on-a-datetime-type-birthday
    public static int GetAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (dateOfBirth.Date > today.AddYears(-age)) 
            age--;

        return age;
    }
}

