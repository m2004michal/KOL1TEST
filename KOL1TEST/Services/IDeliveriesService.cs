using Kolokwium1.DTOs;

namespace Kolokwium1.Services;

public interface IDeliveriesService
{
    Task<DeliveryClientDriverDTO> GetDelivery(int id);

}