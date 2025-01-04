using ECommercePlatform.Models;

namespace ECommercePlatform.Services
{
    public class CartService
    {
        private readonly List<CartItem> _cartItems = new();
        public IEnumerable<CartItem> GetCartItems()=> _cartItems;
        public void AddToCart(int productId, string productName, decimal price, int quantity)
        { 
            var existingItem = _cartItems.FirstOrDefault(item =>item.ProductId == productId);
            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
            {
                _cartItems.Add(new CartItem { 
                ProductId=productId,
                ProductName=productName,
                Price=price,
                Quantity=quantity
                });
            }
        }
        public void UpdateCartItem(int productId, int quantity)
        {
            var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }
        public void RemoveFromCart(int productId) 
        {
            var item = _cartItems.FirstOrDefault(item => item.ProductId==productId);
            if(item!= null)
                _cartItems.Remove(item);
        }
        public void ClearCart()
        {
            _cartItems.Clear();
        }
        public decimal GetTotalAmount() => _cartItems.Sum(item => item.Price*item.Quantity);
    }
}
