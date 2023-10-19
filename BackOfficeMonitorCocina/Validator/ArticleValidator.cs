using FluentValidation;

namespace BackOfficeMonitorCocina.Validator
{
    public class ArticleValidator : AbstractValidator<DAL.DAO.Article>
    {

        public ArticleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del articulo es requerido!");

            RuleFor(x => x.Department).NotEmpty().WithMessage("El departamento es requerido!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DAL.DAO.Article>.CreateWithOptions((DAL.DAO.Article)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
