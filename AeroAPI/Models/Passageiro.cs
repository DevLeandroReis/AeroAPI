using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AeroAPI.Models
{
    public class Passageiro
    {      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "Tamanho máximo de caracteres permitido para 'Nome' = 50")]
        public string Nome { get; set; }

        public int Idade { get; set; }

        [MaxLength(20, ErrorMessage = "Tamanho máximo de caracteres permitido para 'Celular' = 20")]
        public string Celular { get; set; }
    }
}
