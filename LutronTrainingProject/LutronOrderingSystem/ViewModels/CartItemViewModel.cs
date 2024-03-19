﻿using Caliburn.Micro;
using LutronOrderingSystem.Helpers;
using LutronOrderingSystem.Models;

namespace LutronOrderingSystem.ViewModels
{
    public class CartItemViewModel : PropertyChangedBase
    {
        private ProductModel _product;
        public ProductModel Product
        {
            get { return _product; }
            set
            {
                _product = value;
                NotifyOfPropertyChange(() => Product);
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
            }
        }
        public ICommand RemoveCommand { get; private set; }

        public CartItemViewModel(ProductModel product, int quantity)
        {
            Product = product;
            Quantity = quantity;

            RemoveCommand = new RelayCommand(removeItem);
        }

        private void removeItem(object obj)
        {
            if (obj is CartItemViewModel c)
            {
                CartItems.Remove(c);
            }
        }
    }

}