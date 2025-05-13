using KOL1TEST.DTOs;
using KOL1TEST.Models;

namespace KOL1TEST.Repositories;

public interface IDeliveriesRepository
{
    Task<DeliveryClientDriverDTO> GetDelivery(int id);
    Task<List<ProductDTO>> GetProductsForDeliveryId(int id);
    Task<DriverDTO> FindDriverByLicence(string dtoLicenceNumber);
    Task<int> CreateNewDelivery(Delivery delivery);
}