using api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CartController 
  {
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;
    private readonly IShipmentService _shipmentService;

    public CartController(
      ICartService cartService,
      IPaymentService paymentService,
      IShipmentService shipmentService
    ) 
    {
      _cartService = cartService;
      _paymentService = paymentService;
      _shipmentService = shipmentService;
    }

    [HttpPost]
    public string CheckOut(Buy buy) 
    {
        var result = _paymentService.Charge(_cartService.Total(), buy.Card);
        if (!result) return "not charged";
        _shipmentService.Ship(buy.AddressInfo, _cartService.Items());
        return "charged";

    }
  }
}