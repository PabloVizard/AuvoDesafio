using Entity.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories.Interfaces;

namespace Domain.Services
{
    public class FolhaDePontoService : BaseService<FolhaDePonto>, IFolhaDePontoService
    {
        private readonly IFolhaDePontoRepository _folhaDePontoRepository;
        public FolhaDePontoService(IFolhaDePontoRepository folhaDePontoRepository): base(folhaDePontoRepository) 
        {
            _folhaDePontoRepository= folhaDePontoRepository;
        }

    }
}
