namespace TestOIS.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IProductService
    {
        Task<Response> GetProductsAsync();
    }
}

