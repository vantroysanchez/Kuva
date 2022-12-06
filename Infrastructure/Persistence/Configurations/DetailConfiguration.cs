using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class DetailConfiguration: IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.Property(t => t.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Quantity)                
                .IsRequired();
        }
    }
}
