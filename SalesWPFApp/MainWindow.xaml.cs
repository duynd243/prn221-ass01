using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using DataAccess.IRepository;
using SalesWPFApp.CustomControls;
using SalesWPFApp.Pages;
using SalesWPFApp.Utils;

namespace SalesWPFApp;

public partial class MainWindow
{
    private readonly Member _currentMember;
    private readonly IMemberRepository _memberRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOrderRepository _orderRepository;

    public MainWindow(
        IMemberRepository memberRepository,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IOrderRepository orderRepository,
        Member currentMember
    )
    {
        _currentMember = currentMember;
        _memberRepository = memberRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _categoryRepository = categoryRepository;
        InitializeComponent();

        if (!_currentMember.IsAdmin)
        {
            ProductsSidebarItem.Visibility = Visibility.Collapsed;
            MembersSidebarItem.Visibility = Visibility.Collapsed;
        }

        helloText.Text = _currentMember.Email;
        Sidebar.SelectedIndex = 0;
        LogoutListBox.SelectionChanged += (_, _) =>
        {
            if (LogoutListBox.SelectedIndex == -1) return;
            LogoutListBox.SelectedIndex = -1;
            Close();
        };
        Closing += (_, e) =>
        {
            if (LogoutConfirm() == MessageBoxResult.OK)
            {
                Hide();
            }
            else
            {
                e.Cancel = true;
            }
        };
    }


    private MessageBoxResult LogoutConfirm()
    {
        return MessageBox.Show("Do want to logout?", "Exit", MessageBoxButton.OKCancel);
    }


    private void Sidebar_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = Sidebar.SelectedItem as SidebarItem;
        switch (selected?.Page)
        {
            case PAGES.PROFILE:
                PageFrame.Navigate(new ProfilePage(_memberRepository)
                {
                    DataContext = _currentMember
                });
                break;
            case PAGES.PRODUCTS:
                PageFrame.Navigate(new ProductsPage(_productRepository, _categoryRepository)
                {
                    DataContext = _currentMember
                });
                break;
            case PAGES.MEMBERS:
                PageFrame.Navigate(new MembersPage(_memberRepository));
                break;
            case PAGES.ORDERS:
                PageFrame.Navigate(
                    new OrdersPage(
                        _productRepository,
                        _orderRepository,
                        _memberRepository,
                        _currentMember
                    ));
                break;
        }
    }
}