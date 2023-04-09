using Application.Application;
using Domain.Services;
using Infrastructure.Repositories;
using System;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Digite o caminho do arquivo:");
            string path = Console.ReadLine();

            Console.WriteLine("Processando todos os arquivos .csv do local especificado...");

            var files = Directory.GetFiles(path, "*.csv");

            foreach (var file in files)
            {
                var _folhaDePontoRepo = new FolhaDePontoRepository(file);
                var _folhaDePontoService = new FolhaDePontoService(_folhaDePontoRepo);
                var _folhaDePontoApp = new FolhaDePontoApp(_folhaDePontoService);

                var folhaDePonto = await _folhaDePontoApp.GetAllAsync();
            }


            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }



    }
}