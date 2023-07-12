using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class CreateAgendamentosView : ContentPage
{
	public CreateAgendamentosView()
	{
		InitializeComponent();
       
    }

    private void limpaTudo(object sender, TappedEventArgs e) { }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (string)picker.SelectedItem;


        Console.WriteLine($"Opção selecionada: {selectedOption}");
    }

    private void criaAgendamento(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new CreateAgendamentosView());
    }

    private void Return(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }
}