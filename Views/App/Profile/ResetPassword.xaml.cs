using LiquiCycle_FuncionarioPosto.Dtos;

namespace LiquiCycle_FuncionarioPosto.Views.App.Profile;

public partial class ResetPassword : ContentPage
{
	public ResetPassword(FuncionarioPostoDto funcionario)
	{
		InitializeComponent();
        var formattedString = new FormattedString();
        var span1 = new Span { Text = "Uma mensagem foi enviada para o email: \n", TextColor = Color.FromArgb("000000") };
        var span2 = new Span { Text = funcionario.Usuario.Email, TextColor = Color.FromArgb("0B6EFE") };

        formattedString.Spans.Add(span1);
        formattedString.Spans.Add(span2);
        LabelSuccess.FormattedText = formattedString;
    }

    private void ReturnProfile(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SendEmail(object sender, TappedEventArgs e)
    {

    }
}