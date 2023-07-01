using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Requests;
using LiquiCycle_FuncionarioPosto.Services;
using System.Text.Json;
using static LiquiCycle_FuncionarioPosto.Services.ViaCEPService;

namespace LiquiCycle_FuncionarioPosto.Views.App.Profile;

public partial class EditProfile : ContentPage
{
    private readonly FuncionarioPostoDto funcionario;
    public EditProfile()
	{
		InitializeComponent();
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        funcionario = JsonSerializer.Deserialize<FuncionarioPostoDto>(jsonString);

        EntryCep.TextChanged += (sender, e) =>
        {
            var entry = sender as Entry;
            int oldLength = e.OldTextValue?.Length ?? 0;
            int newLength = e.NewTextValue?.Length ?? 0;

            // Only proceed if the user is adding characters, not deleting them.
            if (newLength > oldLength)
            {
                string text = new string(entry.Text.Where(ch => Char.IsDigit(ch)).ToArray());

                if (text.Length >= 2)
                    text = text.Insert(2, ".");
                if (text.Length >= 6)
                    text = text.Insert(6, "-");
                if (text.Length > 10) // Limit the text length to valid CEP length
                {
                    text = text.Remove(10);
                }

                entry.Text = text;
                entry.CursorPosition = entry.Text.Length;
            }
        };
        EntryNome.Text = funcionario.Usuario.Nome;
        EntryCep.Text = funcionario.Usuario.CEP;
        EntryCidade.Text = funcionario.Usuario.Cidade;
        EntryRua.Text = funcionario.Usuario.Rua;
        EntryNumero.Text = funcionario.Usuario.Numero.ToString();
        EntryUF.Text = funcionario.Usuario.UF;
    }
    
    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void SalvarUser(object sender, EventArgs e)
    {
        var Dados = new UsuarioDadosRequest
        {
            Id = funcionario.Usuario.Id,
            Nome = EntryNome.Text,
            CPFouCNPJ = funcionario.Usuario.CPFouCNPJ,
            PerfilEnum = PerfilEnum.Administrador,
            CEP = EntryCep.Text.Replace(".", "").Replace("-", ""),
            Rua = EntryRua.Text,
            Numero = int.TryParse(EntryNumero.Text, out var numero) ? numero : 0,
            UF = EntryUF.Text,
            Cidade = EntryCidade.Text,
            StatusEnum = StatusEnum.Aprovado,
            PostoId = null
        };
        await UpdateUserAsync(Dados);
        funcionario.Usuario.Nome = EntryNome.Text;
        funcionario.Usuario.CEP = EntryCep.Text;
        funcionario.Usuario.Cidade = EntryCidade.Text;
        funcionario.Usuario.Rua = EntryRua.Text;
        funcionario.Usuario.Numero = int.TryParse(EntryNumero.Text, out var numero1) ? numero : 0;
        funcionario.Usuario.UF = EntryUF.Text;
        string jsonString = JsonSerializer.Serialize(funcionario);
        Preferences.Set("usuarioLogado", jsonString);
        await DisplayAlert("Sucesso", "Usuário atualizado com sucesso", "Ok");
        await Navigation.PopModalAsync();
    }

    private async Task UpdateUserAsync(UsuarioDadosRequest usuarioRequest)
    {
        var apiService = new ApiService();
        var result = await apiService.PutAsync<UsuarioDadosRequest, object>("usuario", usuarioRequest);
    }

    private async void OnEntryCompletedCep(object sender, EventArgs e)
    {
        if (EntryCep.Text.Count() == 10)
        {
            try
            {
                var cep = EntryCep.Text.Replace(".", "").Replace("-", "");
                ViaCEPService service = new ViaCEPService();
                Address address = await service.GetAddressByCEPAsync(cep);
                EntryRua.Text = address.Street;
                EntryCidade.Text = address.City;
                EntryUF.Text = address.UF;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                EntryRua.Text = "";
                EntryCidade.Text = "";
                EntryUF.Text = "";
            }
        }
    }
}