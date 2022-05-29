using Microsoft.EntityFrameworkCore;

namespace TodoApp.Controllers {
	[Route("todo")]
	[ApiController]
	public class TodoController : ControllerBase {
		private readonly TodoContext _context;

		public TodoController(TodoContext context) {
			_context = context;
		}

		/// <summary> Get All the items </summary>
		/// <returns></returns>
		// GET: Todo
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {
			return await _context.TodoItems.ToListAsync();
		}

		/// <summary> Get specific item </summary>
		// GET: Todo/5
		[HttpGet("{id:long}")]
		public async Task<ActionResult<TodoItem>> GetTodoItem(long id) {
			var todoItem = await _context.TodoItems.FindAsync(id);

			if (todoItem == null) {
				return NotFound();
			}

			return todoItem;
		}

		// PUT: Todo/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id:long}")]
		public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem) {
			if (id != todoItem.Id) {
				return BadRequest();
			}

			_context.Entry(todoItem).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!TodoItemExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: Todo
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem) {
			_context.TodoItems.Add(todoItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
		}

		// DELETE: Todo/5
		[HttpDelete("{id:long}")]
		public async Task<IActionResult> DeleteTodoItem(long id) {
			var todoItem = await _context.TodoItems.FindAsync(id);
			if (todoItem == null) {
				return NotFound();
			}

			_context.TodoItems.Remove(todoItem);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TodoItemExists(long id) {
			return _context.TodoItems.Any(e => e.Id == id);
		}
	}
}
