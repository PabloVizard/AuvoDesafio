using Application.Application.Interfaces;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application
{
    public class BaseApp<T> : IBaseApp<T> where T : class
    {
        private readonly IBaseService<T> _baseService;

        public BaseApp(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _baseService.GetAllAsync();
        }
    }
}
