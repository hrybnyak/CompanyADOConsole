using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.TableDataGateway
{
    public class CategoryTableDataGateway : ITableDataGateway<Category>
    {
        private SqlConnection _conn;
        public CategoryTableDataGateway()
        {

        }
        public CategoryTableDataGateway(SqlConnection connection)
        {
            _conn = connection;
        }
        public void Delete(Category entity)
        {
            if (GetAll().Where(p => p.Id == entity.Id).FirstOrDefault() != null)
            {
                SqlCommand com = new SqlCommand("DELETE FROM Category WHERE Id = @id", _conn);
                com.Parameters.AddWithValue("@id", entity.Id);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Category> GetAll()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Category", _conn);
            SqlDataReader reader = com.ExecuteReader();
            List<Category> categories = new List<Category>();
            while (reader.Read())
            {
                var category = new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                categories.Add(category);
            }
            reader.Close();
            return categories;
        }

        public Category GetById(int? id)
        {
            if (id == null) return null;
            else
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Category WHERE Id = @id", _conn);
                com.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = com.ExecuteReader();
                Category category = null;
                while (reader.Read())
                {
                    category = new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
                reader.Close();
                return category;
            }
        }

        public void Insert(Category entity)
        {
            if (entity != null)
            {
                SqlCommand com = new SqlCommand("INSERT INTO Category(Name) VALUES (@name)", _conn);
                com.Parameters.AddWithValue("@name", entity.Name);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void Update(Category entity)
        {
            if (GetAll().Where(p => p.Id == entity.Id).FirstOrDefault() != null)
            {
                SqlCommand com = new SqlCommand("UPDATE Category SET Name = @name " +
                    "WHERE id = @id", _conn);
                com.Parameters.AddWithValue("@id", entity.Id);
                com.Parameters.AddWithValue("@name", entity.Name);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
