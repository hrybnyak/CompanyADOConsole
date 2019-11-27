using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Models;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.TableDataGateway
{
    public class ProductTableDataGateway : ITableDataGateway<Product> 
    {
        private SqlConnection _conn;
        public ProductTableDataGateway()
        {

        }
        public ProductTableDataGateway(SqlConnection connection)
        {
            _conn = connection;
        }

        public void Delete(Product entity)
        {
            if (GetAll().Where(p => p.Id == entity.Id).FirstOrDefault() != null)
            {
                SqlCommand com = new SqlCommand("DELETE FROM Product WHERE Id = @id", _conn);
                com.Parameters.AddWithValue("@id", entity.Id);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Product", _conn);
            SqlDataReader reader = com.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                var product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    ProviderId = reader.GetInt32(3),
                    CategoryId = reader.GetInt32(4)
                };
                products.Add(product);
            }
            reader.Close();
            return products;
        }

        public Product GetById(int? id)
        {
            if (id == null) return null;
            else
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Product WHERE Id = @id", _conn);
                com.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = com.ExecuteReader();
                Product product = null;
                while (reader.Read())
                {
                    product = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        ProviderId = reader.GetInt32(3),
                        CategoryId = reader.GetInt32(4)
                    };
                }
                reader.Close();
                return product;
            }
        }

        public void Insert(Product entity)
        {
            SqlCommand com = new SqlCommand("INSERT INTO Product(Name, Price, ProviderId, CategoryId) VALUES (@name, @price, @provider, @category)", _conn);
            com.Parameters.AddWithValue("@name", entity.Name);
            com.Parameters.AddWithValue("@price", entity.Price);
            com.Parameters.AddWithValue("@provider", entity.ProviderId);
            com.Parameters.AddWithValue("@category", entity.CategoryId);
            com.ExecuteNonQuery();
        }

        public void Update(Product entity)
        {
            if (GetAll().Where(p => p.Id == entity.Id).FirstOrDefault() != null)
            {
                SqlCommand com = new SqlCommand("UPDATE Product SET Name = @name, Price = @price, ProviderId = @provider, CategoryId = @category " +
                    "WHERE id = @id", _conn);
                com.Parameters.AddWithValue("@id", entity.Id);
                com.Parameters.AddWithValue("@name", entity.Name);
                com.Parameters.AddWithValue("@price", entity.Price);
                com.Parameters.AddWithValue("@provider", entity.ProviderId);
                com.Parameters.AddWithValue("@category", entity.CategoryId);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
