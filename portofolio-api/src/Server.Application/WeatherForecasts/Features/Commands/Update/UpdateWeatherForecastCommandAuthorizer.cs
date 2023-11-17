using SemSnel.Portofolio.Server.Application.Common.Authorisations.Authorizers;
using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Update;

public sealed class UpdateWeatherForecastCommandAuthorizer
    : AbstractAuthorizer<UpdateWeatherForecastsCommand>
{
    public UpdateWeatherForecastCommandAuthorizer(ICurrentIdentityService currentIdentityService)
    {
       this.AddMustBeAuthenticatedRule(currentIdentityService);
       this.AddMustHavePermissionRule(
           currentIdentityService,
           WeatherForecastPermissions.UpdateWeatherForecastPermission);
    }
}