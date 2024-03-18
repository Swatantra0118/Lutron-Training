using Caliburn.Micro;
using LutronOrderingSystem.DataAccess;
using LutronOrderingSystem.Models;
using System;
using System.Linq;
using System.Windows;

namespace LutronOrderingSystem.ViewModels
{
    public class CartViewModel : Screen
    {
        private BindableCollection<CartItemViewModel> _cartItems;
        public BindableCollection<CartItemViewModel> CartItems
        {
            get { return _cartItems; }
            set
            {
                _cartItems = value;
                NotifyOfPropertyChange(() => CartItems);
            }
        }
        private readonly IWindowManager _windowManager;

        public CartViewModel(IWindowManager windowManager)
        {
            CartItems = new BindableCollection<CartItemViewModel>();
            _windowManager = windowManager;
        }
        public void AddToCart(ProductModel product)
        {
            try{   // Check if the product is already in the cart
            var existingItem = CartItems.FirstOrDefault(item => item.Product.ModelId == product.ModelId);
            if (existingItem != null)
                {
                    existingItem.Quantity++;
                    if (existingItem.Quantity > product.Quantity)
                {
                        existingItem.Quantity--;
                        throw new Exception("You are exceeding available item quantity !!");
                }


                }
            else
            {
                CartItems.Add(new CartItemViewModel(product, 1));
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void RemoveFromCart(ProductModel product)
        {
            var existingItem = CartItems.FirstOrDefault(item => item.Product.ModelId == product.ModelId);

            CartItems.Remove(existingItem);
        }
        
        public void ShowCart()
        {
            _windowManager.ShowDialogAsync(new CartWindowViewModel(CartItems));
        }

        public void clear()
        {
            CartItems.Clear();
        }
    }

}