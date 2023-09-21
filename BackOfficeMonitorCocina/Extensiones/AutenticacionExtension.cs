using Blazored.SessionStorage;
using DAL.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BackOfficeMonitorCocina.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {   private readonly ISessionStorageService _sessionStoarge;        
        private ClaimsPrincipal _sinIformacion = new ClaimsPrincipal(new ClaimsIdentity());
       
        public AutenticacionExtension(ISessionStorageService sessionStorage)
        {
            _sessionStoarge = sessionStorage;
        }

        //guardar informacion 
        public async Task ActualizarEstadoAutenticacion(SessionDto? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;
            
            if(sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.username)
                }, "JwtAuth"));

                await _sessionStoarge.GuardarStorage("sesionUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sinIformacion;
                await _sessionStoarge.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        //devolver la informacion 
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()

        {
            var sesionUsuario = await _sessionStoarge.ObtenerStorage<DAL.DTO.SessionDto>("sesionUsuario");

            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinIformacion));

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                   new Claim(ClaimTypes.Name,sesionUsuario.username)

                }, "JwtAuth"));
            return await Task.FromResult(new AuthenticationState(claimPrincipal));
    

        }
         
        
    } 
    
}
