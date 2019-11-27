using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DAL.Models;

namespace DAL.TableDataGateway
{
    public class ProviderTableDataGateway:ITableDataGateway<Provider>
    {
        private SqlConnection _conn;
        public ProviderTableDataGateway()
        {

        }
        public ProviderTableDataGateway(SqlConnection connection)
        {
            _conn = connection;
        }

        public void Delete(Provider entity)
        {
            if (GetAll().Where(p => p.Id == entity.Id).FirstOrDefault() != null)
            {
                SqlCommand com = new SqlCommand("DELETE FROM Provider WHERE Id = @id", _conn);
                com.Parameters.AddWithValue("@id", entity.Id);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Provider> GetAll()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Provider", _conn);
            SqlDataReader reader = com.ExecuteReader();
            List<Provider> providers = new List<Provider>();
            while (reader.Read())
            {
                var provider = new Provider
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetValue(1).ToString(),
                    PhoneNumber = reader.GetValue(2).ToString()
                };
                providers.Add(provider);
            }
            reader.Close();
            return providers;
        }

        public Provider GetById(int? id)
        {
            if (id == null) return null;
            else
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Provider WHERE Id = @id", _conn);
                com.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = com.ExecuteReader();
                Provider provider = null;
                while (reader.Read())
                {
                    provider = new Provider
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        PhoneNumber = reader.GetString(2)
                    };
                }
                reader.Close();
                return provider;
            }
        }

        public void Insert(Provider entity)
        {
            if (entity != null)
            {
                SqlCommand com = new SqlCommand("INSERT INTO Provider(Name, PhoneNumber) VALUES (@name, @phone)", _conn);
                com.Parameters.AddWithValue("@name", entity.Name);
                com.Parameters.AddWithValue("@phone", entity.PhoneNumber);
                com.ExecuteNonQuery();
            }
        }

        public void Update(Provider entity)
        {
            if (GetAll().Where(p => p.Id == entity.Id).FirstOrDefault() != null)
            {
                SqlCommand com = new SqlCommand("UPDATE Provider SET Name = @name, PhoneNumber = @phone " +
                    "WHERE id = @id", _conn);
                com.Parameters.AddWithValue("@id", entity.Id);
                com.Parameters.AddWithValue("@name", entity.Name);
                com.Parameters.AddWithValue("@phone", entity.PhoneNumber);
                com.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
