using System;

namespace DVLD.Classes
{
    public class clsFormat
    {

        static public string GetGenderName(int Gender)
        {
            switch(Gender)
            {
                case 0:
                    return "Male";

                case 1:
                    return "Female";

                default:
                    return "Male" ;
            }
        }

        static public string GetDateString(DateTime Date)
        {
            return Date.ToString("dd/MM/yyyy");
        }


    }
}
