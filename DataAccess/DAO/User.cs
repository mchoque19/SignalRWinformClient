using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class User
    {
        private int id;
 
        private string username;
 
        private string password;
        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage = "Usuario requerido!")]
        [StringLength(8, ErrorMessage = "La longitud del usuario no puede ser superior a 8.")]
        public string Username { get => username; set => username = value; }
        [Required(ErrorMessage = "Contraseña requerida!")]
        [StringLength(3, ErrorMessage = "La contraseña debe tener al menos 3 caracteres")]
        public string Password { get => password; set => password = value; }
    }
}
