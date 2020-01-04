using System;

namespace Models 
{
    public class IAddressInfo
    {
        public virtual string Street { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string PhoneNumber { get; set; }
    }

    public class ICard
    {
        public virtual string CardNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime ValidTo { get; set; }
    }

    public class CartItem
    {
        public virtual string ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual double Price { get; set; }
    }
}