using System.Windows;
using BusinessObject;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWPFApp.Pages;

namespace SalesWPFApp
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<PRN221_Ass01_FStoreContext>(option =>
            {
                option.UseSqlServer("server=.;database=PRN221_Ass01_FStore;uid=sa;pwd=0204;");
            });
            services.AddScoped(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton<MemberDAO>();
            services.AddSingleton<ProductDAO>();
            services.AddSingleton<CategoryDAO>();
            services.AddSingleton<OrderDAO>();
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ProfilePage>();
            services.AddSingleton<ProductsPage>();
            services.AddSingleton<MembersPage>();
            services.AddSingleton<OrdersPage>();
            services.AddSingleton<ChooseOrderMemberWindow>();
            services.AddSingleton<CreateOrderWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }
}