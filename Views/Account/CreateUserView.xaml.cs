using CommunityToolkit.Maui.Views;
using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Requests;
using LiquiCycle_FuncionarioPosto.Services;
using Microsoft.Maui.Controls;
using static LiquiCycle_FuncionarioPosto.Services.ViaCEPService;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace LiquiCycle_FuncionarioPosto.Views.Account;

public partial class CreateUserView : ContentPage
{
    private List<PostoDto> postos;
    public CreateUserView()
    {
        InitializeComponent();

        LoadPostosAsync();

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

        EntryCPF.TextChanged += (sender, e) =>
        {
            var entry = sender as Entry;
            int oldLength = e.OldTextValue?.Length ?? 0;
            int newLength = e.NewTextValue?.Length ?? 0;

            // Only proceed if the user is adding characters, not deleting them.
            if (newLength > oldLength)
            {
                string text = new string(entry.Text.Where(ch => Char.IsDigit(ch)).ToArray());

                if (text.Length > 11)
                {
                    if (text.Length >= 2)
                        text = text.Insert(2, ".");
                    if (text.Length >= 6)
                        text = text.Insert(6, ".");
                    if (text.Length >= 10)
                        text = text.Insert(10, "/");
                    if (text.Length >= 15)
                        text = text.Insert(15, "-");
                    if (text.Length > 18) // Limit the text length to valid CNPJ length
                    {
                        text = text.Remove(18);
                    }
                }
                else
                {
                    if (text.Length >= 3)
                        text = text.Insert(3, ".");
                    if (text.Length >= 7)
                        text = text.Insert(7, ".");
                    if (text.Length >= 11)
                        text = text.Insert(11, "-");
                    if (text.Length > 14) // Limit the text length to valid CPF length
                    {
                        text = text.Remove(14);
                    }
                }

                entry.Text = text;
                entry.CursorPosition = entry.Text.Length;
            }
        };

        createUser.Opacity = 0.2;
    }

    private async void CreateUser(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            if (EntrySenha.Text != EntryConfirmaSenha.Text)
            {
                await DisplayAlert("Erro", "As senhas não conferem", "Ok");
                popup.Close();
                return;
            }
            if (EntrySenha.Text.Count() < 6)
            {
                await DisplayAlert("Erro", "A senha deve conter no mínimo 6 caracteres", "Ok");
                popup.Close();
                return;
            }
            var usuarioRequest = new UsuarioRequest
            {
                Dados = new UsuarioDadosRequest
                {
                    Nome = EntryNome.Text,
                    Email = EntryEmail.Text,
                    CPFouCNPJ = EntryCPF.Text,
                    PerfilEnum = PerfilEnum.FuncionarioPosto,
                    CEP = EntryCep.Text.Replace(".", "").Replace("-", ""),
                    Rua = EntryRua.Text,
                    Numero = int.TryParse(EntryNumero.Text, out var numero) ? numero : 0,
                    UF = EntryUF.Text,
                    Cidade = EntryCidade.Text,
                    StatusEnum = StatusEnum.Aprovado,
                    PostoId = null
                },
                Conta = new UsuarioContaRequest
                {
                    Login = RadioCPF.IsChecked ? EntryCPF.Text.Replace(".", "").Replace("-", "").Replace("/", "") : EntryEmail.Text,
                    Senha = EntrySenha.Text
                }
            };
            if (PostoPicker.SelectedItem != null)
            {
                var postoNome = PostoPicker.SelectedItem.ToString();
                var posto = postos.FirstOrDefault(p => p.Nome == postoNome);
                if (posto != null)
                {
                    usuarioRequest.Dados.PostoId = posto.Id;
                }
            }
            await CreateUserAsync(usuarioRequest);
            popup.Close();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            popup.Close();
        }
    }
    private async Task CreateUserAsync(UsuarioRequest usuarioRequest)
    {
        try
        {
            var apiService = new ApiService();
            var result = await apiService.PostAsync<UsuarioRequest, object>("usuario", usuarioRequest);

            await DisplayAlert("Sucesso", "Usuário criado com sucesso", "Ok");
            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }

    private async Task<List<PostoDto>> GetPostosAsync()
    {
        try
        {
            var apiService = new ApiService();
            return await apiService.GetAsync<List<PostoDto>>("posto");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    private async void LoadPostosAsync()
    {
        postos = await GetPostosAsync();
        if (postos != null)
        {
            PostoPicker.ItemsSource = postos.Select(posto => posto.Nome).ToList();
        }
    }

    private void ReturnLogin(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
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
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        createUser.IsEnabled =
            !string.IsNullOrWhiteSpace(EntryNome.Text) &&
            !string.IsNullOrWhiteSpace(EntryEmail.Text) &&
            EntryEmail.Text.Contains("@") &&
            EntryEmail.Text.Contains(".com") &&
            !string.IsNullOrWhiteSpace(EntryCPF.Text) &&
            (EntryCPF.Text.Count() == 14 || EntryCPF.Text.Count() == 18) &&
            !string.IsNullOrWhiteSpace(EntrySenha.Text) &&
            !string.IsNullOrWhiteSpace(EntryConfirmaSenha.Text) &&
            !string.IsNullOrWhiteSpace(EntryCep.Text) &&
            !string.IsNullOrWhiteSpace(EntryRua.Text) &&
            !string.IsNullOrWhiteSpace(EntryCidade.Text) &&
            !string.IsNullOrWhiteSpace(EntryUF.Text) &&
            !string.IsNullOrWhiteSpace(EntryNumero.Text);

        if (createUser.IsEnabled)
        {
            createUser.Opacity = 1;
        }
        else
        {
            createUser.Opacity = 0.2;
        }
    }
}