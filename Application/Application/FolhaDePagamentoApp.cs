using Application.Application.Interfaces;
using Application.Models;
using Domain.Repositories.Interfaces;
using Domain.Services;
using Domain.Services.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application
{
    public class FolhaDePagamentoApp : IFolhaDePagamentoApp
    {
        private readonly List<FolhaDePonto> _folhaDePontos;
        public FolhaDePagamentoApp(List<FolhaDePonto> folhaDePontos)
        {
            _folhaDePontos = folhaDePontos;
        }

        public async Task<FolhaDePagamento> ProcessarFolhasDePonto(string file)
        {
            var departamento = file.Split('-')[0].Split('\\')[^1];
            var mesVigencia = file.Split('-')[1].Split('\\')[^1];
            var anoVigencia = int.Parse(file.Split('-')[2].Split('.')[0].Split('\\')[^1]);

            double totalDescontosDepartamento = 0;
            double totalExtrasDepartamento = 0;

            var funcionarios = new List<Funcionario>();

            var registrosFuncionarios = _folhaDePontos.GroupBy(x => x.Codigo);

            await Task.WhenAll(registrosFuncionarios.Select(async registros =>
            {
                var pontosFuncionario = registros.ToList();

                double totalReceber = 0;
                double horasExtras = 0;
                double horasDebito = 0;
                var diasExtras = 0;
                var diasTrabalhados = 0;

                var funcionario = new Funcionario();
                funcionario.Codigo = pontosFuncionario[0].Codigo;
                funcionario.Nome = pontosFuncionario[0].Nome;
                var valorHora = Double.Parse(pontosFuncionario[0].ValorHora.Replace("R$ ", "").Replace(" ", ""));

                var mesInt = DateTime.ParseExact(mesVigencia, "MMMM", new CultureInfo("pt-BR")).Month;
                var diasUteis = GetDiasUteisNoMes(anoVigencia, mesInt);

                await Task.WhenAll(pontosFuncionario.Select( async ponto =>
                {
                    var data = DateTime.ParseExact(ponto.Data, "dd/MM/yyyy", new CultureInfo("pt-BR"));
                    var entrada = DateTime.ParseExact(ponto.Entrada, "HH:mm:ss", new CultureInfo("pt-BR"));
                    var saida = DateTime.ParseExact(ponto.Saida, "HH:mm:ss", new CultureInfo("pt-BR"));
                    var inicioAlmoco = DateTime.ParseExact(ponto.Almoco.Split(" - ")[0], "HH:mm", new CultureInfo("pt-BR"));
                    var fimAlmoco = DateTime.ParseExact(ponto.Almoco.Split(" - ")[1], "HH:mm", new CultureInfo("pt-BR"));
                    var horasTrabalhadasDia = (saida - entrada) - (fimAlmoco - inicioAlmoco);
                    var valorHoraDia = Double.Parse(ponto.ValorHora.Replace("R$ ", "").Replace(" ", ""));

                    if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                    {
                        diasExtras += 1;
                        horasExtras += horasTrabalhadasDia.TotalHours;
                        totalExtrasDepartamento += (horasTrabalhadasDia.TotalHours * valorHoraDia);
                    }
                    else
                    {
                        diasTrabalhados += 1;
                        if (horasTrabalhadasDia.TotalHours - 8 >= 0)
                        {
                            totalExtrasDepartamento += ((horasTrabalhadasDia.TotalHours - 8) * valorHoraDia);
                            horasExtras += horasTrabalhadasDia.TotalHours - 8;
                        }
                        else
                        {
                            totalDescontosDepartamento += ((8 - horasTrabalhadasDia.TotalHours) * valorHoraDia);
                            horasDebito += 8 - horasTrabalhadasDia.TotalHours;
                        }
                    }
                    totalReceber += (horasTrabalhadasDia.TotalHours * valorHoraDia);
                }));

                totalDescontosDepartamento += ((diasUteis - diasTrabalhados) * valorHora);

                funcionario.TotalReceber = Math.Round(totalReceber, 2);
                funcionario.HorasExtras = Math.Round(horasExtras, 2);
                funcionario.HorasDebito = Math.Round(horasDebito, 2);
                funcionario.DiasFalta = diasUteis - diasTrabalhados;
                funcionario.DiasExtras = diasExtras;
                funcionario.DiasTrabalhados = diasTrabalhados;
                funcionarios.Add(funcionario);
            
            }));
            {


                var folhaPagamento = new FolhaDePagamento
                {
                    Departamento = departamento,
                    MesVigencia = mesVigencia,
                    AnoVigencia = anoVigencia,
                    TotalPagar = Math.Round(funcionarios.Sum(x => x.TotalReceber), 2),
                    TotalDescontos = Math.Round(totalDescontosDepartamento, 2),
                    TotalExtras = Math.Round(totalExtrasDepartamento, 2),
                    Funcionarios = funcionarios
                };


                return folhaPagamento;

            }
        }

        protected static int GetDiasUteisNoMes(int ano, int mes)
        {
            var diasUteis = 0;
            var diasNoMes = DateTime.DaysInMonth(ano, mes);

            for (int dia = 1; dia <= diasNoMes; dia++)
            {
                var data = new DateTime(ano, mes, dia);

                if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasUteis++;
                }
            }

            return diasUteis;
        }
    }
}
