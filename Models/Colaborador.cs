using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padronizei.Models
{
    [Table("Colaborador")]
    public class Colaborador
    {
        [Key]
        public int Id{get;set;}
        [Required]

        [StringLength(200)]
        public string Nome{get;set;}
        // Bio: Mensagem de perfil pessoal do colaborador
        public string Bio{get;set;}
        [StringLength(100)]
        public string Matricula{get;set;}
        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email{get;set;}        
        [Required]
        public DateTime DataCriacao{get;set;}    
        [ForeignKey("Departamento")]
        public int DepartamentoId{get;set;}            
    }
}
