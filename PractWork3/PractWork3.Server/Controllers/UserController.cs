using Microsoft.AspNetCore.Mvc;
using PractWork3.Client.Services;
using PractWork3.Server.Contexts;
using PractWork3.Server.Dtos;
using PractWork3.Server.Models;

namespace PractWork3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(AppDbContext context) : ControllerBase
    {
        readonly UserService userService = new(context);

        [HttpGet]
        public ActionResult<List<UserDto>> GetUsers([FromQuery] int page,
                                                    [FromQuery] int pageSize,
                                                    [FromBody] SortOptions sortOption = SortOptions.NoSort,
                                                    [FromBody] List<FilterDto>? filters = null)
        {
            var users = userService.GetUsersDto();
            if (filters is not null)
                foreach (var filter in filters)
                    users = userService.ApplyFilter(users, filter.FilterOption, filter.FilterData);

            if (sortOption != SortOptions.NoSort)
                users = userService.ApplySort(users, sortOption);

            users = userService.ApplyPagination(users, page, pageSize);

            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(int id)
        {
            var user = userService.GetUserDto(id);

            if (user is null)
                return NotFound();

            return user;
        }

        [HttpGet("login")]
        public ActionResult<UserDto> GetUserByLogin([FromQuery] string login)
        {
            var user = userService.GetUserDtoByLogin(login);

            if (user is null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            userService.AddUser(user);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<UserDto> PutUser(int id, [FromBody] User user)
        {
            userService.ChangeUser(id, user);
            return GetUser(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return NoContent();
        }
    }
}
