using API2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace API2.Configurations
{
    public class StudentsConfiguration
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Fullname).HasMaxLength(25).IsRequired(true);
            builder.HasOne(x => x.Group).WithMany(x => x.Students).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
