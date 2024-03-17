using Workshop.Api;
using Workshop.Api.Bll.Services;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories;
using Workshop.Api.Dal.Repositories.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
        builder.Build().Run();
    }
}