using LiquiCycle_FuncionarioPosto.Views.App;

namespace LiquiCycle_FuncionarioPosto.Views.Account;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }
    private void LoginPost(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new AppView());
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