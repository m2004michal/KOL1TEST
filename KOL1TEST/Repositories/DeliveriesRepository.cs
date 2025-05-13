using Kolokwium1.DTOs;
using Microsoft.Data.SqlClient;
using WebApplication1.Exceptions;

namespace Kolokwium1.Repositories;

public class DeliveriesRepository : IDeliveriesRepository
{
    
    private readonly string _connectionString;

    public DeliveriesRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<DeliveryClientDriverDTO> GetDelivery(int id)
        {
        var delivery = new DeliveryClientDriverDTO();

        await using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT date, c.first_name, c.last_name,
               date_of_birth, d.first_name, d.last_name,
               d.licence_number FROM Delivery
                 INNER JOIN dbo.Customer C on C.customer_id = Delivery.customer_id
                 INNER JOIN dbo.Driver D on D.driver_id = Delivery.driver_id
                 where delivery_id = @id";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        delivery = new DeliveryClientDriverDTO()
                        {
                            DateTime = reader.GetDateTime(reader.GetOrdinal("date")),
                            Customer = new CustomerDTO()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("C.first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("C.last_name")),
                                dateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                                
                            },
                            Driver = new DriverDTO()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("D.first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("D.last_name")),
                                LicenseNumber = reader.GetString(reader.GetOrdinal("license_number"))
                            }
                        };
                        
                    }
                    
                }
                
                
            }
            
        }
        return delivery;
    }
}