using Microsoft.Extensions.Configuration;
using PetShop.Domain.DTO;
using PetShop.Repository.Interface;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities;

namespace PetShop.Repository.Implementation
{
    public class BusinessRepository : IBusinessRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly string _sourceConnection;
        private readonly string _targetConnection;

        public BusinessRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _sourceConnection = configuration.GetConnectionString("QuickBiteDb");
            _targetConnection = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }

        public List<Business> GetAllBusinesses()
        {
            return _context.Businesses.ToList();
        }

        public void TransferBusinessData()
        {
            List<BusinessDto> businesses = new List<BusinessDto>();

            using (SqlConnection sourceConn = new SqlConnection(_sourceConnection))
            {
                string query = "SELECT Id, Name, Address, Rating, PhoneNumber, IsOpen, IsAvailable FROM dbo.Business WHERE IsDeleted = 0";
                SqlCommand cmd = new SqlCommand(query, sourceConn);
                sourceConn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    businesses.Add(new BusinessDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Rating = reader.GetFloat(3),
                        PhoneNumber = reader.GetString(4),
                        IsOpen = reader.GetBoolean(5),
                        IsAvailable = reader.GetBoolean(6)
                    });
                }
            }

            using (SqlConnection targetConn = new SqlConnection(_targetConnection))
            {
                targetConn.Open();
                foreach (var business in businesses)
                {
                    string insertQuery = "INSERT INTO dbo.Businesses (Name, Address, Rating, PhoneNumber, IsOpen, IsAvailable) " +
                                         "VALUES (@Name, @Address, @Rating, @PhoneNumber, @IsOpen, @IsAvailable)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, targetConn))
                    {
                        cmd.Parameters.AddWithValue("@Name", business.Name);
                        cmd.Parameters.AddWithValue("@Address", business.Address);
                        cmd.Parameters.AddWithValue("@Rating", business.Rating);
                        cmd.Parameters.AddWithValue("@PhoneNumber", business.PhoneNumber);
                        cmd.Parameters.AddWithValue("@IsOpen", business.IsOpen);
                        cmd.Parameters.AddWithValue("@IsAvailable", business.IsAvailable);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
