using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Models.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorie");
            builder.HasKey(c => c.IdCategoria);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Nome)
                .IsUnique();

            builder.HasOne(c => c.Utente)
            .WithMany(u => u.Categorie)
            .HasForeignKey(c => c.IdUtente)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
