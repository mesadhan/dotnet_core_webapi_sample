
## .Net Core WebAPI Project

> Today we cover those things...
- [x] 1. Add a model class.
- [x] 2. Create the database context.
- [x] 3. Register the database context.
- [x] 4. Add a controller.
- [x] 5. Add CRUD methods.
- [x] 6. Configure routing and URL paths.
- [x] 7. Specify return values.
- [x] 8. Call the web API with REST Client Extention.



# How to Run And Test Applicaiton
> First clone source file.
> And then open terminal and hit below command
    
    dotnet run

> Then, open **api.http** file for test api.


## Implementation:
> Step 1: Create Model Class, **Models/TodoItem.cs**
```csharp 
namespace dotnet_core_webapi_sample.Models {
    public class TodoItem
    {
       public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
```


> Step 2: Create the database context, **Models/TodoContext.cs**
```csharp

using Microsoft.EntityFrameworkCore;
namespace dotnet_core_webapi_sample.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options){}

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
```


> Setp 3: Here, **Startup.css** file method updated

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Config In Memory Database
    services.AddDbContext<TodoContext>(opt =>
        opt.UseInMemoryDatabase("TodoList"));
        
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```

> Step 4,5,6,7:  Add a controller **Controllers/TodoController.cs**

```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_core_webapi_sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_webapi_sample.Controller {

    [Route ("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext _context;

        public TodoController (TodoContext context) {
            _context = context;

            if (_context.TodoItems.Count () == 0) {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add (new TodoItem { Name = "Item1" });
                _context.SaveChanges ();
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems () {
            return await _context.TodoItems.ToListAsync ();
        }

        // GET: api/Todo/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem (long id) {
            var todoItem = await _context.TodoItems.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem (TodoItem item) {
            _context.TodoItems.Add (item);
            await _context.SaveChangesAsync ();

            return CreatedAtAction (nameof (GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutTodoItem (long id, TodoItem item) {
            if (id != item.Id) {
                return BadRequest ();
            }

            _context.Entry (item).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return NoContent ();
        }

        // DELETE: api/Todo/5
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteTodoItem (long id) {
            var todoItem = await _context.TodoItems.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            _context.TodoItems.Remove (todoItem);
            await _context.SaveChangesAsync ();

            return NoContent ();
        }

    }
}
```

> Step 8: Run server and then, open **api.http** file for test api.
> Send Request to different endpint and test output



## Create New Project From Scratch
> If you want to create new project from scratch then hit below command

    dotnet new webapi -o TodoApi



# References:

[Offical Doc. For Web API](
    https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio-code
)

[Offical .Net Core Learning Resource](
https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-2.2
)

Thanks Everyone...