using PractWork3.Server;
using PractWork3.Server.Contexts;
using PractWork3.Server.Dtos;
using PractWork3.Server.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PractWork3.Client.Services
{
    public class UserService(AppDbContext context)
    {
        readonly AppDbContext context = context;
        public List<UserDto> GetUsersDto()
        {
            return context.Users.Select(u => new UserDto()
            {
                Login = u.Login,
                Password = u.Password,
                RoleName = context.Roles.FirstOrDefault(r => r.Id == u.RoleId).Name
            }).ToList();
        }

        public List<UserDto> ApplyPagination(List<UserDto> users, int page = 1, int pageSize = 5)
        {
            var totalItems = users.Count();
            return users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<UserDto> ApplySort(List<UserDto> users, SortOptions option)
        {
            if (users.Count == 0)
                return users;

            switch (option)
            {
                case SortOptions.Login:
                    return users.OrderBy(u => u.Login).ToList();
                case SortOptions.LoginDescending:
                    return users.OrderByDescending(u => u.Login).ToList();
                case SortOptions.Role:
                    return users.OrderBy(u => u.RoleName).ToList();
                case SortOptions.RoleDescending:
                    return users.OrderByDescending(u => u.RoleName).ToList();
                default:
                    return users;
            }
        }

        public List<UserDto> ApplyFilter(List<UserDto> users, FilterOptions option, string sortData)
        {
            switch (option)
            {
                case FilterOptions.ByRole:
                    return users.Where(u => u.RoleName == sortData).ToList();
                case FilterOptions.ByLogin:
                    return users.Where(u => u.RoleName.Contains(sortData)).ToList();
                default:
                    return users;
            }
        }

        public UserDto? GetUserDto(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
                return null;

            return new UserDto()
            {
                Login = user.Login,
                Password = user.Password,
                RoleName = context.Roles.FirstOrDefault(r => r.Id == user.RoleId).Name 
            };
        }

        public UserDto GetUserDtoByLogin(string login)
        {
            var user = context.Users.FirstOrDefault(u => u.Login == login);

            if (user is null)
                return null;

            return new UserDto()
            {
                Login = user.Login,
                Password = user.Password,
                RoleName = context.Roles.FirstOrDefault(r => r.Id == user.RoleId).Name
            };
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
        }

        public bool ChangeUser(int id, User newUserData)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user is null || user.Id != newUserData.Id)
                return false;

            context.Users.Entry(user);
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
                return false;

            context.Users.Remove(user);
            return true;
        }
    }
}
