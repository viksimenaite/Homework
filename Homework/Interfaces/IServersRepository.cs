using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public interface IServersRepository
    {
        Task<List<ServerResponse>> GetAllServersFromDBAsync();
        Task StoreAllServersIntoDBAsync(List<ServerResponse> serversList);
    }
}
