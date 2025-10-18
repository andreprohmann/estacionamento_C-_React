using minimal_api.DTOs;
using minimal_api.dominio.Entidades;

namespace minimal_api.dominio.interfaces;
public interface iAdminServices
{
    object BuscarPorEmail(string email);

    //Definir os métodos da interface
    admin? Login(LoginDTO loginDTO);
}