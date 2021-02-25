using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DinnerWebApp.Data.Models;

namespace DinnerWebApp.Data
{
    public interface IDataRepository
    {
        Task<OwnerDao> Add(OwnerDao owner);
        Task<DinnerDao> Add(DinnerDao dinner);
        Task<List<DinnerDao>> Search(DateTime date);
        Task<List<DinnerDao>> GetDinners(int skip, int take);
        Task<List<OwnerDao>> GetOwners();
    }
}
