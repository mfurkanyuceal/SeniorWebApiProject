using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SeniorWepApiProject
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseUrls("http://172.31.84.140:5000");
                    //webBuilder.UseUrls("http://localhost:5000");
                    webBuilder.UseStartup<Startup>();
                });
    }
}