using ConnectPlus.Models;

namespace ConnectPlus.Interface;

public interface ITipoContatoRepository
{
    void Cadastrar(TipoContato tipoContato);
    void Deletar(Guid id);
    List<TipoContato> Listar();
    TipoContato BuscarPorId(Guid id);
    void Atualizar(Guid id, TipoContato tipoContato);
}
