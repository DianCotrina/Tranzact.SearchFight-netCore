using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform.SearchFight.Service.Engine;
using Platform.SearchFight.Service.Engine.Config;
using Platform.SearchFight.Service.Engine.Converter;
using Platform.SearchFight.Service.Engine.Interfaces;
using Platform.SearchFight.Service.Engine.Model;
using Platform.SearchFight.Service.Engine.Processor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Platform.SearchFight.Service.Engine.Processor.Interfaces;

namespace Platform.SearchFight
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunAsync(args).GetAwaiter().GetResult();
            Console.WriteLine("\n");
        }

        private static async Task RunAsync(string[] args)
        {
            var host = AppStartup(args).Build();
            var searchFightProcessor = host.Services.GetRequiredService<SearchFightProcessor>();

            List<string> searchTopics = new List<string> { ".net", "python" };

            await searchFightProcessor.CallSearchFightService(searchTopics);
            searchFightProcessor.Reports.ForEach(r => Console.WriteLine(r));

            Console.WriteLine("\n");
            Console.ReadLine();

        }

        private static IHostBuilder AppStartup(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            //dependency injection container
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<SearchFightProcessor>();
                    services.AddTransient<IGroupListConverter, GroupListConverter>();
                    services.AddTransient<ISearchReportProcessor, SearchReportProcessor>();
                    services.AddTransient<IWinnerProcessor, WinnerProcessor>();
                    services.Configure<SearchGoogleConfig>(configuration.GetSection("GoogleSearchEngine"));
                    services.Configure<SearchBingConfig>(configuration.GetSection("BingSearchEngine"));
                    services.AddSingleton<ISearchEngine, GoogleSearchEngine>();
                    services.AddSingleton<ISearchEngine, BingSearchEngine>();
                    services.AddSingleton<ISearchTopicProcessor, SearchTopicProcessor>();
                });
        }

    }
}