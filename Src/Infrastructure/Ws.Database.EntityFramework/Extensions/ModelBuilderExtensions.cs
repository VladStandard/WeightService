using System.Net;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ws.Database.EntityFramework.Converters;

namespace Ws.Database.EntityFramework.Extensions;

internal static class ModelBuilderExtensions
{
    public static void SetDefaultTypeForString(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        foreach (IMutableProperty property in entity.GetProperties().Where(i => i.ClrType == typeof(string)))
        {
            int? maxValue = property.GetMaxLength();
            property.SetColumnType($"varchar({(maxValue.HasValue ? maxValue : "max")})");
        }
    }

    public static void UseIpAddressConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<PropertyInfo> ipAddressProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(IPAddress));

            foreach (PropertyInfo property in ipAddressProperties)
                modelBuilder.Entity(entityType.Name).Property(property.Name)
                    .HasConversion(new IpAddressToIPv4StringConverter());
        }
    }

    public static void UseEnumStringConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<PropertyInfo> enumProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            foreach (PropertyInfo property in enumProperties)
            {
                Type converterType = typeof(EnumToStringConverter<>)
                    .MakeGenericType(property.PropertyType);

                int maxLength = Enum.GetNames(property.PropertyType).Max(name => name.Length);

                ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter)
                    .HasColumnType($"varchar({maxLength})");
            }
        }
    }

    public static void MapCreateOrChangeDt(this ModelBuilder modelBuilder)
    {
        const string getDateCmd = "GETDATE()";
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IMutableProperty? createDtProperty = entityType.FindProperty(nameof(SqlColumns.CreateDt)) ?? null;
            IMutableProperty? changeDtProperty = entityType.FindProperty(nameof(SqlColumns.ChangeDt)) ?? null;

            if (createDtProperty != null)
            {
                createDtProperty.ValueGenerated = ValueGenerated.OnAdd;
                createDtProperty.SetColumnName(SqlColumns.CreateDt);
                createDtProperty.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                createDtProperty.SetDefaultValueSql(getDateCmd);
            }

            if (changeDtProperty != null)
            {
                changeDtProperty.SetColumnName(SqlColumns.ChangeDt);
                changeDtProperty.SetDefaultValueSql(getDateCmd);
            }
        }
    }
}