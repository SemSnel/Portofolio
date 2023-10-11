using SemSnel.Portofolio.Application.Common.Authorisations.Authorizers;
using SemSnel.Portofolio.Application.Common.Authorisations.Rules;
using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

public sealed class UpdateWeatherForecastCommandAuthorizer 
    : AbstractAuthorizer<UpdateWeatherForecastsCommand>
{
    public UpdateWeatherForecastCommandAuthorizer(ICurrentUserService currentUserService)
    {
       this.AddMustBeAuthenticatedRule(currentUserService);
       this.AddMustHavePermissionRule(
           currentUserService, 
           WeatherForecastPermissions.UpdateWeatherForecastPermission);
    }
}