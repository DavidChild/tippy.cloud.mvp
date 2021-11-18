using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.IO;
using tippy.cloud.Server;

namespace tippy.publish.monome
{
    public class Startup
    {
       private readonly  IHostEnvironment _env = null;
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
           
            Configuration = configuration;
            _env = env;
        }
        private readonly string apiName = "Tippy Service";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddHttpContextAccessor();
            services.AddControllersWithViews().AddJsonOptions(p =>
            {
                //���д���Ƿ����շ������ֶ�
                //p.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                //���д����ԭ�������ֶ�
                p.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //����ע�����
            var businessItems = GetClassName("tippy.cloud.server.logic");
            foreach (var item in businessItems)
            {
                if (item.Value == null || item.Key == null) continue;
                services.AddScoped(item.Value, item.Key);
            }

            services.AddControllersWithViews();
            services.AddAuthService(Configuration);
          
            services.AddRazorPages();
            if(_env.IsDevelopment())
            {

                services.AddSwaggerGen(s =>
                {
                    s.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",//�汾��
                        Title = $"{apiName} Document��DotnetCore 5.0",//�༭����
                                                                     //Description = $"{apiName} HTTP API V1",//�༭����
                                                                     //Contact = new OpenApiContact { Name = apiName, Email = "2334344234@163.com" },//�༭��ϵ��ʽ
                        License = new OpenApiLicense { Name = apiName }//�༭���֤
                    });
                    s.OrderActionsBy(o => o.RelativePath);
                    var xmlPath = Path.Combine(_env.ContentRootPath, "tippy.publish.monome.xml");// linux���ִ�Сд
                    s.IncludeXmlComments(xmlPath, true); // �ѽӿ��ĵ���·�����ý�ȥ���ڶ���������ʾ�����Ƿ���������Controller��ע�����ɡ��ڶ����������Բ��ӡ�


                    //�������token��֤����ط�
                    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������{token}\"",
                        Name = "token",//jwtĬ�ϵĲ�������
                        In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                        Type = SecuritySchemeType.ApiKey
                    });

                    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {new OpenApiSecurityScheme
                            {
                                Reference=new OpenApiReference()
                                {
                                    Id="Bearer",
                                    Type=ReferenceType.SecurityScheme
                                }
                            },Array.Empty<string>() }
                        });
                });
            }
         
        }


            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "");
                });
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
           
        }
        /// <summary>  
        /// ��ȡ�����е�ʵ�����Ӧ�Ķ���ӿ�
        /// </summary>  
        /// <param name="assemblyName">����</param>
        public Dictionary<Type, Type> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces().Where(p => p.Name != "IBaseServices").FirstOrDefault();
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type>();
        }
    }
}
