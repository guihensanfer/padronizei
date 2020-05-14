using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padronizei.Models
{
    [Table("Conteudo")]
    public class Conteudo
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(150)]
        public string Titulo{get;set;}
        [Required]
        public string Corpo{get;set;}
        [Required]
        public DateTime DataCriacao{get;set;}
        // Colaborador que postou o conteúdo
        [ForeignKey("Colaborador")]
        public int ColaboradorId{get;set;}
        // Departamento que envolve o processo do conteudo postado
        [ForeignKey("Departamento")]
        public int DepartamentoId{get;set;}        
    }
}
