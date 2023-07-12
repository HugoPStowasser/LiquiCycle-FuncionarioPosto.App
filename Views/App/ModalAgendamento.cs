using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class ModalAgendamento : ContentPage
{
    public ModalAgendamento()
    {
        InitializeComponent();

    }

    private void Return(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}