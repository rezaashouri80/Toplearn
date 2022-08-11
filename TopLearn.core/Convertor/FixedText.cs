using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.core.Convertor
{
  public  class FixedText
    {
        public static string FixedEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
