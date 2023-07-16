using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Services;
using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class EnviarUsina : ContentPage
{
    public EnviarUsina()
    {
        InitializeComponent();

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