namespace Presentation.FastEndpoints.Common;

public static class ApiRoutes
{
    public static class Todo
    {
        public const string CreateTodo = "todos";
        public const string GetTodos = "todos";
        public const string GetTodo = "todos/{TodoId}";
    }
}