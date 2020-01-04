// CartControllerTest.cs
using Services;
using Moq;
using NUnit.Framework;
using api.Controllers;
using System.Linq;
using System.Collections.Generic;
using api.ViewModel;
using Models;

namespace api.test
{
    public class Tests
  {
      private CartController _controller;
      private Mock<IPaymentService> _paymentServiceMock;
      private Mock<ICartService> _cartServiceMock;
      private Mock<IShipmentService> _shipmentServiceMock;
      private List<CartItem> _items;
      private Mock<Buy> _buy;

        [SetUp]
      public void Setup()
      {

          _cartServiceMock = new Mock<ICartService>();
          _paymentServiceMock = new Mock<IPaymentService>();
          _shipmentServiceMock = new Mock<IShipmentService>();

          // arrange
          _buy = new Mock<Buy>();
          _buy.Setup(add => add.AddressInfo).Returns(new IAddressInfo { Address = "Test"} );
          _buy.Setup(add => add.Card).Returns(new ICard() { CardNumber = "123" }); 
          
          var cartItemMock = new Mock<CartItem>();
          cartItemMock.Setup(item => item.Price).Returns(10);
            
          _items = new List<CartItem>()
          {
              cartItemMock.Object
          };

          _cartServiceMock.Setup(c => c.Items()).Returns(_items.AsEnumerable());

          _controller = new CartController(_cartServiceMock.Object, _paymentServiceMock.Object, _shipmentServiceMock.Object);
      }

      [Test]
      public void ShouldReturnCharged()
      {
          _paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), _buy.Object.Card )).Returns(true);
          
          // act
          var result = _controller.CheckOut(_buy.Object);
          
          // assert
          _shipmentServiceMock.Verify(s => s.Ship(_buy.Object.AddressInfo, _items.AsEnumerable()), Times.Once());

          //Assert
          Assert.AreEqual("charged", result);
      }

      [Test]
      public void ShouldReturnNotCharged() 
      {
          _paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), _buy.Object.Card)).Returns(false);

          // act
          var result = _controller.CheckOut(_buy.Object);//(cardMock.Object, addressInfoMock.Object);

          // assert
          _shipmentServiceMock.Verify(s => s.Ship(_buy.Object.AddressInfo, _items.AsEnumerable()), Times.Never());
          
          //Assert
          Assert.AreEqual("not charged", result);
      }
  }
}