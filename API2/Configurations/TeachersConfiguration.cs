using API2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API2.Configurations
{
    public class TeachersConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.Fullname).HasMaxLength(25).IsRequired(true);
            builder.HasMany(x=>x.Groups).WithOne(x=>x.Teacher).HasForeignKey(x=>x.TeacherId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
