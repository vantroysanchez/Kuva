using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class HeaderConfiguration: IEntityTypeConfiguration<Header>
    {
        public void Configure(EntityTypeBuilder<Header> builder)
        {
            builder.Property(t => t.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Code)
                .IsRequired();

        }
    }

}
