using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using DataAccess.IRepository;
using SalesWPFApp.Utils;

namespace SalesWPFApp.Pages;

public partial class MembersPage : Page
{
    private readonly IMemberRepository _memberRepository;

    public MembersPage(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
        InitializeComponent();
        LoadMembers();
    }

    private void LoadMembers()
    {
        var members = _memberRepository.GetMembers();
        MembersListView.ItemsSource = members;
    }

    private bool ValidateInput(string action)
    {
        if (string.IsNullOrWhiteSpace(EmailTextBox.Text)
            || string.IsNullOrWhiteSpace(CompanyNameTextBox.Text)
            || string.IsNullOrWhiteSpace(CityTextBox.Text)
            || string.IsNullOrWhiteSpace(CountryTextBox.Text)
            || string.IsNullOrWhiteSpace(PasswordTextBox.Password)
           )
        {
            MessageBox.Show("Please fill all the fields");
            return false;
        }

        if (!ValidationUtils.IsValidEmail(EmailTextBox.Text))
        {
            MessageBox.Show("Please enter a valid email");
            return false;
        }

        return true;
    }

    private Member GetCurrentMember()
    {
        var member = new Member
        {
            Email = EmailTextBox.Text,
            CompanyName = CompanyNameTextBox.Text,
            City = CityTextBox.Text,
            Country = CountryTextBox.Text,
            Password = PasswordTextBox.Password,
            Status = true
        };
        return member;
    }

    private bool NotifyNotSelected()
    {
        if (MembersListView.Items.Count > 0 && MembersListView.SelectedItem == null)
        {
            MessageBox.Show("Please select a member");
            return true;
        }

        return false;
    }

    private void MembersListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedMember = (Member) MembersListView.SelectedItem;
        PasswordTextBox.Password = selectedMember.Password;
        if (selectedMember.Status)
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

    private void InsertButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!ValidateInput("Add Member"))
                return;
            var member = GetCurrentMember();
            _memberRepository.AddMember(member);
            LoadMembers();
            MessageBox.Show("Member added successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Add Member");
        }
    }

    private void RestoreButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NotifyNotSelected())
            return;
        try
        {
            var selectedMember = (Member) MembersListView.SelectedItem;
            _memberRepository.RestoreMember(selectedMember.MemberId);
            DeleteButton.IsEnabled = true;
            RestoreButton.IsEnabled = false;
            LoadMembers();
            MessageBox.Show("Member restored successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Restore Member");
        }
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NotifyNotSelected())
            return;
        try
        {
            var selectedMember = (Member) MembersListView.SelectedItem;
            _memberRepository.DeleteMember(selectedMember.MemberId);
            DeleteButton.IsEnabled = false;
            RestoreButton.IsEnabled = true;
            LoadMembers();
            MessageBox.Show("Member deleted successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Delete Member");
        }
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NotifyNotSelected()) return;
        try
        {
            if (!ValidateInput("Update Member"))
                return;
            var member = (Member) MembersListView.SelectedItem;
            member.Email = EmailTextBox.Text;
            member.CompanyName = CompanyNameTextBox.Text;
            member.City = CityTextBox.Text;
            member.Country = CountryTextBox.Text;
            member.Password = PasswordTextBox.Password;
            _memberRepository.UpdateMember(member);
            LoadMembers();
            MessageBox.Show("Member updated successfully");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Update Member");
        }
    }
}