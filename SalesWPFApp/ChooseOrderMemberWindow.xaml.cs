using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using DataAccess.IRepository;

namespace SalesWPFApp;

public partial class ChooseOrderMemberWindow : Window
{
    public Member? SelectedMember { get; set; }
    private readonly IMemberRepository _memberRepository;

    public ChooseOrderMemberWindow(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
        InitializeComponent();
        LoadMembers();
    }

    private void LoadMembers()
    {
        var members = _memberRepository.GetMembers();
        MembersCombobox.ItemsSource = members;
        MembersCombobox.DisplayMemberPath = "Email";
    }

    private void MembersCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedMember = (Member)MembersCombobox.SelectedItem;
        SelectedMember = selectedMember;
    }
    
    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void CancelButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}