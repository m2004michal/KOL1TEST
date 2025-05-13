using KOL1TEST.DTOs;

namespace KOL1TEST.Services;

public interface IDeliveriesService
{
    Task<DeliveryClientDriverDTO> GetDelivery(int id);

    Task<int> CreateNewDelivery(CreateDeliveryDto dto);
}