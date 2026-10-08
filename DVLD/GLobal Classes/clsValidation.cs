using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsValidation
    {
        static public bool IsNumberOnly(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        static public bool IsLetters(string text)
        {
            // for names text boxes
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                    return false; 
            }
            return true;
        }

        static public bool IsLettersAndNumbers(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetterOrDigit(c))
                    return false;
            }
            return true;
        }

        static public bool IsValidEmail(string text)
        {
            try
            {
                MailAddress mail = new MailAddress(text);
                return mail.Address == text;
            }
            catch
            {
                return false;
            }
        }

        static public bool IsNumber (char c )
        {
            return char.IsDigit(c);
        }

        static public bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }

        static public bool IsNumberOrLetter(char c)
        {
            return char.IsLetterOrDigit(c) ;
            
        }
       
        static public bool IsNumberOrLetterOrSymbol(char c)
        {
            return (char.IsLetterOrDigit(c) || char.IsSymbol(c));

        }
    
        static public bool IsEmpty(string text)
        {
            if (text == "")
                return true;
            else
                return false;
        }
    }
}
