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