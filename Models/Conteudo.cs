using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padronizei.Models
{
    public enum Visibilidade
    {
        Publico = 0,
        Privado = 1
    }

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
        // Visibilidade: Representa a visibilidade do conteúdo postado. Por exemplo, visibilidade  pública pode ser vista por qualquer usuário do sistema.
        [Required]        
        public Visibilidade Visibilidade{get;set;}
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
