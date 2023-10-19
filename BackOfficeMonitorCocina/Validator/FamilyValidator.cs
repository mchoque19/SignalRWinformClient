using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BackOfficeMonitorCocina.DTO
{
    public class FamilyValidator : AbstractValidator<DAL.DAO.Family>
    {

        public FamilyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DAL.DAO.Family>.CreateWithOptions((DAL.DAO.Family)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
            
}
