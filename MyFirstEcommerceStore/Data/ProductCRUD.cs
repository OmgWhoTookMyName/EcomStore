using Npgsql;
using System.Security.Cryptography;

namespace MyFirstEcommerceStore.Data
{
    public class ProductCRUD
    {
        public Task<Products> CreateProducts(Products product)
        {
            //TODO: create a check for uniqueness
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"INSERT INTO products(ProductId, ProductName, ProductDescription) VALUES('{product.ProductId}','{product.ProductName}','{product.ProductDescription}')";
            cmd.ExecuteNonQuery();

            con.Close();
            GetAllProducts();
            return Task.FromResult(product);
        }

        public Task<List<Products>> GetAllProducts()
        {
            //TODO: Clean these commands up
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            var products = new List<Products>();

            string sql = "select * from products";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                while (rea.Read())
                {
                    products.Add(new Products() { ProductName = rea[1].ToString(), ProductDescription = rea[3].ToString(), ProductId = rea[4].ToString() });
                }
                
            }

            con.Close();

            return Task.FromResult(products);
        }

        public Task<bool> DeleteProduct(Products product)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sql = $"delete from products where productid = \'{product.ProductId}\'";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();

            }
            con.Close();
            GetAllProducts();
            return Task.FromResult(true);
        }
    }
}
