using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentOrder.Core.Domain.Abstract;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.Core.Extensions;

namespace PaymentOrder.Core.DataAccess.Sql
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connection;
        public SqlUserRepository(string connection)
        {
            _connection = connection;
        }
        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                string query = "Insert into Users output inserted.id values(@Email,@PasswordHash)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("email", user.Email);
                command.Parameters.AddWithValue("passwordhash", user.PasswordHash);
                user.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(User user)
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                string query = "update Users set Email=@email,PasswordHash=@passwordhash where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", user.Id);
                command.Parameters.AddWithValue("email", user.Email);
                command.Parameters.AddWithValue("passwordhash", user.PasswordHash);
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                string query = "Select * from Users";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var list = new List<User>();
                while (reader.Read())
                {
                    var user = GetFromReader(reader);
                    list.Add(user);
                }

                return list;
            }
        }

        public User Get(int id)
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                string query = "select * from Users where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = GetFromReader(reader);
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public User Get(string email)
        {
            using (var connection = new SqlConnection(_connection))
            {
                
                connection.Open();
                string query = "select * from Users where Email=@email";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = GetFromReader(reader);
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                string query = "delete from Users where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }

        private User GetFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.Get<int>("Id"),
                Email = reader.Get<string>("Email"),
                PasswordHash = reader.Get<string>("PasswordHash"),
            };
        }
    }
}