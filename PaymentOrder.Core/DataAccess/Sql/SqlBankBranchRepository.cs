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
    public class SqlBankBranchRepository : IBankBranchRepository
    {
        private readonly string _connect;

        public SqlBankBranchRepository(string connect)
        {
            _connect = connect;
        }

        public void Add(BankBranch bankBranch)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();
                string query = "Insert into BankBranchs output inserted.id values(@BankId,@Name,@Phone,@Fax,@Address,@CreatorId,@ModifiedDate,@IsDeleted)";
                
                var command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("bankid", bankBranch.Bank.Id);
                command.Parameters.AddWithValue("name", bankBranch.Name);
                command.Parameters.AddWithValue("phone", bankBranch.Phone);
                command.Parameters.AddWithValue("fax", bankBranch.Fax);
                command.Parameters.AddWithValue("address", bankBranch.Address);
                command.Parameters.AddWithValue("creatorid", bankBranch.CreatorId);
                command.Parameters.AddWithValue("ModifiedDate", bankBranch.ModifiedDate);
                command.Parameters.AddWithValue("isdeleted", bankBranch.IsDeleted);
                
                bankBranch.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(BankBranch bankBranch)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();

                string query = "update BankBranchs set BankId=@bankid,Name=@name,Phone=@phone,Fax=@fax,Address=@address,CreatorId=@creatorid,ModifiedDate=@ModifiedDate,IsDeleted=@isdeleted where Id=@id";
                
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("id", bankBranch.Id);
                command.Parameters.AddWithValue("bankid", bankBranch.Bank.Id);
                command.Parameters.AddWithValue("name", bankBranch.Name);
                command.Parameters.AddWithValue("phone", bankBranch.Phone);
                command.Parameters.AddWithValue("fax", bankBranch.Fax);
                command.Parameters.AddWithValue("address", bankBranch.Address);
                command.Parameters.AddWithValue("creatorid", bankBranch.CreatorId);
                command.Parameters.AddWithValue("ModifiedDate", bankBranch.ModifiedDate);
                command.Parameters.AddWithValue("isdeleted", bankBranch.IsDeleted);
                
                command.ExecuteNonQuery();
            }
        }

        public List<BankBranch> GetAll()
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();

                string query = "select BankBranchs.Id,BankBranchs.BankId,Banks.Name as BankName,BankBranchs.Name,BankBranchs.Phone,BankBranchs.Fax,BankBranchs.Address,BankBranchs.CreatorId,BankBranchs.ModifiedDate,BankBranchs.IsDeleted from BankBranchs inner join Banks on BankBranchs.BankId=Banks.Id where BankBranchs.IsDeleted = 0";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var list = new List<BankBranch>();

                while (reader.Read())
                {
                    var bankBranch = GetFromReader(reader);
                    list.Add(bankBranch);
                }

                return list;
            }
        }

        public BankBranch Get(int id)
        {
            using (var connection = new SqlConnection(_connect))
            {
                connection.Open();

                string query = "select BankBranchs.Id,BankBranchs.BankId,Banks.Name as BankName,BankBranchs.Name,BankBranchs.Phone,BankBranchs.Fax,BankBranchs.Address,BankBranchs.CreatorId,BankBranchs.ModifiedDate,BankBranchs.IsDeleted from BankBranchs inner join Banks on BankBranchs.BankId=Banks.Id where BankBranchs.Id = @id and BankBranchs.IsDeleted = 0";
                
                var command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("Id", id);
                
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var bankBranch = GetFromReader(reader);
                    return bankBranch;
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
                string query = "delete from BankBranchs where Id=@id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }

        private BankBranch GetFromReader(SqlDataReader reader)
        {
            return new BankBranch
            {
                Id = reader.Get<int>("Id"),
                Name = reader.Get<string>("Name"),
                Phone = reader.Get<string>("Phone"),
                Fax = reader.Get<string>("Fax"),
                Address = reader.Get<string>("Address"),
                CreatorId = reader.Get<int>("CreatorId"),
                ModifiedDate = reader.Get<DateTime>("ModifiedDate"),
                IsDeleted = reader.Get<bool>("IsDeleted"),
                Bank =new Bank
                {
                    Id = reader.Get<int>("BankId"),
                    Name=reader.Get<string>("BankName")
                }
            };
        }
    }
}
