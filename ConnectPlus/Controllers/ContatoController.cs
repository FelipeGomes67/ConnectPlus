using ConnectPlus.DTO;
using ConnectPlus.Interface;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoRepository _contatoRepository;

    public ContatoController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }


    /// <summary>
    /// Endpoint da API que faz a chamada para o método de lista os contatos
    /// </summary>
    /// <returns>Status code 200 e a lista de contatos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_contatoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de buscar um contato específico
    /// </summary>
    /// <param name="id">id do contato buscado</param>
    /// <returns>Status code 200 e o contato</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_contatoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de cadastrar um novo contato
    /// </summary>
    /// <param name="contato">Contato a ser cadastrado</param>
    /// <returns>Status code 201 e o contato cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromForm] ContatoDTO contato)
    {
        try
        {
            string nomeArquivo = "padrao.png"; // Nome inicial

            if (contato.Imagem != null && contato.Imagem.Length > 0)
            {
                var extensao = Path.GetExtension(contato.Imagem.FileName);
                nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = Path.Combine("wwwroot", "imagens");
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await contato.Imagem.CopyToAsync(stream);
                }
            }

            var novoContato = new Contato
            {
                Nome = contato.Nome!,
                FormaContato = contato.FormaContato!,
                IdTipoContato = contato.IdTipoContato!.Value,
                ImagemUrl = "/Imagens/" + nomeArquivo
            };

            _contatoRepository.Cadastrar(novoContato);
            return StatusCode(201, novoContato);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de atualizar um contato
    /// </summary>
    /// <param name="id">Id do contato a ser atualizado</param>
    /// <param name="contato">Contato com os dados atualizados</param>
    /// <returns>Status code 204 e o contato atualizado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromForm] ContatoDTO contato)
    {
        try
        {
            var contatoExistente = _contatoRepository.BuscarPorId(id);
            if (contatoExistente == null) return NotFound();

            if (contato.Imagem != null && contato.Imagem.Length > 0)
            {
                if (!string.IsNullOrEmpty(contatoExistente.ImagemUrl))
                {
                    var caminhoAntigo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", contatoExistente.ImagemUrl.TrimStart('/'));
                    if (System.IO.File.Exists(caminhoAntigo))
                    {
                        System.IO.File.Delete(caminhoAntigo);
                    }
                }

                var extensao = Path.GetExtension(contato.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
                var caminhoNovo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens", nomeArquivo);

                using (var stream = new FileStream(caminhoNovo, FileMode.Create))
                {
                    await contato.Imagem.CopyToAsync(stream);
                }

                contatoExistente.ImagemUrl = "/imagens/" + nomeArquivo;
            }

            contatoExistente.Nome = contato.Nome!;
            _contatoRepository.Atualizar(id, contatoExistente);

            return Ok(contatoExistente);
        }
        catch (IOException ioEx)
        {
            return BadRequest($"Erro de arquivo: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro geral: {ex.Message}");
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de deletar um contato
    /// </summary>
    /// <param name="id">Id do contato a ser excluido</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            var contato = _contatoRepository.BuscarPorId(id);
            if (contato == null) return NotFound();

            if (!string.IsNullOrEmpty(contato.ImagemUrl) && contato.ImagemUrl != "/imagens/padrao.png")
            {
                var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", contato.ImagemUrl.TrimStart('/'));

                if (System.IO.File.Exists(caminhoArquivo))
                {
                    System.IO.File.Delete(caminhoArquivo);
                }
            }

            _contatoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}
