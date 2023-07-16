using CommunityToolkit.Maui.Views;
using LiquiCycle_FuncionarioPosto.Dtos;
using LiquiCycle_FuncionarioPosto.Services;
using Microsoft.Maui.Controls;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Button = Microsoft.Maui.Controls.Button;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class AgendamentosView : ContentPage
{
	public AgendamentosView()
	{
		InitializeComponent();


        //List<TransacaoClienteDto> agendamentos = new List<TransacaoClienteDto>();
        
        var agendamentos = GetAgendamentoAsync("1");
        
        //listAgendamentos.ItemsSource = agendamentos;
        //List<getAgendamentos> agendamentos = new List<getAgendamentos>()
        //{
        //    new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L",idagendamento},
        //    new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
        //    new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
        //    new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},
        //    new getAgendamentos(){nome = "Aroldo Gomes da Silva", liquido = "Óleo de cozinha", litros = "5L"},

        //};
        //listAgendamentos.ItemsSource = agendamentos;

    }

    private async Task<List<TransacaoClienteDto>> GetAgendamentoAsync(string idPosto)
    {
        
        try
        {
            var apiService = new ApiService();
            return await apiService.GetAsync<List<TransacaoClienteDto>>("cliente/"+ idPosto);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    private async Task<List<UsinaDto>> GetUsinasAsync()
    {
        try
        {
            var apiService = new ApiService();
            return await apiService.GetAsync<List<UsinaDto>>("usina");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
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

    private void Botao_Clicado(object sender, EventArgs e)
    {
        
            
        //string uid = btnAprovarAgendameto.AutomationId;

        //POST COM O ID DO AGENTE
    }
    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {

        var clickedItem = e.Item;

        if (clickedItem != null) 
        { 

        }



    }
}


