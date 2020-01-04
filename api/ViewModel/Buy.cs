using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace api.ViewModel
{
    public class Buy
    {
        public virtual ICard Card { get; set; }

        public virtual IAddressInfo AddressInfo { get; set; }
        
    }
}
