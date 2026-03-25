using ConnectPlus.DTO;
using ConnectPlus.Interface;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private readonly ITipoContatoRepository _tipoContatoRepository;

    public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
    {
        _tipoContatoRepository = tipoContatoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de lista os tipos de contato
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de contato</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoContatoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de buscar um tipo de contato específico
    /// </summary>
    /// <param name="id">id do tipo de contato buscado</param>
    /// <returns>Status code 200 e o tipo de contato</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoContatoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de cadastrar um novo tipo de contato
    /// </summary>
    /// <param name="tipoContato">Tipo de contato a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de contato cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoContatoDTO tipoContato)
    {
        try
        {
            var novoTipoContato = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };

            _tipoContatoRepository.Cadastrar(novoTipoContato);
            return StatusCode(201, novoTipoContato);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de atualizar um tipo de contato
    /// </summary>
    /// <param name="id">Id do tipo contato a ser atualizado</param>
    /// <param name="tipoContato">Tipo contato com os dados atualizados</param>
    /// <returns>Status code 204 e o tipo de contato atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContato)
    {
        try
        {
            var tipoContatoAtualizado = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };
            _tipoContatoRepository.Atualizar(id, tipoContatoAtualizado);
            return StatusCode(204, tipoContatoAtualizado);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de deletar um tipo de contato
    /// </summary>
    /// <param name="id">Id do tipo contato a ser excluido</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoContatoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
