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
              var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
              string url = config["SignalRConfig:SignalRUrl"];
              webBuilder.UseUrls(url);
          });