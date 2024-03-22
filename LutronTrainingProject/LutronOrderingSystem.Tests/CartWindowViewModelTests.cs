using Caliburn.Micro;
using LutronOrderingSystem.Models;
using LutronOrderingSystem.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LutronOrderingSystem.Tests
{
    [TestFixture]
    public class CartWindowViewModelTests
    {
        [Test]
        public void AddToCartExistingTest()
        {
            //Arrange
            var productModel = new ProductModel { ModelId = 2, Quantity = 5 };
            var existingCartItem = new CartItemViewModel(productModel, 3);
            var cartItems = new BindableCollection<CartItemViewModel> { existingCartItem };

            var windowManagerMock = new Mock<IWindowManager>();
            var viewModel = new CartViewModel(windowManagerMock.Object) { CartItems = cartItems };

            //Act
            viewModel.AddToCart(productModel);
            var updatedCartItem = cartItems.Single(item => item.Product.ModelId == productModel.ModelId);
        }


        [Test]
        public void AddToCartNewTest()
        {
            //Arrange
            var productModel = new ProductModel { ModelId = 2, Quantity = 3 };
            var cartItems = new BindableCollection<CartItemViewModel>();

            var windowManagerMock = new Mock<IWindowManager>();
            var viewModel = new CartViewModel(windowManagerMock.Object) { CartItems = cartItems };

            //Act
            viewModel.AddToCart(productModel);
        }

        [Test]
        public void RemoveItemTest()
        {
            //Arrange
            var cartItems = new List<CartItemViewModel>
            {
                new CartItemViewModel(new ProductModel { ModelId = 2 }, 1),
                new CartItemViewModel(new ProductModel { ModelId = 3 }, 1)
            };
            var viewModel = new CartWindowViewModel(new BindableCollection<CartItemViewModel>(cartItems));

            //Act
            viewModel.removeItem(cartItems.First());

        }

        [Test]
        public void CheckoutTest()
        {
            //Arrange
            var cartItems = new List<CartItemViewModel>
            {
                new CartItemViewModel(new ProductModel { ModelId = 2 }, 1),
                new CartItemViewModel(new ProductModel { ModelId = 3 }, 1)
            };
            var viewModel = new CartWindowViewModel(new BindableCollection<CartItemViewModel>(cartItems));

            //Act
            viewModel.Checkout(null);
        }
    }
}
