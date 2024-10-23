using ChipShop.Controllers;
using ChipShop.Controllers.Interfaces;
using ChipShop.Enums;
using ChipShop.Models;

namespace ChipShop.Test.BasicKitchenTests
{
    public class BasicOrderControllerTests
    {
        public IOrderController _orderController;

        public BasicOrderControllerTests()
        {
            _orderController = new BasicOrderController();
        }

        [Fact]
        public void PrintReceipt_ShouldHaveCorrectNumberOfItems()
        {
            // Arrange
            var customerOrder = new CustomerOrder
            {
                // We set up some data to test our method
                Id = 1,
                OrderList =
                [
                    new MenuItem
                    {
                        Id = 1,
                        Name = "Burger",
                        Size = Size.Medium,
                        BasePrice = 5.5m,
                    },
                    new MenuItem
                    {
                        Id = 2,
                        Name = "Chips",
                        Size = Size.Large,
                        BasePrice = 3,
                    },
                ]
            };

            // Keep a record of what we expect the result to be
            // Our receipt shows 3 things:
            //                  Order Number,
            //                  The list of things ordered,
            //                  The total cost of the order
            // We put 2 items on the order, so the expected length is 4
            var expectedResult = 4;

            // Act
            // Run the method we want to test, and save the result to a variable
            var result = _orderController.PrintReceipt(customerOrder);

            // Assert
            // Use Assert calls to test the results!
            Assert.Equal(expectedResult, result.Count);
        }

        [Fact]
        public void PrintReceipt_ShouldListOrderNumberOnLineOne()
        {
            // Arrange

            // Act

            // Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void PrintReceipt_ShouldListAllOrderedItems()
        {
            // Arrange

            // Act

            // Assert
            throw new NotImplementedException();

        }

        [Fact]
        public void PrintReceipt_ShouldThrowErrorForEmptyOrder()
        {
            // Arrange

            // Act

            // Assert

            // Hint: use `Assert.Throws()` to test this one
            throw new NotImplementedException();

        }

        [Theory]
        // First number is the expected total, each number thereafter is the prices you want to list
        // Try to create multiple scenarios and edge cases to get the most out of a [Theory] test!
        [InlineData(10, 1, 2, 3, 4)]
        public void CalculateOrderCost_ShouldReturnCorrectTotal(decimal expectedResult, params decimal[] orderPrices)
        {
            // Arrange

            // Act

            // Assert
            throw new NotImplementedException();

        }
    }
}