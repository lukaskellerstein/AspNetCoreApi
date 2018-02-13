using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestApi.DataAccess;
using TestApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TestApi.Business
{
    public class TestService
    {
        TestDbContext _db;

        public TestService(TestDbContext db)
        {
            _db = db;
        }

        public Product[] GetAllProducts()
        {
            var result = _db.Products.AsQueryable<Product>();
            return result.ToArray();
        }


        public TestDTO GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", id);
            var result = _db.Products.Find(filter).FirstOrDefault();
            return TestDTO.Data(result);
        }

        public TestDTO AddProduct(Product item)
        {
            _db.Products.InsertOne(item);
            return TestDTO.Data(item);
        }

        public TestDTO RemoveProduct(Product item)
        {
            _db.Products.DeleteOne(Builders<Product>.Filter.Eq(r => r.Id, item.Id));
            return TestDTO.Ok();
        }

        public TestDTO UpdateProduct(Product item)
        {
            _db.Products.ReplaceOne(Builders<Product>.Filter.Eq(r => r.Id, item.Id), item, new UpdateOptions() { IsUpsert = true });
            return TestDTO.Ok();
        }

        public void Seed(){
            //******************************************/
            /*  SEED */
            //******************************************/
            Product temp = new Product();
            temp.Name = "iPhone X";
            temp.State = "NEW";
            temp.Category = "Mobile phones";
            temp.Price = 35000;
            _db.Products.InsertOne(temp);

            temp = new Product();
            temp.Name = "Samsung s8";
            temp.State = "APPROVED";
            temp.Category = "Mobile phones";
            temp.Price = 30000;
            _db.Products.InsertOne(temp);

            temp = new Product();
            temp.Name = "Google Pixel 2";
            temp.State = "APPROVED";
            temp.Category = "Mobile phones";
            temp.Price = 32000;
            _db.Products.InsertOne(temp);
        }

    }
}
