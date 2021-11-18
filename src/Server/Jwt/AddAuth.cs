
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tippy.cloud.Server
{
    public static class AddAuthServices
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = JsonConvert.DeserializeObject<JwtOption>(Bitter.Base.NetCore.ConfigManage.JsonConfigMange.GetInstance().AppSettings["JWT"].ToString());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwt.Issuer,
                            ValidAudience = jwt.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                            RequireExpirationTime = true
                        };
                    });

            
            return services;
        }

        
    }
}
