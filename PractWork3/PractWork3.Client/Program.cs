using PractWork3.Server.Dtos;
using PractWork3.Server.Models;
using System.Net.Http.Json;

using var client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5230/api/");

while (true) {
    Console.WriteLine("1. Список пользователей.");
    Console.WriteLine("2. Детальный просмотр");

    var option = Console.ReadKey();

    switch (option.KeyChar)
    {
        case '1':
            PrintList();
            break;
        case '2':
            PrintDetails();
            break;
        default:
            break;
    }
}

static void PrintList()
{
    int page = 1;
    int pageSize = 5;
    SortOptions sort;
    List<FilterDto> filters;

    while(true)
    {
        try
        {
            Console.WriteLine($"Страница: {page}. Размер страницы {pageSize}.");
            Console.WriteLine("Table");
            Console.WriteLine("Действия: <, >, Добавить, sort:<Столбец>, filter:<столбец>,<данные сортировки>, exit");
            var option = Console.ReadLine();
        }
        catch 
        {
            continue;
        }
    }
}

static void PrintDetails()
{
    Console.WriteLine("Введите логин");
    var option = Console.ReadLine();

    Console.WriteLine("User");
    Console.WriteLine("Действия: Удалить, Изменить, exit");
}

static void ChangeUser(User user)
{
    Console.WriteLine("Введите новые данные (пустое - без измен)");
}


