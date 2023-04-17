using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EmployeeRecord.Utilities
{
    public static class ValidUser
    {
        public static List<string> DataAnotationsValid(this object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, context, results, true);

            if (isValid)
            {
                return null;
            }

            var errores = results.Select(r => r.ErrorMessage).ToList();
            return errores;
        }

        public static byte[] Base64StringToBytes(this string text)
        {
            return Convert.FromBase64String(text);
        }
    }


}
