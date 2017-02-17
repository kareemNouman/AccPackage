using Account.Core.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Mappers
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.PasswordHash)
                .HasMaxLength(50);

            this.Property(t => t.Salt)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.Salt).HasColumnName("Salt");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
        }
    }
}
