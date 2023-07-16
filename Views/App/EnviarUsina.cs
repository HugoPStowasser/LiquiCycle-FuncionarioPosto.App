using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Services;
using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class EnviarUsina : ContentPage
{
    List<UsinaDto> usinas;
    public EnviarUsina()
    {
        InitializeComponent();
        GetUsinasAsync();
    }

    private void Return(object sender, EventArgs e) 
    {
        Navigation.PopModalAsync();
    }

    private async Task<List<LiquidoDto>> GetLiquidosAsync()
    {
        try
        {
            var apiService = new ApiService();
            return await apiService.GetAsync<List<LiquidoDto>>("liquido");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    private async void GetUsinasAsync()
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
                pickerUsina.SelectedItem = "Selecione a Usina";
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }
    private void UsinaPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedUsina = (string)picker.SelectedItem;

        if (selectedUsina != "Selecione a Usina")
        {
            // Filtrar a lista de usinas pelo nome selecionado
            var filteredUsinas = usinas.Where(u => u.Nome.StartsWith(selectedUsina)).ToList();

            // Atualizar as opções do Picker de Usina
            var usinaNames = filteredUsinas.Select(u => u.Nome).ToList();
            usinaNames.Insert(0, "Selecione a Usina");
            picker.ItemsSource = usinaNames;
            picker.SelectedItem = usinaNames.FirstOrDefault();
        }
    }

    private void selecionaliquido(object sender, EventArgs e) 
    {
        Navigation.PushModalAsync(new AddLiquido());
    }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (string)picker.SelectedItem;


        Console.WriteLine($"Opção selecionada: {selectedOption}");
    }


}