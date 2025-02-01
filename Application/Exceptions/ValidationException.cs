using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public List<string> ErrorList { get; set; }
        public ValidationErrorException() : base("One or More Exception Occured.")
        {
            ErrorList = new List<string>();
            
        }

        public ValidationErrorException(List<ValidationFailure> failures) : this()
        {
            foreach(var failure in failures)
            {
                ErrorList.Add(failure.ErrorMessage);
            }
            
        }
    }
}
