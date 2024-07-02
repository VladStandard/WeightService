using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ws.Database.EntityFramework.Entities.Print.Labels;

namespace Ws.Database.EntityFramework.Entities.Print.LabelsZpl;

public class LabelZplMapConfig : IEntityTypeConfiguration<LabelZplEntity>
{

    public void Configure(EntityTypeBuilder<LabelZplEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.LabelsZpl, SqlSchemas.Print);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("UID");

        #endregion

        #region FK

        builder.HasOne<LabelEntity>()
            .WithOne(l => l.Zpl)
            .HasForeignKey<LabelZplEntity>(n => n.Id)
            .HasPrincipalKey<LabelEntity>(p => p.Id)
            .HasConstraintName($"FK_{SqlTables.LabelsZpl}__LABEL");

        #endregion


        builder.Property(e => e.Zpl)
            .HasColumnName("ZPL")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(e => e.Rotate)
            .HasColumnName("ROTATE")
            .IsRequired();

        builder.Property(e => e.Width)
            .HasColumnName("WIDTH")
            .IsRequired();

        builder.Property(e => e.Height)
            .HasColumnName("HEIGHT")
            .IsRequired();
    }
}