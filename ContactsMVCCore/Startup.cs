using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ContactsMVCCore.Context;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ContactsMVCCore;


public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var con = Configuration.GetConnectionString("DBConnection");

        services.AddAuthorization();
        services.AddSession();  
        services.AddDistributedMemoryCache();

        services.AddControllers().AddNewtonsoftJson(opts =>
        {
            opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
            opts.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        }); 
        services.AddDbContext<ContactsDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DBConnection")));

    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseAuthorization();
        app.UseAuthentication();
        app.UseSession();
        app.MapControllers();
        app.Run();
    }
}