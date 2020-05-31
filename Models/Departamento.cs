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
        [Display(Name="Organização")]        
        public int OrganizacaoId{get;set;}            
        public Organizacao Organizacao{get;set;}
    }
}
