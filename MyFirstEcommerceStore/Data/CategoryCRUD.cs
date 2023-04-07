using MyFirstEcommerceStore.Data.Enums;
using MyFirstEcommerceStore.Data.Models;
using Npgsql;


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
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString())){
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    cats.Add(new Category() { CategoryId = rea[1].ToString(), CategoryDescription = rea[3].ToString(), CategoryName = rea[2].ToString(), ParentCategoryId = rea[4].ToString(), Tier = tierEnum });
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

            string sql = $"select * from categories where tier = '{tier.ToString()}'";
            Tier tierEnum = Tier.Tier1;

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                NpgsqlDataReader rea = command.ExecuteReader();
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString()))
                    {
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    cats.Add(new Category() { CategoryId = rea[1].ToString(), CategoryDescription = rea[3].ToString(), CategoryName = rea[2].ToString(), ParentCategoryId = rea[4].ToString(), Tier = tierEnum });
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
                while (rea.Read())
                {
                    if (!string.IsNullOrEmpty(rea[5].ToString()))
                    {
                        Enum.TryParse(rea[5].ToString(), out tierEnum);
                    }
                    cats.Add(new Category() { CategoryId = rea[1].ToString(), CategoryDescription = rea[3].ToString(), CategoryName = rea[2].ToString(), ParentCategoryId = rea[4].ToString(), Tier = tierEnum });
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
                if (rea.HasRows)
                {
                    while (rea.Read())
                    {
                        if (!string.IsNullOrEmpty(rea[5].ToString()))
                        {
                            Enum.TryParse(rea[5].ToString(), out tierEnum);
                        }

                        cat = new Category() { CategoryId = rea[1].ToString(), CategoryDescription = rea[3].ToString(), CategoryName = rea[2].ToString(), ParentCategoryId = rea[4].ToString(), Tier = tierEnum };

                    }

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
    }
}
