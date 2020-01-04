using System.Collections.Generic;
using Models;

namespace Services
{
  public interface IShipmentService
  {
    void Ship(IAddressInfo info, IEnumerable<CartItem> items);
  }
}