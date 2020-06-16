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
        [Display(Name="Descrição")]
        public string Descricao{get;set;}
        [DataType(DataType.Date)]
        [Display(Name="Data de criação")]
        public DateTime DataCriacao{get;set;}
        [Display(Name="Organização")]        
        public int OrganizacaoId{get;set;}
        public Organizacao Organizacao{get;set;}
        [Required]        
        [Display(Name="Colaborador responsável")]
        public int ColaboradorId{get;set;}        
        public Colaborador Colaborador{get;set;}
    }
}
