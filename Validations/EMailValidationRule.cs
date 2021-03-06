﻿using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace POP_SF7
{
    class EMailValidationRule : ValidationRule
    {
        Regex regex = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", RegexOptions.IgnoreCase);

        public static string CorrectPattern = " x@x.xxx";

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            String v = value as string;
            if (v != null && regex.Match(v).Success)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Neispravan format e-mail adrese");
        }
    }
}
