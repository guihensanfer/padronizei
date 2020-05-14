using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padronizei.Models
{
    [Table("Organizacao")]
    public class Organizacao
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(80)]
        public string Nome{get;set;}        
        [StringLength(15)]
        public string CNPJ{get;set;}
        public DateTime DataCriacao{get;set;}
    }
}
