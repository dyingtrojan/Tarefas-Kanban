using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tarefas_Kanban.Models
{
    public class Atividade
    {
        [Key]
        public int IdAtividade { get; set; }
        public int IdUsuario { get; set; }
        public string descricao { get; set; }
        public string nomeSetor { get; set; }
        public string prioridade { get; set; }
        public DateOnly dataCadastro { get; set; }
        public string status { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario usuario { get; set; }
    }
}
