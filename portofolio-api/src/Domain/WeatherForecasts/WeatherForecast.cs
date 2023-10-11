using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts.Events;

namespace SemSnel.Portofolio.Domain.WeatherForecasts;

public class WeatherForecast : AggregateRoot<Guid>, IAuditableEntity
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
    
    public static WeatherForecast Create(DateOnly requestDate, int requestTemperatureC, string? requestSummary)
    {
        var forecasts = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = requestDate,
            TemperatureC = requestTemperatureC,
            Summary = requestSummary
        };
        
        var message = new WeatherForecastCreatedEvent(forecasts.Id);
        
        forecasts.AddDomainEvent(message);
        
        return forecasts;
    }
    
    public ErrorOr<Success> Update(DateOnly requestDate, int requestTemperatureC, string? requestSummary)
    {
        Date = requestDate;
        TemperatureC = requestTemperatureC;
        Summary = requestSummary;
        
        var message = new WeatherForecastUpdatedEvent(this.Id);
        
        AddDomainEvent(message);
        
        return Result.Success();
    }

    public void Cancel()
    {
        var message = new WeatherForecastDeletedEvent(Id);
        
        AddDomainEvent(message);
    }
}