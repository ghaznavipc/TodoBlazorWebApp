using Microsoft.EntityFrameworkCore;
using TodoWebApp.Models;

namespace TodoWebApp.Data;

public class TodoService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ILogger<TodoService> _logger;

    public TodoService(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILogger<TodoService> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<List<TodoItem>> GetTodosAsync()
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.TodoItems
                .OrderByDescending(t => t.CreateDate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting todos");
            throw;
        }
    }

    public async Task<TodoItem?> GetTodoByIdAsync(int id)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.TodoItems.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting todo with id {Id}", id);
            throw;
        }
    }

    public async Task<TodoItem> CreateTodoAsync(string title)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var todo = new TodoItem
            {
                Title = title?.Trim(),
                CreateDate = DateTime.UtcNow,
                IsDone = false
            };

            context.TodoItems.Add(todo);
            await context.SaveChangesAsync();
            return todo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating todo");
            throw;
        }
    }

    public async Task<TodoItem> UpdateTodoAsync(TodoItem todo)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var existingTodo = await context.TodoItems.FindAsync(todo.Id);

            if (existingTodo == null)
                throw new InvalidOperationException($"Todo with id {todo.Id} not found");

            existingTodo.Title = todo.Title?.Trim() ?? string.Empty;
            existingTodo.IsDone = todo.IsDone;

            if (todo.IsDone && !existingTodo.CompleteDate.HasValue)
                existingTodo.CompleteDate = DateTime.UtcNow;
            else if (!todo.IsDone)
                existingTodo.CompleteDate = null;

            await context.SaveChangesAsync();
            return existingTodo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating todo with id {Id}", todo.Id);
            throw;
        }
    }

    public async Task ToggleTodoStatusAsync(int id)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var todo = await context.TodoItems.FindAsync(id);

            if (todo == null)
                throw new InvalidOperationException($"Todo with id {id} not found");

            todo.IsDone = !todo.IsDone;

            if (todo.IsDone)
                todo.CompleteDate = DateTime.UtcNow;
            else
                todo.CompleteDate = null;

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling todo with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteTodoAsync(int id)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var todo = await context.TodoItems.FindAsync(id);

            if (todo == null)
                throw new InvalidOperationException($"Todo with id {id} not found");

            context.TodoItems.Remove(todo);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting todo with id {Id}", id);
            throw;
        }
    }
}
