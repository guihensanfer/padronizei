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
        [StringLength(150)]
        public string Bio{get;set;}
        [Display(Name = "Matrícula")]
        [StringLength(100)]
        public string Matricula{get;set;}
        [Required]
        [EmailAddress]
        [StringLength(200)]
        [Display(Name="E-mail")] 
        public string Email{get;set;}                
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Data de criação")]           
        public DateTime DataCriacao{get;set;}    
        [Display(Name="Departamento")]
        [ForeignKey("Departamento")]        
        public Departamento DepartamentoRelacionado{get;set;}                 
    }
}
