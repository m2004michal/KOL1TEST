namespace KOL1TEST.DTOs;

public class CreateDeliveryDto
{
    public int DeliveryId { get; set; }
    public int CustomerId { get; set; }
    public string LicenceNumber { get; set; }
    public List<ProductDTO> Products { get; set; }
}