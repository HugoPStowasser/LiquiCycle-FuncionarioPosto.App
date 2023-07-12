using Microsoft.Maui.Controls;
using System;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class AgendamentosView : ContentPage
{
	public AgendamentosView()
	{
		InitializeComponent();
        List<getAgendamentos> agendamentos = new List<getAgendamentos>()
        {
            new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
            new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
            new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
            new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
            new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},

        };
        listAgendamentos.ItemsSource = agendamentos;

    }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (string)picker.SelectedItem;

        
        Console.WriteLine($"Opção selecionada: {selectedOption}");
    }

    private void NavigationCreateAgendamento(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new CreateAgendamentosView());
    }

    public class getAgendamentos 
    {
        public string nome { get; set; }
        public string liquido { get; set; }
        public string litros { get; set; }


    }
}


