using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Database;

namespace UsuariosAPI.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime DataNascimento { get; set; }
        public Usuario() : base() { }
    }
}
