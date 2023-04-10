using Application.Application;
using Domain.Services;
using Entity.Entities;
using Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Application.Models;
using System.Text.Json;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Digite o caminho do arquivo:");
            string path = Console.ReadLine();

            Console.WriteLine("O caminho do arquivo digitado foi: " + path);

            var files = Directory.GetFiles(path, "*.csv");

            var listaFolhaPagamento = new List<FolhaDePagamento>();

            await Task.WhenAll(files.Select(async file =>
            {
                try
                {
                    var _folhaDePontoRepo = new FolhaDePontoRepository(file);
                    var _folhaDePontoService = new FolhaDePontoService(_folhaDePontoRepo);
                    var _folhaDePontoApp = new FolhaDePontoApp(_folhaDePontoService);

                    var folhaDePonto = await _folhaDePontoApp.GetAllAsync();

                    var _folhaDePagamentoApp = new FolhaDePagamentoApp(folhaDePonto);


                    var folhaDePagamento = await _folhaDePagamentoApp.ProcessarFolhasDePonto(file);

                    listaFolhaPagamento.Add(folhaDePagamento);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao processar folhas de ponto. \n" + ex.Message);
                    throw;
                }
                
            }));

            try
            {
                var filePathJson = path + "/FolhaDePagamento.json";

                var opcoes = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                var json = JsonSerializer.Serialize(listaFolhaPagamento, opcoes);

                using (var sw = new StreamWriter(filePathJson, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(json);
                }

                Console.WriteLine("Arquivo salvo com sucesso em " + filePathJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar arquivo json.\n" + ex.Message);
                throw;
            }
            

            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }



    }
}