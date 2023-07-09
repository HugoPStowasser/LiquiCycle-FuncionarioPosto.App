using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class UsinasView : ContentPage
{
	public UsinasView()
	{
		InitializeComponent();
        
    }

    private void enviaUsina(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new EnviarUsina());
    }

}