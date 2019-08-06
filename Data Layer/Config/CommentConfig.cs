using cms.Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cms.Data_Layer.Config
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment.HasKey(c => c.Id);
            comment.HasMany(c => c.Replies)
                .WithOne(c => c.ParentComment)
                .HasForeignKey(c => c.ParentCommentId);

            comment.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);
        }
    }
}