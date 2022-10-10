using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using DataAccess.IRepository;
using MaterialDesignThemes.Wpf;

namespace SalesWPFApp.Pages;

public partial class ProductsPage : Page
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductsPage(IProductRepository productRepository,
        ICategoryRepository categoryRepository
    )
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        InitializeComponent();
        LoadCategories();
        LoadProducts();
    }

    private void LoadProducts()
    {
        var products = _productRepository.GetProducts().OrderByDescending(p => p.ProductId);
        ProductsListView.ItemsSource = products;
    }

    private void LoadCategories()
    {
        var categories = _categoryRepository.GetCategories();
        CategoryComboBox.ItemsSource = categories;
        CategoryComboBox.DisplayMemberPath = "CategoryName";
        CategoryComboBox.SelectedValuePath = "CategoryId";
    }

    private bool NotifyNotSelected()
    {
        if (ProductsListView.Items.Count > 0 && ProductsListView.SelectedItem == null)
        {
            MessageBox.Show("Please select a product");
            return true;
        }

        return false;
    }

    private void ProductsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedProduct = (Product) ProductsListView.SelectedItem;
        if (selectedProduct.Status)
        {
            DeleteButton.IsEnabled = true;
            RestoreButton.IsEnabled = false;
        }
        else
        {
            DeleteButton.IsEnabled = false;
            RestoreButton.IsEnabled = true;
        }
    }

    private bool ValidateInput(string action)
    {
        if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text)
            || CategoryComboBox.SelectedItem == null
            || string.IsNullOrWhiteSpace(WeightTextBox.Text)
            || string.IsNullOrWhiteSpace(UnitPriceTextBox.Text)
            || string.IsNullOrWhiteSpace(UnitsInStockTextBox.Text)
           )
        {
            MessageBox.Show("Please fill all the fields");
            return false;
        }

        var price = new decimal(0);
        if (!Decimal.TryParse(UnitPriceTextBox.Text, out price) || price <= 0)
        {
            MessageBox.Show("Unit Price is invalid!", action);
            return false;
        }

        int stock = 0;
        if (!int.TryParse(UnitsInStockTextBox.Text, out stock) || stock < 0)
        {
            MessageBox.Show("Units in stock is invalid!", action);
            return false;
        }

        return true;
    }

    private Product GetCurrentProduct()
    {
        var product = new Product()
        {
            ProductName = ProductNameTextBox.Text,
            CategoryId = (int) CategoryComboBox.SelectedValue,
            Weight = WeightTextBox.Text,
            UnitPrice = Decimal.Parse(UnitPriceTextBox.Text),
            UnitsInStock = Int32.Parse(UnitsInStockTextBox.Text),
            Status = true,
        };
        return product;
    }

    private void InsertButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!ValidateInput("Add Product")) return;
            var p = GetCurrentProduct();
            _productRepository.AddProduct(p);
            LoadProducts();
            MessageBox.Show("Product added successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Add Product");
        }
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NotifyNotSelected()) return;
        try
        {
            if (!ValidateInput("Update Product")) return;
            var product = (Product) ProductsListView.SelectedItem;
            product.ProductName = ProductNameTextBox.Text;
            product.CategoryId = (int) CategoryComboBox.SelectedValue;
            product.Weight = WeightTextBox.Text;
            product.UnitPrice = Decimal.Parse(UnitPriceTextBox.Text);
            product.UnitsInStock = Int32.Parse(UnitsInStockTextBox.Text);
            _productRepository.UpdateProduct(product);
            LoadProducts();
            MessageBox.Show("Product updated successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Update Product");
        }
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NotifyNotSelected()) return;
        try
        {
            var product = (Product) ProductsListView.SelectedItem;
            _productRepository.DeleteProduct(product.ProductId);
            DeleteButton.IsEnabled = false;
            RestoreButton.IsEnabled = true;
            LoadProducts();
            MessageBox.Show("Product deleted successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Delete Product");
        }
    }

    private void RestoreButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NotifyNotSelected()) return;
        try
        {
            var product = (Product) ProductsListView.SelectedItem;
            _productRepository.RestoreProduct(product.ProductId);
            DeleteButton.IsEnabled = true;
            RestoreButton.IsEnabled = false;
            LoadProducts();
            MessageBox.Show("Product restored successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Restore Product");
        }
    }
}