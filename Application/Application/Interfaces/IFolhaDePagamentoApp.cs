using Application.Models;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Interfaces
{
    public interface IFolhaDePagamentoApp 
    {
        Task<FolhaDePagamento> ProcessarFolhasDePonto(string file);
    }

}
