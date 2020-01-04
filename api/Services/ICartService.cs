using System.Collections.Generic;
using Models;

namespace Services
{
  public interface ICartService
  {
    double Total();
     IEnumerable<CartItem> Items();
  }
}