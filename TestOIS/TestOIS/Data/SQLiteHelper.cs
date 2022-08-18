namespace TestOIS.Data
{
    
    using SQLiteNetExtensions;
    using Models;
    using System.Threading.Tasks;
    using System.Collections;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SQLiteHelper
    {
        #region Constructor
        public SQLiteHelper(string path)
        {
            db = new SQLiteAsyncConnection(path);
            db.CreateTableAsync<Product>().Wait();
            db.CreateTableAsync<Rating>().Wait();
        }
        #endregion

        #region Attributes
        private readonly SQLiteAsyncConnection db;
        #endregion

        #region Methods
        public async Task SaveProductsLocalAsync(List<Product> products)
        {
            try
            {
                await SaveAndDeleteProduct(products);
                await SaveAndDeleteRating(products);
            }
            catch (Exception)
            {

            }
            
        }

        private async Task SaveAndDeleteProduct(List<Product> products)
        {
            await db.DeleteAllAsync<Product>();
            await db.InsertAllAsync(products);
        }

        private async Task SaveAndDeleteRating(List<Product> products)
        {
            await db.DeleteAllAsync<Rating>();
            await db.InsertAllAsync(products.Select(x => new Rating
            {
                Count = x.Rating.Count,
                Rate = x.Rating.Rate,
                ProductId = x.Id
            }));
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var query =
                        (from product in await db.Table<Product>().ToListAsync()
                        join rating in await db.Table<Rating>().ToListAsync() on product.Id equals rating.ProductId
                        select new Product
                        {
                             Id = product.Id,
                             Category = product.Category,
                             Title = product.Title,
                             Description = product.Description,
                             Price = product.Price,
                             Image = product.Image,
                             Rating = new Rating
                             {
                                 Count = rating.Count,
                                 Rate = rating.Rate,
                                 ProductId = product.Id
                             }
                        }).ToList();

                return query;
            }
            catch (Exception)
            {

            }
            return new List<Product>();
        }

        
        #endregion
    }
}

