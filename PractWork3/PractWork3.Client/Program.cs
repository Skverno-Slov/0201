using PractWork3.Server.Dtos;
using PractWork3.Server.Models;
using System.Net.Http.Json;

using var client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5230/api/");

while (true) {
    Console.WriteLine("1. Список пользователей.");
    Console.WriteLine("2. Детальный просмотр");
    Console.WriteLine("3. Добавление пользователя");
    Console.WriteLine("4. Изменение пользователя");
    Console.WriteLine("5. Удаление пользователя");

    var option = Console.ReadKey();

    switch (option.KeyChar)
    {
        case '1':
            PrintList();
            break;
        case '2':
            break;
        case '3':
            break;
        case '4':
            break;
        case '5':
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
            client.GetFromJsonAsync("");
        }
        catch 
        {
            continue;
        }
    }
}
