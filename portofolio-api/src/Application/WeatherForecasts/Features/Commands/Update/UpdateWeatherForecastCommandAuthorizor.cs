using SemSnel.Portofolio.Application.Common.Authorisations.Authorizors;
using SemSnel.Portofolio.Application.Common.Authorisations.Rules;
using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

public sealed class UpdateWeatherForecastCommandAuthorizor 
    : AbstractAuthorizor<UpdateWeatherForecastsCommand>
{
    public UpdateWeatherForecastCommandAuthorizor(ICurrentUser currentUser)
    {
        this.AddMustBeAuthenticatedRule(currentUser);
        this.AddMustHavePermissionRule(currentUser, Permission.UPDATE_WEATHER_FORECAST_PERMISSION);
    }
}