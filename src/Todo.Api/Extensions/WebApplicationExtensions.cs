using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Todo.Api.Extensions;
public static class WebApplicationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection s, IConfiguration cfg)
    {
        var key = Encoding.ASCII.GetBytes(cfg["Jwt:Secret"]!);
        s.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = cfg["Jwt:Issuer"],
                    ValidAudience = cfg["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}