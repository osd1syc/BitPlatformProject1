using Project1.Server.Api.Models.Todo;

namespace Project1.Server.Api.Data.Configurations.Todo;

public class TodoConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasOne(t => t.User)
            .WithMany(u => u.TodoItems)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
