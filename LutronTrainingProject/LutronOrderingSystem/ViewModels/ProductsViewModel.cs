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
using static LutronOrderingSystem.Models.ProductModel;

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
        public ICommand ShowControlStationsCommand { get; private set; }
        public ICommand ShowEnclosuresCommand { get; private set; }


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

            ShowControlStationsCommand = new RelayCommand(ShowControlStations);
            ShowEnclosuresCommand = new RelayCommand(ShowEnclosures);
        }

        private void ShowControlStations(object obj)
        {
            IsControlStationsVisible = true;
            IsEnclosuresVisible = false;
        }

        private void ShowEnclosures(object obj)
        {
            IsControlStationsVisible = false;
            IsEnclosuresVisible = true;
        }

        private bool _isControlStationsVisible = true;
        public bool IsControlStationsVisible
        {
            get { return _isControlStationsVisible; }
            set
            {
                _isControlStationsVisible = value;
                NotifyOfPropertyChange(nameof(IsControlStationsVisible));
            }
        }

        private bool _isEnclosuresVisible;
        public bool IsEnclosuresVisible
        {
            get { return _isEnclosuresVisible; }
            set
            {
                _isEnclosuresVisible = value;
                NotifyOfPropertyChange(nameof(IsEnclosuresVisible));
            }
        }

        private bool CanShowWindow(object obj)
        {
            return true;
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
            //if (obj is productViewModel productToDelete)
            //{
            //    databaseManager.DeleteProduct(productToDelete.Product.ModelId);
            //}

            if (obj is int modelId)
            {
                databaseManager.DeleteProduct(modelId);
                if (IsControlStationsVisible)
                {
                    var controlStation = ControlStations.FirstOrDefault(cs => cs.ModelId == modelId);
                    if (controlStation != null)
                    {
                        ControlStations.Remove(controlStation);
                    }
                }
                else if (IsEnclosuresVisible)
                {
                    var enclosure = Enclosures.FirstOrDefault(e => e.ModelId == modelId);
                    if (enclosure != null)
                    {
                        Enclosures.Remove(enclosure);
                    }
                }

            }

        }

        private async void EditProduct(object obj)
        {
            //if (obj is productViewModel productViewModel)
            //{
            //    EditProductViewModel editProductViewModel = new EditProductViewModel(productViewModel.Product);
            //    WindowManager windowManager = new WindowManager();
            //    bool? result = await windowManager.ShowDialogAsync(editProductViewModel);

            //    if (result.HasValue && result.Value)
            //    {
            //        databaseManager.UpdateProduct(editProductViewModel.Product);
            //    }
            //}
            if (obj != null)
            {
                productViewModel productViewModel = null;

                // Check if obj is a ControlStationModel or EnclosureModel
                if (obj is ControlStationModel controlStation)
                {
                    // Convert ControlStationModel to productViewModel
                    productViewModel = new productViewModel(new ProductModel
                    {
                        ModelId = controlStation.ModelId,
                        ModelDisplayString = controlStation.ModelDisplayString,
                        Description = controlStation.Description,
                        NumberOfButtons = controlStation.NumberOfButtons,
                        Quantity = controlStation.Quantity
                        // Add other properties as needed
                    });
                }
                else if (obj is EnclosureModel enclosure)
                {
                    // Convert EnclosureModel to productViewModel
                    productViewModel = new productViewModel(new ProductModel
                    {
                        ModelId = enclosure.ModelId,
                        ModelDisplayString = enclosure.ModelDisplayString,
                        Description = enclosure.Description,
                        Quantity = enclosure.Quantity,
                        MountType = (MountTypeEnum)Enum.Parse(typeof(MountTypeEnum), enclosure.MountType)
                        // Add other properties as needed
                    });
                }

                if (productViewModel != null)
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
        }
        private async void AddProductAsync(object obj)
        {
            AddProductViewModel addProductViewModel = new AddProductViewModel();
            WindowManager windowManager = new WindowManager();
            bool? result = await windowManager.ShowDialogAsync(addProductViewModel);

            if (result.HasValue && result.Value)
            {
                var newProduct = addProductViewModel.Product;

                databaseManager.AddProduct(newProduct);

                LoadControlStations();
                LoadEnclosures();

                NotifyOfPropertyChange(nameof(ControlStations));
                NotifyOfPropertyChange(nameof(Enclosures));
            }

        }

        public Array ProductCategoryValues => Enum.GetValues(typeof(ProductModel.ProductCategory));

        public Array MountTypeValues => Enum.GetValues(typeof(ProductModel.MountTypeEnum));




    }

}
