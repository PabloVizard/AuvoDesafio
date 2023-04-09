using Application.Application.Interfaces;
using Domain.Services.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application
{
    public class FolhaDePontoApp : BaseApp<FolhaDePonto>, IFolhaDePontoApp
    {
        private readonly IFolhaDePontoService _folhaDePontoService;
        public FolhaDePontoApp(IFolhaDePontoService folhaDePontoService) : base(folhaDePontoService)
        {
            _folhaDePontoService = folhaDePontoService;
        }

    }
}
