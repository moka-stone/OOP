using Microsoft.Extensions.DependencyInjection;
using RecordManager.Application.Services;
using RecordManager.Domain.Interfaces;
using RecordManager.Infrastructure.External;
using RecordManager.Infrastructure.Repositories;
using RecordManager.Presentation;

var services = new ServiceCollection();

// Register services
services.AddSingleton<IStudentRepository, StudentRepository>();
services.AddSingleton<QuoteApiAdapter>();
services.AddSingleton<StudentService>();
services.AddSingleton<ConsoleUI>();

var serviceProvider = services.BuildServiceProvider();

// Run the application
var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();
await consoleUI.Run();
