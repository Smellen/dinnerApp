using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DinnerWebApp.Data.Models;

namespace DinnerWebApp.Data
{
    public interface IDataRepository
    {
        Task<DinnerDao> Add(DinnerDao dinner);
        Task<List<DinnerDao>> GetAllDinners();
    }
}
