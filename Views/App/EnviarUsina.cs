using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Services;
using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class EnviarUsina : ContentPage
{
    public List<UsinaDto> usinas;
    public List<LiquidoDto> liquidos { get; set; }
    private string previousSelectedUsina = "Selecione a Usina";

    public EnviarUsina()
    {
        InitializeComponent();
        pickerUsina.SelectedIndexChanged -= UsinaPickerSelectedIndexChanged;
        pickerUsina.SelectedIndexChanged += UsinaPickerSelectedIndexChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = GetUsinasAsync();
    }

    private void Return(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void selecionaliquido(object sender, EventArgs e)
    {
        try
        {
            var apiService = new ApiService();
            liquidos = await apiService.GetAsync<List<LiquidoDto>>("liquido");

            await Navigation.PushModalAsync(new AddLiquido(liquidos));
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }

    private async Task GetUsinasAsync()
    {
        try
        {
            var apiService = new ApiService();
            usinas = await apiService.GetAsync<List<UsinaDto>>("usina");

            if (usinas != null)
            {
                // Adicionar as usinas ao Picker
                var usinaNames = usinas.Select(u => u.Nome).ToList();
                usinaNames.Insert(0, "Selecione a Usina");
                pickerUsina.ItemsSource = usinaNames;
                pickerUsina.SelectedIndex = 0;

                // Adicionar o manipulador de eventos do Picker após definir o ItemsSource
                pickerUsina.SelectedIndexChanged += UsinaPickerSelectedIndexChanged;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void UsinaPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;

        if (picker.SelectedItem != null)
        {
            var selectedUsina = (string)picker.SelectedItem;

            if (selectedUsina != previousSelectedUsina && selectedUsina != "Selecione a Usina")
            {
                previousSelectedUsina = selectedUsina;

                // Filtrar a lista de usinas pelo nome selecionado
                var filteredUsinas = usinas.Where(u => u.Nome == selectedUsina).ToList();

                if (filteredUsinas.Count > 0)
                {
                    var selectedUsinaDto = filteredUsinas.First();

                    // Atualizar o texto da opção selecionada
                    picker.SelectedItem = selectedUsinaDto.Nome;
                }
            }
        }
    }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (string)picker.SelectedItem;

        Console.WriteLine($"Opção selecionada: {selectedOption}");
    }
}