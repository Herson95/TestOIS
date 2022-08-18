using System;
namespace TestOIS.Interfaces
{
    using System.Threading.Tasks;
    using Models;

    public interface INetworkService
    {
        Task<Response> CheckConnectionAsync();
    }
}

