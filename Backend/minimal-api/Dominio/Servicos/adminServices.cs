using minimal_api.dominio.interfaces;
using minimal_api.DTOs;
using minimal_api.dominio.Entidades;
using minimal_api.infraestrutura.Db;

namespace minimal_api.dominio.Servicos;
public class adminServices: iAdminServices
{
    //Injetar o DbContexto
    private readonly DbContexto _contexto;

    //Construtor que recebe o DbContexto
    public adminServices(DbContexto contexto)
    {
        _contexto = contexto;
    }
    
    //Implementar os métodos da interface
    public admin? Login(LoginDTO loginDTO)
    {
        //Verificar se o email e a senha existem no banco de dados
        var adm = _contexto.Admins.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
        return adm;        
        
    }
}