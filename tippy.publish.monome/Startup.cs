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
                //这个写法是返回驼峰命名字段
                //p.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                //这个写法按原样返回字段
                p.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //集中注册服务
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
                        Version = "v1",//版本号
                        Title = $"{apiName} Document—DotnetCore 5.0",//编辑标题
                                                                     //Description = $"{apiName} HTTP API V1",//编辑描述
                                                                     //Contact = new OpenApiContact { Name = apiName, Email = "2334344234@163.com" },//编辑联系方式
                        License = new OpenApiLicense { Name = apiName }//编辑许可证
                    });
                    s.OrderActionsBy(o => o.RelativePath);
                    var xmlPath = Path.Combine(_env.ContentRootPath, "tippy.publish.monome.xml");// linux区分大小写
                    s.IncludeXmlComments(xmlPath, true); // 把接口文档的路径配置进去。第二个参数表示的是是否开启包含对Controller的注释容纳【第二个参数可以不加】


                    //启用添加token验证输入地方
                    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入{token}\"",
                        Name = "token",//jwt默认的参数名称
                        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
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
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
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
