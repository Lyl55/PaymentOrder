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
    public class SqlBankRepository:IBankRepository
    {
        private readonly string _connect;

        public SqlBankRepository(string connect)
        {
            _connect = connect;
        }
        public void Add(Bank bank)
        {
            using (var connection=new SqlConnection(_connect))
            {
                connection.Open();
                string query = "Insert into Banks output inserted.id values(@Name,@VOEN,@CorrespondentAccount,@SWIFT,@CreatorId,@modifiedDate,@IsDeleted)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("name", bank.Name);
                command.Parameters.AddWithValue("voen", bank.VOEN);
                command.Parameters.AddWithValue("correspondentaccount", bank.CorrespondentAccount);
                command.Parameters.AddWithValue("swift", bank.SWIFT);
                command.Parameters.AddWithValue("creatorid", bank.CreatorId);
                command.Parameters.AddWithValue("modifiedDate", bank.ModifiedDate);
                command.Parameters.AddWithValue("isdeleted", bank.IsDeleted);
                bank.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(Bank bank)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query =
                    "update Banks set Name=@name,VOEN=@voen,CorresPondentAccount=@correspondentaccount,SWIFT=@swift,CreatorId=@creatorid,modifiedDate=@modifiedDate,IsDeleted=@isdeleted where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", bank.Id);
                command.Parameters.AddWithValue("name", bank.Name);
                command.Parameters.AddWithValue("voen", bank.VOEN);
                command.Parameters.AddWithValue("correspondentaccount", bank.CorrespondentAccount);
                command.Parameters.AddWithValue("swift", bank.SWIFT);
                command.Parameters.AddWithValue("creatorid", bank.CreatorId);
                command.Parameters.AddWithValue("modifiedDate", bank.ModifiedDate);
                command.Parameters.AddWithValue("isdeleted", bank.IsDeleted);
                command.ExecuteNonQuery();
            }
        }

        public List<Bank> GetAll()
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Banks where IsDeleted = 0";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var list = new List<Bank>();
                while (reader.Read())
                {
                   var bank = GetFromReader(reader);
                   list.Add(bank);
                }

                return list;
            }
        }

        public Bank Get(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "select * from Banks where Id= @id and IsDeleted = 0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var bank = GetFromReader(reader);
                    return bank;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "delete from Banks where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();

            }
        }

        private Bank GetFromReader(SqlDataReader reader)
        {
            return new Bank
            {
                Id = reader.Get<int>("Id"),
                Name = reader.Get<string>("Name"),
                VOEN = reader.Get<string>("VOEN"),
                CorrespondentAccount = reader.Get<string>("CorrespondentAccount"),
                SWIFT = reader.Get<string>("SWIFT"),
                CreatorId = reader.Get<int>("CreatorId"),
                ModifiedDate = reader.Get<DateTime>("ModifiedDate"),
                IsDeleted = reader.Get<bool>("IsDeleted")
            };
        }
    }
}
