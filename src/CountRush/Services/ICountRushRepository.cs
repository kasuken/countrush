using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountRush.Services
{
    public interface ICountRushRepository
    {
        Task<int> RetrieveVisitors(string repositoryname);
    }
}
