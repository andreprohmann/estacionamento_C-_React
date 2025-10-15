namespace minimal_api.dominio.interfaces;
using minimal_api.DTOs;
using minimal_api.dominio.Entidades;

public interface iAdminServices
{
    //Definir os métodos da interface
    admin? Login(LoginDTO loginDTO);
}