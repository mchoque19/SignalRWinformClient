using DAL.DTO;
using System.ComponentModel.DataAnnotations;

namespace BackOfficeMonitorCocina.DTO
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre requerido!")]
        [StringLength(8, ErrorMessage = "La longitud del usuario no puede ser superior a 8.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value f .")]
        //[Required(ErrorMessage = "Familia requerido!"), ]
        public int idFamily { get; set; }


        [Required(ErrorMessage = "Familia requerido!"), ]
        public FamilyDto prueba { get; set; }

    }
}
