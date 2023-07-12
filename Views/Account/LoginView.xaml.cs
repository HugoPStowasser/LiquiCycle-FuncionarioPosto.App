using CommunityToolkit.Maui.Views;
using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Requests;
using LiquiCycle_FuncionarioPosto.Services;
using LiquiCycle_FuncionarioPosto.Views.App;
using System.Text.Json;

namespace LiquiCycle_FuncionarioPosto.Views.Account;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }
    private async void LoginPost(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        if (EntryLogin.Text == null || EntrySenha.Text == null)
        {
            await DisplayAlert("Erro", "Preencha todos os campos", "OK");
            popup.Close();
            return;
        }

        var apiService = new ApiService();

        LoginRequest loginRequest = new LoginRequest
        {
            Login = EntryLogin.Text,
            Senha = EntrySenha.Text,
            PerfilEnum = PerfilEnum.FuncionarioPosto
        };
        try
        {
            Task<LoginDto> loginTask = apiService.PostAsync<LoginRequest, LoginDto>("manter-conta/login", loginRequest);
            Task timeoutTask = Task.Delay(TimeSpan.FromSeconds(15));

            Task completedTask = await Task.WhenAny(loginTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                await DisplayAlert("Erro", "Não foi possível fazer login", "OK");
                popup.Close();
                return; 
            }

            LoginDto loginResponse = await loginTask;

            if (loginResponse != null)
            {
                string jsonString = JsonSerializer.Serialize(loginResponse.Administrador);
                Preferences.Default.Set("usuarioLogado", jsonString);
                Application.Current.MainPage = new NavigationPage(new AppView());
            }
            popup.Close(); 
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
            popup.Close(); 
            return;
        }
    }

    private void NavigationForgotPassword(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new ForgotPasswordView());
    }

    private void NavigationCreateUser(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new CreateUserView());
    }
}