using System.ComponentModel.DataAnnotations;

namespace Tarefas_Kanban.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string nome { get; set; }

        public string email { get; set; }
    }
}
