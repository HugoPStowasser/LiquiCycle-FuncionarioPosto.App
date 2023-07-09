using CommunityToolkit.Maui;
using LiquiCycle_FuncionarioPosto.Views.Account;
using LiquiCycle_FuncionarioPosto.Views.App;
using Microsoft.Extensions.Logging;

namespace LiquiCycle_FuncionarioPosto;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).RegisterViews(); ;

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<LoginView>();
        mauiAppBuilder.Services.AddTransient<AppView>();
        mauiAppBuilder.Services.AddTransient<ResetPasswordPage>();

        return mauiAppBuilder;
    }
}
