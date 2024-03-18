using Caliburn.Micro;
using LutronOrderingSystem.DataAccess;
using LutronOrderingSystem.Helpers;
using LutronOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace LutronOrderingSystem.ViewModels
{
    public class CartWindowViewModel : Screen
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
        private readonly DatabaseManager databasemanager;
        public ICommand CheckoutCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public CartWindowViewModel(BindableCollection<CartItemViewModel> cartItems)
        {
            CartItems = cartItems;
            RemoveCommand = new RelayCommand(removeItem);
            CheckoutCommand = new RelayCommand(Checkout);
            databasemanager = new DatabaseManager();
        }

        private void removeItem(object obj)
        {
            if(obj is CartItemViewModel c)
            {
                CartItems.Remove(c);
            }
        }

        private void Checkout(object obj)
        {
            //WindowManager _windowManager = new WindowManager();
            //_windowManager.ShowDialogAsync(new CheckoutConfirmationViewModel());
            if (CartItems.Count != 0)
            {
                MessageBox.Show("Your order is successfully placed !!", "Order Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Your cart is empty !!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (var c in CartItems)
                {
                    ProductModel p = databasemanager.GetProductById(c.Product.ModelId);
                    p.Quantity -= c.Quantity;
                    databasemanager.UpdateProduct(p);
                }
                CartItems.Clear();

            
        }
    }
    public class CheckoutConfirmationViewModel : Screen
    {
        public void Confirm()
        {
            TryCloseAsync(true);
        }

        public void Cancel()
        {
            TryCloseAsync(false);
        }
    }
}