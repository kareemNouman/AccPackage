using Account.Core.Framework.Domain_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Mappers
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
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

            this.Property(t => t.AddressID)
                .HasMaxLength(50);

            this.Property(t => t.Department)
                .HasMaxLength(50);

            this.Property(t => t.Image)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Employee");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Salary).HasColumnName("Salary");
            this.Property(t => t.DOJ).HasColumnName("DOJ");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.Department).HasColumnName("Department");
            this.Property(t => t.Allowance).HasColumnName("Allowance");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.EmployeeNo).HasColumnName("EmployeeNo");
        }
    }
}
