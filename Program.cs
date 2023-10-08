// See https://aka.ms/new-console-template for more information
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using as2cs_home.Reader;
class as2cs {

static async Task Main(string[] args){

    using(var loggerFactory = LoggerFactory.Create(builder => {
        builder
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("System", LogLevel.Warning)
            .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
            .AddConsole();
    }));

    


     var host = CreateHostBuilder(args).Build();

    var source = args[0];
    var destination = args[1];

    var asConverter = host.Services.GetRequiredService<ASConverter>();
    asConverter.setSource(source);
    asConverter.setDestination(destination);
    //logger.LogInformation("Starting to read files from : " + source);
    asConverter.startReading();
    asConverter.startConverting().Wait();

    
    
}

static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<ASConverter>();
        })
        .ConfigureLogging((_, logging) => 
        {
            logging.ClearProviders();
            logging.AddSimpleConsole(options => options.IncludeScopes = true);
            //logging.AddEventLog();
        });
}