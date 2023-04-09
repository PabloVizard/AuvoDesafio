using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Interfaces
{
    public interface IBaseApp <T> where T: class
    {
        Task<List<T>> GetAllAsync();
    }
}
