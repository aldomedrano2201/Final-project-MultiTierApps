using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Final_Project.VALIDATOR
{
    public class Validator
    {
        public static bool IsValidId(string input)
        {
            //the ^ starts the pattern and the $ ends the pattern, \d{n} = number of characters
            //version 1
            if (!(Regex.IsMatch(input, @"^\d$")))
            {
                return false;
            }


            return true;
        }


        public static bool IsValidId(string input, int size, ref string errMessage, string type)
        {
              if (input.Length == 0)
                {
                    errMessage = "Value is required";
                    return false;
                }


                //the ^ starts the pattern and the $ ends the pattern, \d{n} = number of characters
                //version 1
                if (!(Regex.IsMatch(input, @"^\d{" + size + "}$")))
                {
                    errMessage = "Employee ID must be " + size + "-digit number";
                    return false;
                }
           


            return true;
        }

        public static bool IsValidName(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if ((!(char.IsLetter(input[i]))) && (!(char.IsWhiteSpace(input[i]))))
                {
                    return false;
                }
            }


            return true;
        }

        public static bool IsValidEmail(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (!input.Contains("@"))
                {
                    return false;
                }
            }


            return true;
        }



    }
}
