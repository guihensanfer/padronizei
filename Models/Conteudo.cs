using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Padronizei.Models
{    
    public enum Visibilidade
    {
        [Display(Name="Público")]
        Publico = 0,
        [Display(Name="Privado", Description="Somente colaboradores da mesma organização poderão ver.")]
        Privado = 1
    }

    [Table("Conteudo")]
    public class Conteudo
    {        
        [Key]
        public int Id{get;set;}
        [Required]
        [Display(Name="Título")]
        [StringLength(150)]
        public string Titulo{get;set;}
        [Required]        
        public string Corpo{get;set;}
        // Visibilidade: Representa a visibilidade do conteúdo postado. Por exemplo, visibilidade pública pode ser vista por qualquer usuário do sistema.
        [Required]                
        public Visibilidade Visibilidade{get;set;}
        [Required]
        [Display(Name="Data de criação")]
        [DataType(DataType.Date)]
        public DateTime DataCriacao{get;set;}        
        // Colaborador que postou o conteúdo
        [Required]
        [Display(Name="Colaborador dono do conteúdo")]                
        public int ColaboradorId{get;set;}
        public Colaborador Colaborador{get;set;}
        // Departamento que envolve o processo do conteudo postado
        [Display(Name="Departamento relacionado")]                
        public int DepartamentoId{get;set;}        
        public Departamento Departamento{get;set;}           
    }
}
