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
    public class LibroConfiguration : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.ToTable("Libri");
            builder.HasKey(l => l.IdLibro);

            builder.Property(l => l.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Autore)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Editore)
                .HasMaxLength(100);

            builder.Property(l => l.DataPubblicazione)
                .IsRequired();

            builder.HasOne(l => l.Utente)
                   .WithMany(u => u.Libri)
                   .HasForeignKey(l => l.IdUtente);
        }
    }

}
