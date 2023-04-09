using System;
using System.Collections.Generic;
using Domain.Repositories.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Entities;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;

namespace Infrastructure.Repositories
{
    public class FolhaDePontoRepository : BaseRepository<FolhaDePonto>, IFolhaDePontoRepository
    {
        private readonly string _filePath;
        public FolhaDePontoRepository(string filePath) : base(filePath) 
        {
            _filePath = filePath;
        }
        
    }
}
