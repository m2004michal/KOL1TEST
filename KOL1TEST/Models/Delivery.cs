namespace Kolokwium1.Models;

public class Delivery
{
    public int DeliveryId { get; set; }
    public int CustomerId { get; set; }
    public int DriverId { get; set; }
    public DateTime Date { get; set; }
    
}