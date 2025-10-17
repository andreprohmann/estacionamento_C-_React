
using minimal_api.dominio.Entidades;
using minimal_api.infraestrutura.Db;
using minimal_api.dominio.interfaces;
using Microsoft.EntityFrameworkCore;

namespace minimal_api.dominio.Servicos;
public class veiculosServices: iVeiculosServices
{
    //Injetar o DbContexto
    private readonly DbContexto _contexto;

    //Construtor que recebe o DbContexto para injetar no atributo 
    public veiculosServices(DbContexto contexto)
    {
        _contexto = contexto;
    }

    //Implementar os métodos da interface
    public void Atualizar(veiculo veiculo)
    {
        _contexto.Set<veiculo>().Update(veiculo);
        _contexto.SaveChanges();
    }
    public void Cadastrar(veiculo veiculo)
    {
        _contexto.Set<veiculo>().Add(veiculo);
        _contexto.SaveChanges();
    }
    public veiculo? BuscarPorId(int id)
    {
        return _contexto.Set<veiculo>().Where(v => v.Id == id).FirstOrDefault();
    }
    public void Deletar(veiculo veiculo)
    {
        _contexto.Set<veiculo>().Remove(veiculo);
        _contexto.SaveChanges();
    }
    public List<veiculo> Todos(int page = 1, string? Nome = null, string? Marca = null)
    {
        var query = _contexto.Veiculos.AsQueryable();
        if (!string.IsNullOrEmpty(Nome))
        {
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{Nome.ToLower()}%"));
        }

        int itemsPerPage = 10;

        query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
        
        return query.ToList();
    }
}