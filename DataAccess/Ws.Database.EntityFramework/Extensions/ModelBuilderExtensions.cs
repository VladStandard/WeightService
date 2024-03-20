using System.Net;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ws.Database.EntityFramework.Converters;

namespace Ws.Database.EntityFramework.Extensions;

internal static class ModelBuilderExtensions
{
    public static void UseIpAddressConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            var ipAddressProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(IPAddress));

            foreach (PropertyInfo property in ipAddressProperties)
            {
                modelBuilder.Entity(entityType.Name).Property(property.Name)
                    .HasConversion(new IpAddressToIPv4StringConverter());
            }
        }
    }
    
    public static void UseEnumStringConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            var enumProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            foreach (PropertyInfo property in enumProperties)
            {
                Type converterType = typeof(EnumToStringConverter<>)
                    .MakeGenericType(property.PropertyType);

                ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
            }
        }
    }
}