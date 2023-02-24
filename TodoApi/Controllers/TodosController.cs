using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly AppDbContext context;

        public TodosController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<TodosController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoModel>> Get()
        {
            return Ok(context.Todos.ToList());
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoModel> Get(int id)
        {
            TodoModel? todo = context.Todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound("Sorry but todo item was not found");
            }

            return Ok(todo);
        }

        // POST api/<TodosController>
        [HttpPost]
        public IActionResult Post([FromBody] TodoModel todo)
        {
            //get all todo items from db
            List<TodoModel> todos = context.Todos.ToList();

            TodoModel? foundTodo = todos.FirstOrDefault(t => t.Task.Trim().ToLower() == todo.Task.Trim().ToLower());

            if (foundTodo == null)
            {
                context.Todos.Add(todo);
                context.SaveChanges();
                //KAN MAN SKICKA TILLBAKA BÅDA OBJEKTET OCH RESPONSE MESSAGE?
                return Ok("Item was successfully added!");
            }
            else
            {
                return BadRequest("Todo item already exists!");
            }
            
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public ActionResult<TodoModel> Put(int id, [FromBody] TodoModel todo)
        {
            //get all todo items from db
            List<TodoModel> todos = context.Todos.ToList();

            TodoModel? existingTodo = todos.FirstOrDefault(t => t.Id == todo.Id);

            if (existingTodo == null)
            {
                return NotFound("Sorry, couldn't find the todo item");
            }

            existingTodo.Task = todo.Task;
            existingTodo.IsDone = todo.IsDone;
            context.SaveChanges();
            return Ok("Item successfully updated!");
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public ActionResult<TodoModel> Delete(int id)
        {
            //get all todo items from db
            List<TodoModel> todos = context.Todos.ToList();

            TodoModel? todoToDelete = todos.FirstOrDefault(t => t.Id == id);

            if (todoToDelete == null)
            {
                return NotFound("Sorry, couldn't find the todo item");
            }

            context.Todos.Remove(todoToDelete);
            context.SaveChanges();
            return Ok(todoToDelete);
        }
    }
}
