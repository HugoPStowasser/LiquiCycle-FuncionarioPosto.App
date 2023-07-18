using LiquiCycle_FuncionarioPosto.Dtos;
using Microsoft.Maui.Controls;

namespace LiquiCycle_FuncionarioPosto.Views.App;

public partial class AddLiquido : ContentPage
{
    public LiquidoDto SelectedLiquido { get; private set; }

    public AddLiquido(List<LiquidoDto> liquidos)
    {
        InitializeComponent();

        foreach (var liquido in liquidos)
        {
            var radioButton = new RadioButton
            {
                Content = liquido.Nome,
                IsChecked = false,
                HorizontalOptions = LayoutOptions.Start
            };

            radioButton.CheckedChanged += (s, e) =>
            {
                if (radioButton.IsChecked)
                    SelectedLiquido = liquido;
            };

            var rowDefinition = new RowDefinition { Height = GridLength.Star };
            gridLiquidos.RowDefinitions.Add(rowDefinition);
            Grid.SetRow(radioButton, gridLiquidos.RowDefinitions.Count - 1);
            gridLiquidos.Children.Add(radioButton);
        }
    }

    private void Return(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}