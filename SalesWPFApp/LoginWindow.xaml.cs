using System;
using System.Windows;
using BusinessObject;
using DataAccess.IRepository;
using SalesWPFApp.Utils;

namespace SalesWPFApp
{
    public partial class LoginWindow : Window
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;

        private void ShowMainWindow(Member member)
        {
            PasswordBox.Password = "";
            Hide();
            var mainWindow = new MainWindow(_memberRepository, _productRepository, _categoryRepository,
                _orderRepository, member);
            mainWindow.Closed += (sender, args) => Show();
            mainWindow.Show();
        }

        public LoginWindow(IMemberRepository memberRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository
        )
        {
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            InitializeComponent();

            // For testing
            EmailTextBox.Text = "admin@fstore.com";
            PasswordBox.Password = "admin@@";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var password = PasswordBox.Password;

            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter email and password");
                EmailTextBox.Focus();
                return;
            }

            if (!ValidationUtils.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email");
                EmailTextBox.Focus();
                return;
            }

            try
            {
                var defaultMember = JsonUtils.GetDefaultMember();
                if (defaultMember is null) return;
                if (email == defaultMember.Email && password == defaultMember.Password)
                {
                    defaultMember.IsAdmin = true;
                    ShowMainWindow(defaultMember);
                    return;
                }

                var member = _memberRepository.Login(email, password);
                if (member is not null)
                {
                    if (!member.Status)
                    {
                        MessageBox.Show("Your account is locked");
                        return;
                    }

                    member.IsAdmin = false;
                    ShowMainWindow(member);
                    return;
                }

                MessageBox.Show("Invalid email or password!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}