using Account.Core.Framework.Domain_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Mappers
{
    public class VendorMap : EntityTypeConfiguration<Vendor>
    {
        public VendorMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.MiddleName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.Company)
                .HasMaxLength(50);

            this.Property(t => t.AliasName)
                .HasMaxLength(50);

            this.Property(t => t.Notes)
                .HasMaxLength(200);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.Fax)
                .HasMaxLength(50);

            this.Property(t => t.EmailId)
                .HasMaxLength(50);

            this.Property(t => t.Website)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Vendor");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Company).HasColumnName("Company");
            this.Property(t => t.AliasName).HasColumnName("AliasName");
            this.Property(t => t.AddressId).HasColumnName("AddressId");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            this.Property(t => t.AsOfDate).HasColumnName("AsOfDate");
            this.Property(t => t.TaxId).HasColumnName("TaxId");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
