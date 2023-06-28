using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace LiquiCycle_FuncionarioPosto.Views.Account;

public partial class ForgotPasswordView : ContentPage, INotifyPropertyChanged
{
    private int _active = 0;
    public int Active
    {
        get => _active;
        set
        {
            if (_active != value)
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }
    public ForgotPasswordView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void ReturnLogin(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SendEmail(object sender, EventArgs e)
    {
        string email = EntryEmail.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            // Realizar ações quando o campo estiver vazio
            // Exemplo: exibir mensagem de erro
            DisplayAlert("Erro", "O campo de e-mail não pode estar vazio", "OK");
        }
        else if (!IsValidEmail(email))
        {
            // Verificar se o formato do e-mail é válido
            
                // Realizar ações quando o formato de e-mail for inválido
                // Exemplo: exibir mensagem de erro
                DisplayAlert("Erro", "Por favor, insira um e-mail válido", "OK");
            
        }
        else 
        { 
         if (Active == 1)
         {
            Active = 0;
         }
         else
         {
            Active = 1;
            var formattedString = new FormattedString();
            var span1 = new Span { Text = "Uma mensagem foi enviada para o email: \n", TextColor = Color.FromArgb("000000") };
            var span2 = new Span { Text = EntryEmail.Text, TextColor = Color.FromArgb("0B6EFE") };

            formattedString.Spans.Add(span1);
            formattedString.Spans.Add(span2);
            LabelSuccess.FormattedText = formattedString;
         }
        }
    }
    public new event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
    }
}