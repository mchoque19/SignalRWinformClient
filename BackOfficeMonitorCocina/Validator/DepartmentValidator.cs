using DAL.DTO;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BackOfficeMonitorCocina.DTO
{
    public class DepartmentValidator : AbstractValidator<DAL.DAO.Department>
    {
       
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido!");
            RuleFor(x => x.Family).NotEmpty().WithMessage("La familia   es requerido!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DAL.DAO.Department>.CreateWithOptions((DAL.DAO.Department)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
