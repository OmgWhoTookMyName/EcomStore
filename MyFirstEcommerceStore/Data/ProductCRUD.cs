using Microsoft.Extensions.FileProviders;
using Npgsql;
using System.Security.Cryptography;

namespace MyFirstEcommerceStore.Data
{
    public class ProductCRUD
    {
        #region Products
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

            string sql = "select p.productid, productname, productdescription, b.name, pi.URL from products p left join brandLinks bl on p.productid=bl.productid left join brands b on b.brandid=bl.brandid left join ProductImages pi on pi.productid=p.productid";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                while (rea.Read())
                {
                    products.Add(new Products() { ProductName = rea[1].ToString(), ProductDescription = rea[2].ToString(), ProductId = rea[0].ToString(), BrandName = rea[3].ToString(), URL = rea[4].ToString() });
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
            return Task.FromResult(true);
        }


        #endregion
        #region Brands
        public Task<List<Brand>> GetAllBrands()
        {
            //TODO: Clean these commands up
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var brands = new List<Brand>();

            string sql = "select * from Brands";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                while (rea.Read())
                {
                    brands.Add(new Brand { BrandId = rea[3].ToString(), Description = rea[2].ToString(), Name = rea[1].ToString() });
                }

            }

            con.Close();
            return Task.FromResult(brands);
        }


        public Task<Brand> CreateBrand(Brand brand)
        {
            //TODO: create a check for uniqueness
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"INSERT INTO Brands(Name, Description, BrandId) VALUES('{brand.Name}','{brand.Description}','{brand.BrandId}')";
            cmd.ExecuteNonQuery();

            con.Close();
            return Task.FromResult(brand);
        }

        public Task<bool> DeleteBrand(Brand brand)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sql = $"delete from brands where brandid = \'{brand.BrandId}\'";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();

            }
            con.Close();
            return Task.FromResult(true);
        }

        public async Task<List<string>> GetBrandNames()
        {
            var brands = await GetAllBrands();

            List<string> brandNames = new List<string>(); 

            foreach(var brand in brands)
            {
                if (brand.Name is not null)
                {
                    brandNames.Add(brand.Name);
                }
                
            }

            return (List<string>)brandNames;
        }

        public async Task<Brand> BrandLookup(string brandName)
        {
            var brands = await GetAllBrands();
            Brand targetBrand = new Brand();
            foreach(var brand in brands)
            {
                if(brandName.Equals(brand.Name))
                {
                    targetBrand = brand;
                }
            }

            return await Task.FromResult(targetBrand);
        }

        public async Task<bool> LinkBrandsToProducts(Products product, Brand brand)
        {
            //TODO: create a check for uniqueness
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"INSERT INTO BrandLinks(ProductId, BrandId) VALUES('{product.ProductId}','{brand.BrandId}')";
            cmd.ExecuteNonQuery();

            con.Close();
            return await Task.FromResult(true);
        }

        #endregion

        public async Task<bool> UploadImagePathsToDb(string url, Products product)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"INSERT INTO ProductImages(ProductId, URL) VALUES('{product.ProductId}','{url}')";
            cmd.ExecuteNonQuery();

            con.Close();

            return await Task.FromResult(true);
        }
    }
}
