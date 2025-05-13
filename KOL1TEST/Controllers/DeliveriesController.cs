using KOL1TEST.DTOs;
using KOL1TEST.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOL1TEST.Controllers;

[ApiController]
[Route("api/[controller]")]


public class DeliveriesController: ControllerBase
{
    
    private readonly IDeliveriesService _deliveriesService;
    public DeliveriesController(IDeliveriesService deliveriesService)
    {
        _deliveriesService = deliveriesService;
    }
    
    [HttpGet("{deliveryId}")]
    public async Task<IActionResult> GetDeliveries(int deliveryId)
    {
        var deliveries = await _deliveriesService.GetDelivery(deliveryId);
        return Ok(deliveries);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateNewClientAsync([FromBody] CreateDeliveryDto dto)
    {
        var deliveryId = await _deliveriesService.CreateNewDelivery(dto);
        return Created($"/api/deliveries", new { DeliveriesId = deliveryId});
    }

    
}