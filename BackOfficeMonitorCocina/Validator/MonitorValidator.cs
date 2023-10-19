using FluentValidation;

namespace BackOfficeMonitorCocina.Validator
{
    public class MonitorValidator : AbstractValidator<DAL.DAO.Monitor>
    {

        public MonitorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DAL.DAO.Monitor>.CreateWithOptions((DAL.DAO.Monitor)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
