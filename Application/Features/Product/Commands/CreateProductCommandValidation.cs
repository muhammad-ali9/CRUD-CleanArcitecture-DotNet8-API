using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is Required.")
                .Length(2, 10)
                .WithMessage("{PropertyName} should be between 2 and 10.");

            RuleFor(x => x.Rate)
                .NotEmpty().
                NotNull()
                .LessThan(600).WithMessage("{PropertyName} should be less than 600.");
        }
    }
}
