using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Views.App.Profile;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class ProfileView : ContentPage
{
    private readonly FuncionarioPostoDto funcionario;
    public ProfileView()
	{
		InitializeComponent();
        funcionario = new FuncionarioPostoDto()
        {
            Id = 1,
            Usuario = new UsuarioDto
            {
                Id = 1,
                Nome = "Usuario Adm",
                CPFouCNPJ = "12345678910",
                Email = "usuario1@email.com",
                PerfilEnum = PerfilEnum.Cliente,
                CEP = "12345678",
                Rua = "Rua A",
                Numero = 1,
                UF = "UF",
                Cidade = "Cidade",
                StatusEnum = StatusEnum.Bloqueado,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        };
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

    }
}