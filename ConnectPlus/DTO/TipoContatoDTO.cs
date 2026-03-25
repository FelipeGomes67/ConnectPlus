using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO;

public class TipoContatoDTO
{
    [Required(ErrorMessage = "O título do tipo contato é obrigatório.")]
    public string? Titulo { get; set; }
}
