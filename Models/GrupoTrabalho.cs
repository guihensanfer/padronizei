using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padronizei.Models
{
    [Table("GrupoTrabalho")]
    public class GrupoTrabalho
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(80)]
        public string Nome{get;set;}
        [Required]
        [StringLength(200)]
        public string Descricao{get;set;}
        public DateTime DataCriacao{get;set;}
        [ForeignKey("Organizacao")]
        public int OrganizacaoId{get;set;}
        [Required]
        [ForeignKey("Colaborador")]
        public int ColaboradorId{get;set;}        
    }
}
