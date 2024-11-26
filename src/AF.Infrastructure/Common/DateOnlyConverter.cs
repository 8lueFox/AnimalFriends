using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AF.Infrastructure.Common;

public class DateOnlyConverter() : ValueConverter<DateOnly, DateTime>(
    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
    dateTime => DateOnly.FromDateTime(dateTime));
    
public class TimeOnlyConverter() : ValueConverter<TimeOnly, TimeSpan>(
    timeOnly => timeOnly.ToTimeSpan(),
    timeSpan => TimeOnly.FromTimeSpan(timeSpan));