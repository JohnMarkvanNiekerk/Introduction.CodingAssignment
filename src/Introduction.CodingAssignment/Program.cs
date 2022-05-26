// See https://aka.ms/new-console-template for more information
using Introduction.CodingAssignment;
using Introduction.CodingAssignment.Data;
using Introduction.CodingAssignment.OutputWriter;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

Console.WriteLine("Starting Container...");
try
{
    // Create a DI contaner 
    var services = new ServiceCollection();
    ConfigureServices(services);

    Console.WriteLine("Registering Services..");
    // Register services in Singleton
    void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<IDataReader, DataReader>();
        services.AddSingleton<IProcessor, Processor>();
        services.AddSingleton<IOutputWriter, OutputWriter>();
    }
    //Get the processor and start the work
    var provider = services.BuildServiceProvider();
    var processor = provider.GetService<IProcessor>();
    bool process = true;
    while (process)
    {

        Console.Clear();
        Console.WriteLine("Welcome! Please select an option by typing the number and pressing enter");
        Console.WriteLine("1: Load files for processing.");
        Console.WriteLine("2: Use the default data.");

        try
        { 
           var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter a path to the user.txt file");
                    var userFile = Console.ReadLine();
                    Console.WriteLine("Enter a path to the tweet.txt file");
                    var tweetFile = Console.ReadLine();
                    await processor.ProcessAsync(userFile, tweetFile);
                    break;
                case "2":
                    await processor.ProcessAsync();
                    break;
                default:
                    break;
            }
            Console.WriteLine("To start processing with the default data provided, press enter");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            process = false;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    Console.WriteLine("Press any key to continue..");
    Console.ReadKey();
}
