using RGP.GestaoTarefas.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RGP.GestaoTarefas.Data.Repository
{
    public static class UsuarioInitializer
    {
        public static void Initialize(RgpDbContext context)
        {
            if (!context.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario { Id = "1", Nome = "João Silva" },
                    new Usuario { Id = "2", Nome = "Ana Silva" },
                    new Usuario { Id = "3", Nome = "Rodrigo Pereira"}
                };

                context.Usuarios.AddRange(usuarios);
                context.SaveChanges();
            }
        }
    }
}
