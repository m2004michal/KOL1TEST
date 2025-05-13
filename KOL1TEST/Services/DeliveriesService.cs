using Kolokwium1.DTOs;
using Kolokwium1.Repositories;
using WebApplication1.Exceptions;

namespace Kolokwium1.Services;

public class DeliveriesService : IDeliveriesService
{
    
    private readonly IDeliveriesRepository _deliveriesRepository;
    public DeliveriesService(IDeliveriesRepository deliveriesyRepository)
    {
        _deliveriesRepository = deliveriesyRepository;
    }
    
    public async Task<DeliveryClientDriverDTO> GetDelivery(int id)
    {
        if(id<0)
            throw new BadRequestException("id must be greater than 0");
        
        // if(!await _deliveriesRepository.DoesDeliveryExist(id))
        //     throw new NotFoundException("client with id: "+id+" doesnt exist");
        
        return await _deliveriesRepository.GetDelivery(id);
    }
}