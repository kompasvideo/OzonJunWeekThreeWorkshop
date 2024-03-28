using Workshop.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
        builder.Build().Run();
    }
}