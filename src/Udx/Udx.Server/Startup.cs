using IdentityServer4.AccessTokenValidation;
using LogDashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Udx.Admin;
using Udx.Auth;
using Udx.Cms;
using Udx.Configs;
using Udx.Dbs;
using Udx.Filters;
using Udx.LogUtils;
using Udx.Utils;
namespace Udx.Server;
public class Startup
{
    private static string BasePath => AppContext.BaseDirectory;
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _env;
    private readonly ConfigHelper _configHelper;
    public readonly AppConfig _appConfig;
    private readonly Dictionary<string, DbConfig> _dbConfigs;
    private readonly CacheConfig _cacheConfig;
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
        _configHelper = new ConfigHelper();
        _appConfig = _configHelper.Get<AppConfig>("appconfig", env.EnvironmentName) ?? new AppConfig();
        _dbConfigs = new ConfigHelper().Get<Dictionary<string, DbConfig>>("dbconfig", _env.EnvironmentName);
        _cacheConfig = _configHelper.Get<CacheConfig>("cacheconfig", env.EnvironmentName) ?? new CacheConfig();
     

    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            //options.Providers.Add<CustomCompressionProvider>();
            options.MimeTypes =
                ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "image/svg+xml" });
        });
       
        services.TryAddScoped<IPermissionHandler, PermissionHandler>();
        services.AddSingleton(_appConfig);
        services.AddSingleton(_dbConfigs);
        services.AddSingleton(_cacheConfig);
        //用户信息
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        if (_appConfig.IdentityServer.Enable)
        {
            //is4
            services.AddScoped<IUser, UserIdentiyServer>();
        }
        else
        {
            //jwt
            services.AddScoped<IUser, User>();
        }

        // services.Configure<JsonOptions>(options => options.JsonSerializerOptions.IgnoreNullValues = true);

        //应用配置
        services.AddSingleton(_appConfig);



        #region CORS
        services.AddCors(c =>
        {
            c.AddPolicy("any", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
               ;
            });

            c.AddPolicy("limit", policy =>
            {
                policy.WithOrigins("http://localhost:5201", "https://*:udx.cn")
                .AllowAnyHeader().AllowAnyMethod();
            });
        });

        //services.AddCors();
        #endregion
        #region swaggerGen
        services.AddSwaggerGen(options =>
        {

            //注册Swagger生成器，定义一个和多个Swagger 文档

            options.SwaggerDoc("UdxService", new OpenApiInfo
            {
                Title = "Udx Service",
                Version = "v1",
                Description = "API for UdxService",
                Contact = new OpenApiContact() { Name = "udx-dev", Email = "dev@udx.cn" }
            });
            options.SwaggerDoc("UdxAdmin", new OpenApiInfo { Title = "UdxAdmin Service", Version = "v1", Description = "API for UdxAdmin" });
            options.SwaggerDoc("UdxCms", new OpenApiInfo { Title = "UdxCms Service", Version = "v1" });
            //设置要展示的接口
            options.DocInclusionPredicate((docName, apiDes) =>
            {
                if (!apiDes.TryGetMethodInfo(out System.Reflection.MethodInfo method)) return false;
                var version = method.DeclaringType
                                    .GetCustomAttributes(true)
                                    .OfType<ApiExplorerSettingsAttribute>()
                                    .Select(m => m.GroupName);
                if (docName == "v1" && !version.Any()) return true;
                var actionVersion = method.GetCustomAttributes(true)
                                            .OfType<ApiExplorerSettingsAttribute>()
                                            .Select(m => m.GroupName);
                if (actionVersion.Any()) return actionVersion.Any(v => v == docName);
                return version.Any(v => v == docName);
            });

            options.IncludeXmlComments(Path.Combine(BasePath, "XmlComments\\Udx.Admin.xml"), true);
            options.IncludeXmlComments(Path.Combine(BasePath, "XmlComments\\Udx.Cms.xml"), true);
            //添加Jwt验证设置
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                          {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                          });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Value: Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

        });
        #endregion

        #region 身份认证授权
        var jwtConfig = _configHelper.Get<JwtConfig>("jwtconfig", _env.EnvironmentName);
        services.TryAddSingleton(jwtConfig);

        if (_appConfig.IdentityServer.Enable)
        {
            //is4
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
            })
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = _appConfig.IdentityServer.Url;
                options.RequireHttpsMetadata = false;
                options.SupportedTokens = SupportedTokens.Jwt;
                options.ApiName = "admin.server.api";
                options.ApiSecret = "secret";
            })
            .AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), o => { });
        }
        else
        {
            //jwt
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                //  options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                //  options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
          // .AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), o => { });
        }
        #endregion
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            options.SuppressAsyncSuffixInActionNames = false;
            options.AllowEmptyInputInBodyModelBinding = true;
            options.Filters.Add<UdxExceptionFilter>();
            options.Filters.Add<UdxAuthorizationFilter>();
            // options.Filters.Add<UdxFilter>();
        });

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        //Udx.Admin              
        services.AddUdx(_configuration);
        services.AddUdxDbs(_configuration);
        services.AddUdxAdmin(_configuration);
        services.AddUdxCms(_configuration);

        //阻止Log接收状态消息
        services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);
        services.AddLogDashboard();

        services.AddHealthChecks().AddDbContextCheck<AdminContext>();

        // services.AddHealthChecksUI();
        //Servies
        Udx.DependencyInjection.ServiceProviderManager.Instance = services.BuildServiceProvider();
    }



    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        app.UseResponseCompression();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/UdxService/swagger.json", "UdxService v1");
                c.SwaggerEndpoint("/swagger/UdxAdmin/swagger.json", "UdxAdmin v1");
                c.SwaggerEndpoint("/swagger/UdxCms/swagger.json", "UdxCms v1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            //app.UseReDoc();
        }

        //app.UseHttpsRedirection();
        app.UseFileServer(enableDirectoryBrowsing: false);
        app.UseRouting();
        app.UseCors("any");
        
        app.UseUdx();
        app.UseHealthChecks("/health");
        // app.UseHealthChecksUI();
        app.UseRequestLogContext();
        
        app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest);
        app.UseLogDashboard();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
               name: "default",
               pattern: "{controller=One}/{action=Index}/{id?}");
        });

    }
}