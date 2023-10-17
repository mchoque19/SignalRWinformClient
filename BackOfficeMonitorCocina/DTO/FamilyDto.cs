using System.ComponentModel.DataAnnotations;

namespace BackOfficeMonitorCocina.DTO
{
    public class FamilyDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre requerido!")]
        [StringLength(8, ErrorMessage = "La longitud del usuario no puede ser superior a 8.")]
        public string Name { get; set; }

        public FamilyDto()
        {

        } 
        public FamilyDto(int id, string name)
        {
            Id = id ;
            Name = name;
        }
    }
}
