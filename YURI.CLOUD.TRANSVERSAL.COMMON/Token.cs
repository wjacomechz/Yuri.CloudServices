using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    public static class Token
    {
        public static string JwtToPayloadUserData(HttpContext httpContext, ref string Usuario, ref string Rol, ref string mensaje)
        {
            try
            {
                string Jwt = httpContext.Request.Headers[HeaderNames.Authorization];
                Jwt = Jwt?.Replace("Bearer", null)?.Trim();
                if (string.IsNullOrEmpty(Jwt)) throw new System.Exception("Token no recibido");
                var tokenSecure = new JwtSecurityTokenHandler().ReadToken(Jwt) as JwtSecurityToken;
                if (tokenSecure.Payload == null ||
                    (!tokenSecure.Payload.ContainsKey("unique_name") |
                    !tokenSecure.Payload.ContainsKey("role") |
                    !tokenSecure.Payload.ContainsKey(ClaimTypes.UserData)))
                    throw new System.Exception($"Payload no válido [{tokenSecure.Payload?.ToString()}]");
                Usuario = tokenSecure.Payload["unique_name"].ToString(); // Usuario interno, externo, credencial cia
                Rol = tokenSecure.Payload["role"].ToString(); // rol = rnc cia
                return tokenSecure.Payload[ClaimTypes.UserData].ToString(); // Información adicional
            }
            catch (System.Exception ex)
            {
                mensaje = ex.Message;
                return null;
            }
        }
    }
}
