
namespace KOL1TEST.DTOs;

public class DeliveryClientDriverDTO
{
    public DateTime DateTime { get; set; }
    public CustomerDTO Customer { get; set; }
    public DriverDTO Driver { get; set; }
    public List<ProductDTO> Products { get; set; }
}