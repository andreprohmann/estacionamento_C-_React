using minimal_api.DTOs;
using minimal_api.dominio.Entidades;

namespace minimal_api.dominio.interfaces;
public interface iAdminServices
{
    //Definir os métodos da interface
    admin? Login(LoginDTO loginDTO);
}