using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // In-memory user store for demonstration
        private static readonly List<User> users = new List<User>();

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                // Return a copy to prevent external modification
                return Ok(users.ToList());
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound($"User with ID {id} not found.");
                return Ok(user);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
                users.Add(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                // Update other properties as needed

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                users.Remove(user);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }

    // Simple User model with validation
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        // Add other properties as needed
    }

}
