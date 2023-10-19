using FluentValidation;

namespace BackOfficeMonitorCocina.Validator
{
    public class LoginValidator : AbstractValidator<DAL.DAO.User>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("El usuario es requerido!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es requerida!");

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DAL.DAO.User>.CreateWithOptions((DAL.DAO.User)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
