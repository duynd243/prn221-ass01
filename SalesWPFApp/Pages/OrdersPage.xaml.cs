using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using DataAccess.IRepository;

namespace SalesWPFApp.Pages;

public partial class OrdersPage : Page
{
    private readonly IMemberRepository _memberRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly Member _currentMember;

    private DateTime _fromDate;
    private DateTime _toDate;

    public OrdersPage(IProductRepository productRepository,
        IOrderRepository orderRepository,
        IMemberRepository memberRepository,
        Member currentMember
    )
    {
        _currentMember = currentMember;
        _memberRepository = memberRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        InitializeComponent();

        CreateOrderButton.Visibility = (_currentMember.IsAdmin) ? Visibility.Visible : Visibility.Collapsed;

        _fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        _toDate = DateTime.Now;
        FromDatePicker.SelectedDate = _fromDate;
        ToDatePicker.SelectedDate = _toDate;

        LoadOrders();
    }

    private void ShowAllToggleButton_Changed(object sender, RoutedEventArgs e)
    {
        if (ShowAllToggleButton.IsChecked == true)
        {
            FromDatePicker.IsEnabled = false;
            ToDatePicker.IsEnabled = false;
        }
        else
        {
            FromDatePicker.IsEnabled = true;
            ToDatePicker.IsEnabled = true;
        }

        LoadOrders();
    }

    private void LoadOrders()
    {
        IEnumerable<Order> orders;
        if (_currentMember.IsAdmin)
        {
            orders = ShowAllToggleButton.IsChecked == true
                ? _orderRepository.GetOrders()
                : _orderRepository.GetOrders(_fromDate, _toDate);
        }
        else
        {
            orders = ShowAllToggleButton.IsChecked == true
                ? _orderRepository.GetMemberOrders(_currentMember.MemberId)
                : _orderRepository.GetMemberOrders(_currentMember.MemberId, _fromDate, _toDate);
        }

        OrdersListView.ItemsSource = orders;
    }

    private void FromDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        _fromDate = FromDatePicker.SelectedDate ?? DateTime.Now;
        LoadOrders();
    }

    private void ToDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        _toDate = ToDatePicker.SelectedDate ?? DateTime.Now;
        LoadOrders();
    }

    private void CreateOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new ChooseOrderMemberWindow(_memberRepository);

        if (dialog.ShowDialog() != true) return;
        var orderMember = dialog.SelectedMember;
        if (orderMember is null)
        {
            MessageBox.Show("Please select a member");
        }
        else
        {
            var orderDialog = new CreateOrderWindow(
                _productRepository,
                _orderRepository,
                orderMember
            );

            if (orderDialog.ShowDialog() == true)
            {
                LoadOrders();
            }
        }
    }
}