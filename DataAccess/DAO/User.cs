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
        [Required]
        [StringLength (4, ErrorMessage = "El nombre es requerido.")]
        private string username;
        [Required]
        [StringLength (3, ErrorMessage = "La contraseña es requerida.")]
        private string password;
        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
