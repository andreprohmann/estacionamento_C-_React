using minimal_api.DTOs;
using minimal_api.dominio.Entidades;
using minimal_api.Migrations;

namespace minimal_api.dominio.interfaces;
public interface iVeiculosServices
{
    //Definir os métodos da interface
    List<veiculo> Todos(int page, string? Nome = null, string? Marca = null);
    veiculo? BuscarPorId(int id);
    void Cadastrar(veiculo veiculo);
    void Atualizar(int id, veiculo veiculo);
    void Deletar(veiculo veiculo);
    
}