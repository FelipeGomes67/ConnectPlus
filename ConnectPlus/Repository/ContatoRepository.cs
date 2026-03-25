using ConnectPlus.BdContextConnectPlus;
using ConnectPlus.Interface;
using ConnectPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.Repository;

public class ContatoRepository : IContatoRepository
{
    private readonly ConnectPlusContext _context;

    public ContatoRepository(ConnectPlusContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um contato
    /// </summary>
    /// <param name="id">id do contato</param>
    /// <param name="contato">Novos dados do contato</param>
    public void Atualizar(Guid id, Contato contato)
    {
        var contatoBuscado = _context.Contatos.Find(id);

        if(contatoBuscado != null)
        {
            contatoBuscado.Nome = contato.Nome;
            contatoBuscado.FormaContato = contato.FormaContato;
            contatoBuscado.ImagemUrl = contato.ImagemUrl;
            contatoBuscado.IdTipoContato = contato.IdTipoContato;
        }
        _context.SaveChanges();
    }
    /// <summary>
    /// Busca um contato por id
    /// </summary>
    /// <param name="id">id do contato a ser buscado</param>
    /// <returns>objeto do contato com as informações de contato buscado</returns>
    public Contato BuscarPorId(Guid id)
    {
        return _context.Contatos.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo contato
    /// </summary>
    /// <param name="contato">Contato a ser cadastrado</param>
    public void Cadastrar(Contato contato)
    {
        _context.Contatos.Add(contato);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um contato
    /// </summary>
    /// <param name="id">id do contato a ser deletado</param>
    public void Deletar(Guid id)
    {
        var contatoBuscado = _context.Contatos.Find(id);

        if(contatoBuscado != null)
        {
            _context.Contatos.Remove(contatoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de contatos cadastrados
    /// </summary>
    /// <returns>Uma lista de contatos</returns>
    public List<Contato> Listar()
    {
        return _context.Contatos
            .Include(c => c.IdTipoContatoNavigation)
            .ToList();
    }
}
