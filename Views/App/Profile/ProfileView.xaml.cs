using CommunityToolkit.Maui.Views;
using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Views.Account;
using LiquiCycle_FuncionarioPosto.Views.App.Profile;
using System.Text.Json;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class ProfileView : ContentPage
{
    public FuncionarioPostoDto funcionario;
    public ProfileView()
	{
		InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        funcionario = JsonSerializer.Deserialize<FuncionarioPostoDto>(jsonString);
        lblNome.Text = funcionario.Usuario.Nome;
    }

    private void EditProfile(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new EditProfile());
    }
    private async void ResetPassword(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmação", "Tem certeza que desja redefinir sua senha?", "Confirmar", "Cancelar");
        if (answer)
        {
            await Navigation.PushModalAsync(new ResetPassword(funcionario));
        }
    }
    private void SairConta(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        Preferences.Remove("usuarioLogado");
        Application.Current.MainPage = new NavigationPage(new LoginView());

        popup.Close();
    }
}