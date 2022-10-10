using System.Windows;
using BusinessObject;
using DataAccess.IRepository;

namespace SalesWPFApp;

public partial class CreateOrderWindow : Window
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly Member _orderMember;

    public CreateOrderWindow(IProductRepository productRepository,
        IOrderRepository orderRepository,
        Member orderMember)
    {
        _orderMember = orderMember;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        InitializeComponent();
        OrderMemberLabel.Content = $"Order For {_orderMember.Email}";
        LoadProducts();
    }

    private void LoadProducts()
    {
        ProductComboBox.ItemsSource = _productRepository.GetProducts();
        ProductComboBox.DisplayMemberPath = "ProductName";
        ProductComboBox.SelectedValuePath = "ProductId";
    }
}