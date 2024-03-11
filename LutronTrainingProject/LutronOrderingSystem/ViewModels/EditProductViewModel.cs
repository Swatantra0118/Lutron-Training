using Caliburn.Micro;
using LutronOrderingSystem.Models;

namespace LutronOrderingSystem.ViewModels
{
    public class EditProductViewModel : Screen
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

        public EditProductViewModel(ProductModel product)
        {
            Product = product;
        }

        public void SaveChanges()
        {
            TryCloseAsync(true); // Close the dialog and return true to indicate successful editing
        }

        public void Cancel()
        {
            TryCloseAsync(false); // Close the dialog and return false to indicate cancelation
        }
    }
}
