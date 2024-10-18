using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace INTITUTO1.Client.Auth
{
    public class AuthStateProviderFalso : AuthenticationStateProvider
    {
        private ClaimsPrincipal _usuario = new ClaimsPrincipal();

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
            // Retorna el estado de autenticación basado en el usuario actual
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(_usuario)));
        }

        // Método para loguear al usuario
        public void Login(string usuario)
        {
            // Crear los claims del usuario autenticado
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario),
            new Claim(ClaimTypes.Role, "User") // Puedes agregar más claims aquí
        };

            // Crear el ClaimsIdentity con los claims
            var identity = new ClaimsIdentity(claims, "apiauth_type");
            _usuario = new ClaimsPrincipal(identity);

            // Notificar que el estado de autenticación ha cambiado
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // Método para cerrar la sesión del usuario
        public void Logout()
        {
            // Eliminar la identidad del usuario
            _usuario = new ClaimsPrincipal(new ClaimsIdentity());

            // Notificar que el estado de autenticación ha cambiado
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

}
