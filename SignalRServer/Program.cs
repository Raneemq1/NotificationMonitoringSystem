using SignalRServer;

CreateHostBuilder(args).Build().Run();
static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureLogging(logging =>
          {
              logging.ClearProviders();
              logging.AddConsole();
          })
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder.ConfigureServices(services =>
              {
                  services.AddSignalR();
              });

              webBuilder.Configure(app =>
              {
                  app.UseRouting();
                  app.UseEndpoints(endpoints =>
                  {

                      endpoints.MapHub<AlertHub>("/AlertHub");
                  });
              });
              var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
              string url = config["SignalRConfig:SignalRUrl"];
              webBuilder.UseUrls(url);
          });