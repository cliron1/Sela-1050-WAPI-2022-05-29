using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data {
	public class TodoContext : DbContext {
		public TodoContext(DbContextOptions<TodoContext> options)
			: base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<TodoItem>().HasData(
				new TodoItem { Id = 1, Name = "Learn C#" },
				new TodoItem { Id = 2, Name = "Learn Java" },
				new TodoItem { Id = 3, Name = "Learn React" }
			);
		}

		public DbSet<TodoItem> TodoItems { get; set; }
	}
}
