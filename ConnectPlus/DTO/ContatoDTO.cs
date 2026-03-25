using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConnectPlus.DTO;

public class ContatoDTO
{
    [Required(ErrorMessage = "O nome do contato é obrigatório.")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "A forma de contato é obrigatório.")]
    public string? FormaContato { get; set; }
    [Required(ErrorMessage = "A imagem do contato é obrigatório.")]
    public IFormFile? Imagem { get; set; }

    [JsonIgnore]
    public string? ImagemUrl { get; set; }
    public Guid? IdTipoContato { get; set; }
}
