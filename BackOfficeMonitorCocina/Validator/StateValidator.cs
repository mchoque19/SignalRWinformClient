using FluentValidation;

namespace BackOfficeMonitorCocina.Validator
{
    public class StateValidator : AbstractValidator<DAL.DAO.State>
    {

        public StateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido!");
            RuleFor(x => x.Color).NotEmpty().WithMessage("El color es requerido!");
            RuleFor(x => x.Order).NotEmpty().WithMessage("El numero de orden es requerido!");
            RuleFor(x => x.Marchando).NotEmpty().WithMessage("Es requerido!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DAL.DAO.State>.CreateWithOptions((DAL.DAO.State)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
 
}
