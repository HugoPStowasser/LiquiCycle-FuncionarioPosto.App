using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class AddLiquido : ContentPage
{
    public AddLiquido()
    {
        InitializeComponent();

    }

    private void Return(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}