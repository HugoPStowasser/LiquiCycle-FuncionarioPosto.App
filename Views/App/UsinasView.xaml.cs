using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class UsinasView : ContentPage
{
	public UsinasView()
	{
		InitializeComponent();
        List<getUsinas> usinas = new List<getUsinas>()
        {
            new getUsinas(){nome = "Posto Casa Verde", codigo = "#34567"},
            

        };
        listUsinas.ItemsSource = usinas;
    }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (string)picker.SelectedItem;


        Console.WriteLine($"Opção selecionada: {selectedOption}");
    }

    private void limpaTudo(object sender, TappedEventArgs e) { }

    public class getUsinas
    {
        public string nome { get; set; }
        public string codigo { get; set; }


    }


}