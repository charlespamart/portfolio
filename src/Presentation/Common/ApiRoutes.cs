namespace Presentation.Common;

public static class ApiRoutes
{
    public const string Root = "api/v{version:apiVersion}";
    
    public static class Todo
    {
        public const string CreateTodo = "todos";
        public const string GetTodos = "todos";
        public const string GetTodo = "todos/{TodoId}";
    }
}