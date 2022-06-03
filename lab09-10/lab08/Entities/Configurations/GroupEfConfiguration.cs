﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab08.Entities.Configurations
{
    public class GroupEfConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(e => e.IdGroup).HasName("Group_pk");
            builder.Property(e => e.IdGroup).UseIdentityColumn();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(10);

            //unikalna wartosc w kolumnie (np pesel)
            //builder.HasIndex(e => e.Name).IsUnique();

            builder.ToTable("Group");
        }
    }
}
