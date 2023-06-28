namespace LiquiCycle_FuncionarioPosto.Views.App.Profile;

public partial class EditProfile : ContentPage
{
	public EditProfile()
	{
		InitializeComponent();
	}
    
    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SalvarUser(object sender, EventArgs e)
    {

    }
}