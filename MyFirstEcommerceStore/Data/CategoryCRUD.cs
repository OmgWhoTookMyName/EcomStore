using MyFirstEcommerceStore.Data.Enums;
using MyFirstEcommerceStore.Data.Models;
using Npgsql;
using Radzen.Blazor;
using System.Reflection.PortableExecutable;


namespace MyFirstEcommerceStore.Data
{
    public class CategoryCRUD
    {
        public Task<Category> CreateCategory(Category category)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"INSERT INTO categories(CategoryId, Name, Description, ParentCategoryId, Tier) VALUES('{category.CategoryId}','{category.CategoryName}','{category.CategoryDescription}','{category.ParentCategoryId}','{category.Tier}')";
            cmd.ExecuteNonQuery();

            return Task.FromResult(category);
        }

        public Task<List<Category>> GetAllCategories()
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            var cats = new List<Category>();

            string sql = "select * from categories";
            Tier tierEnum = Tier.Tier1;

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                string catId, catDesc, catName, parentCat;
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString())){
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    int catIdIndex = rea.GetOrdinal("categoryid");
                    int catDescIndex = rea.GetOrdinal("description");
                    int catNameIndex = rea.GetOrdinal("name");
                    int catParentIndex = rea.GetOrdinal("parentcategoryid");
                    if (!rea.IsDBNull(catIdIndex))
                    {
                        catId = rea.GetString(catIdIndex);
                    }
                    else
                    {
                        catId = string.Empty;
                    }
                    if(!rea.IsDBNull(catDescIndex))
                    {
                        catDesc = rea.GetString(catDescIndex);
                    }
                    else
                    {
                        catDesc = string.Empty;
                    }
                    if(!rea.IsDBNull(catNameIndex))
                    {
                        catName = rea.GetString(catNameIndex);
                    }
                    else
                    {
                        catName = string.Empty;
                    }
                    if(!rea.IsDBNull (catParentIndex))
                    {
                        parentCat = rea.GetString(catParentIndex);
                    }
                    else
                    {
                        parentCat = string.Empty;
                    }

                    cats.Add(new Category() { CategoryId = catId, CategoryDescription = catDesc, CategoryName = catName, ParentCategoryId = parentCat, Tier = tierEnum });
                }
            }
            con.Close();
            return Task.FromResult(cats);

        }



        public Task<List<Category>> GetAllCategoriesByTier(Tier tier)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            var cats = new List<Category>();

            string sql = $"select * from categories where tier = '{tier}'";
            Tier tierEnum = Tier.Tier1;

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                string catId, catDesc, catName, parentCat;
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString()))
                    {
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    int catIdIndex = rea.GetOrdinal("categoryid");
                    int catDescIndex = rea.GetOrdinal("description");
                    int catNameIndex = rea.GetOrdinal("name");
                    int catParentIndex = rea.GetOrdinal("parentcategoryid");
                    if (!rea.IsDBNull(catIdIndex))
                    {
                        catId = rea.GetString(catIdIndex);
                    }
                    else
                    {
                        catId = string.Empty;
                    }
                    if (!rea.IsDBNull(catDescIndex))
                    {
                        catDesc = rea.GetString(catDescIndex);
                    }
                    else
                    {
                        catDesc = string.Empty;
                    }
                    if (!rea.IsDBNull(catNameIndex))
                    {
                        catName = rea.GetString(catNameIndex);
                    }
                    else
                    {
                        catName = string.Empty;
                    }
                    if (!rea.IsDBNull(catParentIndex))
                    {
                        parentCat = rea.GetString(catParentIndex);
                    }
                    else
                    {
                        parentCat = string.Empty;
                    }

                    cats.Add(new Category() { CategoryId = catId, CategoryDescription = catDesc, CategoryName = catName, ParentCategoryId = parentCat, Tier = tierEnum });
                }
            }
            con.Close();
            return Task.FromResult(cats);
        }

        public Task<List<Category>> GetAllChildren(Category parentCat)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            var cats = new List<Category>();

            string sql = $"select * from categories where parentcategoryid = '{parentCat.CategoryId}'";

            Tier tierEnum = Tier.Tier1;

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                string catId, catDesc, catName, parenCat;
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString()))
                    {
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    int catIdIndex = rea.GetOrdinal("categoryid");
                    int catDescIndex = rea.GetOrdinal("description");
                    int catNameIndex = rea.GetOrdinal("name");
                    int catParentIndex = rea.GetOrdinal("parentcategoryid");
                    if (!rea.IsDBNull(catIdIndex))
                    {
                        catId = rea.GetString(catIdIndex);
                    }
                    else
                    {
                        catId = string.Empty;
                    }
                    if (!rea.IsDBNull(catDescIndex))
                    {
                        catDesc = rea.GetString(catDescIndex);
                    }
                    else
                    {
                        catDesc = string.Empty;
                    }
                    if (!rea.IsDBNull(catNameIndex))
                    {
                        catName = rea.GetString(catNameIndex);
                    }
                    else
                    {
                        catName = string.Empty;
                    }
                    if (!rea.IsDBNull(catParentIndex))
                    {
                        parenCat = rea.GetString(catParentIndex);
                    }
                    else
                    {
                        parenCat = string.Empty;
                    }

                    cats.Add(new Category() { CategoryId = catId, CategoryDescription = catDesc, CategoryName = catName, ParentCategoryId = parenCat, Tier = tierEnum });
                }
            }

            con.Close();
            return Task.FromResult(cats);

        }

        public Task<Category> GetCategoryByName(string categoryName)
        {
            //TODO: Find a cleaner way to get categories that is less risky
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            Category cat = new();

            string sql = $"select * from categories where name = '{categoryName}'";
            Tier tierEnum = Tier.Tier1;

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                string catId, catDesc, catName, parenCat;
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString()))
                    {
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    int catIdIndex = rea.GetOrdinal("categoryid");
                    int catDescIndex = rea.GetOrdinal("description");
                    int catNameIndex = rea.GetOrdinal("name");
                    int catParentIndex = rea.GetOrdinal("parentcategoryid");
                    if (!rea.IsDBNull(catIdIndex))
                    {
                        catId = rea.GetString(catIdIndex);
                    }
                    else
                    {
                        catId = string.Empty;
                    }
                    if (!rea.IsDBNull(catDescIndex))
                    {
                        catDesc = rea.GetString(catDescIndex);
                    }
                    else
                    {
                        catDesc = string.Empty;
                    }
                    if (!rea.IsDBNull(catNameIndex))
                    {
                        catName = rea.GetString(catNameIndex);
                    }
                    else
                    {
                        catName = string.Empty;
                    }
                    if (!rea.IsDBNull(catParentIndex))
                    {
                        parenCat = rea.GetString(catParentIndex);
                    }
                    else
                    {
                        parenCat = string.Empty;
                    }

                    cat = (new Category() { CategoryId = catId, CategoryDescription = catDesc, CategoryName = catName, ParentCategoryId = parenCat, Tier = tierEnum });
                }

            }
            con.Close();
            return Task.FromResult(cat);
        }

        public Task<bool> DeleteCategory(Category category)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sql = $"delete from categories where categoryid = \'{category.CategoryId}\'";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();

            }
            con.Close();
            return Task.FromResult(true);

        }

        public async Task<bool> CreateCategoryLinks(Products product, Category category)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            
            ProductCRUD crud = new ProductCRUD();

            product = await crud.ProductCleanser(product);

            string sql = $"INSERT INTO CategoryLinks(productid, categoryid) values('{product.ProductId}','{category.CategoryId}')";
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();

            con.Close();

            return await Task.FromResult(true);

        }

        public async Task<bool> DeleteCategoryLinks(Products product, Category category)
        {
            var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            ProductCRUD crud = new ProductCRUD();

            product = await crud.ProductCleanser(product);

            string sql = $"delete from categorylinks where productid = '{product.ProductId}' and categoryid = '{category.CategoryId}'";
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();

            con.Close();

            return await Task.FromResult(true);

        }
    }
}
