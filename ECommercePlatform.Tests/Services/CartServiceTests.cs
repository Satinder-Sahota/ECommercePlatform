using ECommercePlatform.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePlatform.Tests.Services
{
    public class CartServiceTests
    {
        private readonly CartService _cartService;
        public CartServiceTests()
        {
            // Arrange: Create an instance of CartService for testing
            _cartService = new CartService();
        }

        [Fact]
        public void AddToCart_ShouldAddNewItem_WhenItemDoesNotExist()
        {
            // Arrange: Define a product to add
            int productId = 1;
            string productName = "Test Product";
            decimal price = 10.00m;
            int quantity = 2;

            // Act: Add the product to the cart
            _cartService.AddToCart(productId, productName, price, quantity);    

            // Assert: Verify the product is added to the cart
            var cartItems = _cartService.GetCartItems();
            Assert.Single(cartItems);// Ensure only one item is added
            Assert.Equal(productId,cartItems.First().ProductId);// Verify product ID
            Assert.Equal(quantity, cartItems.First().Quantity);// Verify quanitity

        }
        [Fact]
        public void AddToCart_ShouldUpdateQuantity_WhenItemAlreadyExists()
        {
            // Arrange
            int productId = 1;
            string productName = "Test Product";
            decimal price = 10.00m;
            int intialQuantity = 2;
            int additionalQuantity = 3;

            // Add the product intially
            _cartService.AddToCart(productId, productName, price, intialQuantity);

            // Act: Add the same product again with additional quantity
            _cartService.AddToCart(productId,productName, price, additionalQuantity);

            // Assert: Verify the quantity is updated
            var cartItems = _cartService.GetCartItems();
            Assert.Single(cartItems); // Still one item in the cart
            Assert.Equal(intialQuantity+additionalQuantity, cartItems.First().Quantity);
        }
        [Fact]
        public void UpdateCartItem_ShouldChangeQuantity()
        {
            // Arrange
            int productId = 1;
            string productName = "Test Product";
            decimal price = 10.00m;
            int initialQuantity = 2;
            int updatedQuantity = 5;

            // Add an item to the cart
            _cartService.AddToCart(productId, productName, price, initialQuantity);

            // Act: Update the quantity of the product
            _cartService.UpdateCartItem(productId, updatedQuantity);

            // Assert: Verify the quantity is updated
            var cartItems = _cartService.GetCartItems();
            Assert.Single(cartItems);
            Assert.Equal(updatedQuantity, cartItems.First().Quantity);
        }
        [Fact]
        public void RemoveFromCart_ShouldRemoveItem()
        {
            // Arrange
            int productId = 1;
            string productName = "Test Product";
            decimal price = 10.00m;
            int quantity = 2;

            // Add an item to the cart
            _cartService.AddToCart(productId, productName, price, quantity);

            // Act: Remove the item from the cart
            _cartService.RemoveFromCart(productId);

            // Assert: Verify the cart is empty
            var cartItems = _cartService.GetCartItems();
            Assert.Empty(cartItems);
        }
        [Fact]
        public void ClearCart_ShouldEmptyTheCart()
        {
            // Arrange: Add multiple items to the cart
            _cartService.AddToCart(1, "Product 1", 10.00m, 2);
            _cartService.AddToCart(2, "Product 2", 20.00m, 1);

            // Act: Clear the cart
            _cartService.ClearCart();

            // Assert: Verify the cart is empty
            var cartItems = _cartService.GetCartItems();
            Assert.Empty(cartItems);
        }

        [Fact]
        public void GetTotalAmount_ShouldReturnCorrectSum()
        {
            // Arrange: Add multiple items to the cart
            _cartService.AddToCart(1, "Product 1", 10.00m, 2); // Total: 20.00
            _cartService.AddToCart(2, "Product 2", 20.00m, 1); // Total: 20.00

            // Act: Get the total amount
            var totalAmount = _cartService.GetTotalAmount();

            // Assert: Verify the total amount
            Assert.Equal(40.00m, totalAmount);
        }
    }
}
