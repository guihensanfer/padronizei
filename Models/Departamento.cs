using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padronizei.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(100)]
        public string Nome{get;set;}
        [Required]
        [ForeignKey("Organizacao")]
        public int OrganizacaoId{get;set;}
    }
}
