using Caliburn.Micro;
using LutronOrderingSystem.DataAccess;
using LutronOrderingSystem.Helpers;
using LutronOrderingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand AddCommand { get; private set; }
        public ICommand MinusCommand { get; private set; }
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5018/api/Order";


        public CartWindowViewModel(BindableCollection<CartItemViewModel> cartItems)
        {
            CartItems = cartItems;
            RemoveCommand = new RelayCommand(removeItem);
            CheckoutCommand = new RelayCommand(Checkout);
            AddCommand = new RelayCommand(add);
            MinusCommand = new RelayCommand(minus);
            databasemanager = new DatabaseManager();
            _httpClient = new HttpClient();

        }

        private void add(object obj)
        {
            if (obj is CartItemViewModel c)
            {
                c.Quantity++;
                if (c.Quantity > c.Product.Quantity)
                {
                    MessageBox.Show("Desired Quantity unavailable !!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    c.Quantity--;
                }
            }
        }

        private void minus(object obj)
        {
            if (obj is CartItemViewModel c)
            {
                c.Quantity--;
                if (c.Quantity < 1)
                {
                    MessageBox.Show("Cannot further reduce the Quantity !!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    c.Quantity++;
                }
            }
        }

        public async void PlaceOrderAsync(List<CartItemDTO> cartItems)
        {
            try
            {
                // Serialize list to JSON
                string cartItemsJson = JsonConvert.SerializeObject(cartItems);

                var content = new StringContent(cartItemsJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiBaseUrl, content);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");

            }
        }
        private void removeItem(object obj)
        {
            if (obj is CartItemViewModel c)
            {
                CartItems.Remove(c);
            }
        }



        private void Checkout(object obj)
        {
            if (CartItems.Count != 0)
            {
                MessageBox.Show("Your order is successfully placed !!", "Order Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Your cart is empty !!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            var Itemz = new List<CartItemDTO>();
            foreach (var c in CartItems)
            {
                var newItem = new CartItemDTO();
                newItem.Product = c.Product;
                newItem.Quantity = c.Quantity;
                Itemz.Add(newItem);
                ProductModel p = databasemanager.GetProductById(c.Product.ModelId);
                p.Quantity -= c.Quantity;
                databasemanager.UpdateProduct(p);
            }
            PlaceOrderAsync(Itemz);
            CartItems.Clear();


        }
    }

}