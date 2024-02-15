namespace Presentation.FastEndpoints.Common;

public static class ApiRoutes
{
    private const string BaseUrl = "api/v{version:apiVersion}";

    public static class Todo
    {
        public const string CreateTodo = BaseUrl + "todos";
        public const string GetTodos = BaseUrl + "todos";
        public const string GetTodo = BaseUrl + "todo/{TodoId}";
    }
}