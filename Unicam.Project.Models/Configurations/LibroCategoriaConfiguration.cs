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
    public class LibroCategoriaConfiguration : IEntityTypeConfiguration<LibroCategoria>
    {
        public void Configure(EntityTypeBuilder<LibroCategoria> builder)
        {
            builder.ToTable("LibriCategorie");
            builder.HasKey(lc => new { lc.IdLibro, lc.IdCategoria });

            builder.HasOne(lc => lc.Libro)
                .WithMany(l => l.LibriCategorie)
                .HasForeignKey(lc => lc.IdLibro);

            builder.HasOne(lc => lc.Categoria)
                .WithMany(c => c.LibroCategorie)
                .HasForeignKey(lc => lc.IdCategoria);
        }
    }

}
