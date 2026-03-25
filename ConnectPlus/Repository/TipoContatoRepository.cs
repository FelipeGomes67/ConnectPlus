using ConnectPlus.BdContextConnectPlus;
using ConnectPlus.Interface;
using ConnectPlus.Models;

namespace ConnectPlus.Repository;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly ConnectPlusContext _context;

    public TipoContatoRepository(ConnectPlusContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um tipo de contato
    /// </summary>
    /// <param name="id">id do tipo contato</param>
    /// <param name="tipoContato">Novos dados do tipo contato</param>
    public void Atualizar(Guid id, TipoContato tipoContato)
    {
        var tipoContatoBuscado = _context.TipoContatos.Find(id);

        if (tipoContatoBuscado != null)
        {
            tipoContatoBuscado.Titulo = tipoContato.Titulo;
        }
        _context.SaveChanges();

    }

    /// <summary>
    /// Busca um tipo de contato por id
    /// </summary>
    /// <param name="id">id do tipo contato a ser buscado</param>
    /// <returns>objeto do tipocontato com as informações de tipo contato buscado</returns>
    public TipoContato BuscarPorId(Guid id)
    {
        return _context.TipoContatos.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de contato
    /// </summary>
    /// <param name="tipoContato">Tipo de contato a ser cadastrado</param>
    public void Cadastrar(TipoContato tipoContato)
    {
        _context.TipoContatos.Add(tipoContato);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um tipo de contato
    /// </summary>
    /// <param name="id">id do tipo de contato a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoContatoBuscado = _context.TipoContatos.Find(id);

        if (tipoContatoBuscado != null)
        {
            _context.TipoContatos.Remove(tipoContatoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de tipos de contato cadastrados
    /// </summary>
    /// <returns>Uma lista de tipo contatos</returns>
    public List<TipoContato> Listar()
    {
        return _context.TipoContatos.ToList();
    }
}
