using Caliburn.Micro;
using LutronOrderingSystem.DataAccess;
using LutronOrderingSystem.Helpers;
using LutronOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LutronOrderingSystem.ViewModels
{
    public class ProductsViewModel : Screen
    {
        private ObservableCollection<productViewModel> _products;
        public ObservableCollection<productViewModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(nameof(Products));
            }
        }

        private readonly DatabaseManager databaseManager;
        public ObservableCollection<EnclosureModel> Enclosures { get; set; }
        public ObservableCollection<ControlStationModel> ControlStations { get; set; }

        public ICommand AddCommand { get;  set; }
        public ICommand EditCommand { get;  set; }
        public ICommand DeleteCommand { get;  set; }


        public ProductsViewModel()
        {
            databaseManager = new DatabaseManager();
            Enclosures = new ObservableCollection<EnclosureModel>();
            LoadEnclosures();
            ControlStations = new ObservableCollection<ControlStationModel>();
            LoadControlStations();
            InitializeCommands();
        }
        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddProductAsync,CanShowWindow);
            EditCommand = new RelayCommand(EditProduct, CanShowWindow);
            DeleteCommand = new RelayCommand(DeleteProduct);
        }

        private void LoadControlStations()
        {
            DataTable dataTable = databaseManager.GetProducts();
            ControlStations.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                // Check if NumberOfButtons is DBNull or not
                if (!Convert.IsDBNull(row["NumberOfButtons"]))
                {
                    ControlStationModel controlStation = new ControlStationModel(
                        Convert.ToInt32(row["ModelId"]),
                        row["ModelDisplayString"].ToString(),
                        row["Description"].ToString(),
                        Convert.ToInt32(row["NumberOfButtons"]),
                        Convert.ToInt32(row["Quantity"])
                    );
                    ControlStations.Add(controlStation);
                }
            }
        }


        private void LoadEnclosures()
        {
            DataTable dataTable = databaseManager.GetProducts();
            Enclosures.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                if (!Convert.IsDBNull(row["MountType"]))
                {
                    EnclosureModel enclosure = new EnclosureModel(
                      Convert.ToInt32(row["ModelId"]),
                      row["ModelDisplayString"].ToString(),
                      row["Description"].ToString(),
                      row["MountType"].ToString(),
                      Convert.ToInt32(row["Quantity"])
                  );
                    Enclosures.Add(enclosure);
                }
            }
        }


        private void DeleteProduct(object obj)
        {
            if (obj is productViewModel productToDelete)
            {
                databaseManager.DeleteProduct(productToDelete.Product.ModelId);
            }
        }

        private async void EditProduct(object obj)
        {
            if (obj is productViewModel productViewModel)
            {
                EditProductViewModel editProductViewModel = new EditProductViewModel(productViewModel.Product);
                WindowManager windowManager = new WindowManager();
                bool? result = await windowManager.ShowDialogAsync(editProductViewModel);

                if (result.HasValue && result.Value)
                {
                    databaseManager.UpdateProduct(editProductViewModel.Product);
                }
            }
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }
        private async void AddProductAsync(object obj)
        {
            AddProductViewModel addProductViewModel = new AddProductViewModel();
            WindowManager windowManager = new WindowManager();
            bool? result = await windowManager.ShowDialogAsync(addProductViewModel);

            if (result.HasValue && result.Value)
            {
                //addProductViewModel.Product.ModelId = 999;
                var newViewModel = new productViewModel
                {
                    Product = addProductViewModel.Product
                };
                databaseManager.AddProduct(newViewModel.Product);
            }

        }


    }

}
