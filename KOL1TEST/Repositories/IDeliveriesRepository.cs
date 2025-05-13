using Kolokwium1.DTOs;

namespace Kolokwium1.Repositories;

public interface IDeliveriesRepository
{
    Task<DeliveryClientDriverDTO> GetDelivery(int id);

}