using KOL1TEST.DTOs;
using KOL1TEST.Models;
using Microsoft.Data.SqlClient;

namespace KOL1TEST.Repositories;

public class DeliveriesRepository : IDeliveriesRepository
{
    
    private readonly string _connectionString;

    public DeliveriesRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    
    
    
    public async Task<List<ProductDTO>> GetProductsForDeliveryId(int id)
    {
        var products = new List<ProductDTO>();

        await using (var connection = new SqlConnection(_connectionString))
        {
            
            await connection.OpenAsync();

            
            var query = @"SELECT name, price, amount FROM Product_Delivery d
                          INNER JOIN dbo.Product p on p.product_id = d.product_id where delivery_id = @id";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        ProductDTO productDTO = new ProductDTO()
                        {
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            Amount = reader.GetInt32(reader.GetOrdinal("amount"))
                        };
                        products.Add(productDTO);

                    }
                    
                }
                
                
            }
            
        }
        return products;
    }

    public async Task<DriverDTO> FindDriverByLicence(string dtoLicenceNumber)
    {
        var driverDto = new DriverDTO();
        
        await using (var connection = new SqlConnection(_connectionString))
        {
            var driver = new DriverDTO();
            
            await connection.OpenAsync();

            
            var query = @"SELECT * FROM dbo.Driver d Where licence_number = @dtoLicenceNumber";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@dtoLicenceNumber", dtoLicenceNumber);

                await using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {

                        driverDto = new DriverDTO()
                        {
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            LicenseNumber = reader.GetString(reader.GetOrdinal("licence_number"))
                        };

                    }
                    
                }
                
                
            }
            
        }
        return driverDto;
    }

    public async Task<int> CreateNewDelivery(Delivery delivery)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = @"INSERT INTO Delivery (delivery_id, customer_id, driver_id, date)
                OUTPUT INSERTED.delivery_id
                VALUES(@delivery_id, @customer_id, @driver_id, @date)
                         ";
            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@delivery_id", delivery.DeliveryId);
                command.Parameters.AddWithValue("@customer_id",delivery.CustomerId);
                command.Parameters.AddWithValue("@driver_id", delivery.DriverId);
                command.Parameters.AddWithValue("@date", delivery.Date);
                
                var insertedid = await command.ExecuteScalarAsync();
                
                return Convert.ToInt32(insertedid);
            }
        }
    }


    public async Task<DeliveryClientDriverDTO> GetDelivery(int id)
    {
        var delivery = new DeliveryClientDriverDTO();

        await using (var connection = new SqlConnection(_connectionString))
        {
            
            await connection.OpenAsync();

            
            var query = @"SELECT
                 date, c.first_name, c.last_name, date_of_birth, d.first_name, d.last_name, d.licence_number FROM Delivery
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
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                dateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                                
                            },
                            Driver = new DriverDTO()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                LicenseNumber = reader.GetString(reader.GetOrdinal("licence_number"))
                            },
                            Products = new List<ProductDTO>(){}
                        };
                        
                    }
                    
                }
                
                
            }
            
        }

        delivery.Products = await GetProductsForDeliveryId(id);
        return delivery;
    }
}