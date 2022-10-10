using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using DataAccess.IRepository;
using SalesWPFApp.Utils;

namespace SalesWPFApp.Pages;

public partial class ProfilePage
{
    private const string Save = "Save Changes";
    private const string Edit = "Edit Profile";
    private readonly IMemberRepository _memberRepository;

    public ProfilePage(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
        InitializeComponent();
        SubmitButton.Content = Edit;
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(EmailTextBox.Text) 
            || string.IsNullOrWhiteSpace(CountryTextBox.Text)
            || string.IsNullOrWhiteSpace(CompanyTextBox.Text)
            || string.IsNullOrWhiteSpace(CityTextBox.Text)
            )
        {
            MessageBox.Show("Please fill all the fields");
            return false;
        }
        if(!ValidationUtils.IsValidEmail(EmailTextBox.Text))
        {
            MessageBox.Show("Please enter a valid email");
            return false;
        }
        
        return true;
    }
    
    private void ToggleFields()
    {
        EmailTextBox.IsEnabled = !EmailTextBox.IsEnabled;
        CountryTextBox.IsEnabled = !CountryTextBox.IsEnabled;
        CompanyTextBox.IsEnabled = !CompanyTextBox.IsEnabled;
        CityTextBox.IsEnabled = !CityTextBox.IsEnabled;
    }

    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        var buttonContent = (string) SubmitButton.Content;

        if (buttonContent == Edit)
        {
            ToggleFields();
            SubmitButton.Content = Save;
        }
        else if (buttonContent == Save)
        {
            if (!ValidateInput())
            {
                return;
            }
            try
            {
                if (((Member) DataContext).IsAdmin)
                {
                    JsonUtils.SaveDefaultMember((Member) DataContext);
                }
                else
                {
                    _memberRepository.UpdateMember((Member) DataContext);
                }
                MessageBox.Show("Profile updated successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                ToggleFields();
                SubmitButton.Content = Edit;
            }
        }
    }
}