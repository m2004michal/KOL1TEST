using Kolokwium1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers;

[ApiController]
[Route("api/[controller]")]


public class DeliveriesController: ControllerBase
{
    
    private readonly IDeliveriesService _deliveriesService;
    public DeliveriesController(IDeliveriesService deliveriesService)
    {
        _deliveriesService = deliveriesService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeliveries(int deliveryId)
    {
        var deliveries = await _deliveriesService.GetDelivery(deliveryId);
        return Ok(deliveries);
    }


    
}