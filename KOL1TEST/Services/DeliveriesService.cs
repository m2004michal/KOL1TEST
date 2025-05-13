using KOL1TEST.DTOs;
using KOL1TEST.Exceptions;
using KOL1TEST.Models;
using KOL1TEST.Repositories;


namespace KOL1TEST.Services;

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
    

    public async Task<int> CreateNewDelivery(CreateDeliveryDto dto)
    {
        var delivery = new Delivery()
        {
            DeliveryId = dto.DeliveryId,
            CustomerId = dto.CustomerId,
            DriverId = _deliveriesRepository.FindDriverByLicence(dto.LicenceNumber).Id,
            Date = DateTime.Now,
        };
        return await _deliveriesRepository.CreateNewDelivery(delivery);

    }
}