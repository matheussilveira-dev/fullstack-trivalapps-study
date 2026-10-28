using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrivalTasks.Api.Models;
using TrivalTasks.Api.Settings;

namespace TrivalTasks.Api.Services;

public class TaskService
{
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskService(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString
        );

        var database = mongoClient.GetDatabase(
            settings.Value.DatabaseName
        );

        _tasks = database.GetCollection<TaskItem>(
            settings.Value.CollectionName
        );
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _tasks
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        await _tasks.InsertOneAsync(task);

        return task;
    }
}