using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoServices
{
    private readonly IMongoCollection<TodoItem> _TodoItem;

    public TodoServices(IOptions<TodoDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);

        var database = client.GetDatabase(settings.Value.DatabaseName);

        _TodoItem = database.GetCollection<TodoItem>(settings.Value.TodoCollection);

        try
        {
            var result = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Result;
            Console.WriteLine(" MongoDB connection established.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" MongoDB connection failed: {ex.Message}");
        }
    }



    public async Task<List<TodoItem>> GetAll()
    {
        return await _TodoItem.Find(todo => true).ToListAsync();
    }


    public async Task<TodoItem?> GetById(string id)
    {
        return await _TodoItem.Find(todo => todo.Id == id).FirstOrDefaultAsync();
    }


    public async Task Add(TodoItem todo)
    {
        await _TodoItem.InsertOneAsync(todo);
    }


    public async Task Update(string id, TodoItem updatedtodo)
    {
        updatedtodo.Id = id;
        await _TodoItem.ReplaceOneAsync(todo => todo.Id == id, updatedtodo);
    }


    public async Task Delete(string id)
    {
        await _TodoItem.DeleteOneAsync(todo => todo.Id == id);
    }


    public async Task<List<TodoItem>> GetByStatus(TodoStatus status)
    {
        // Use Find and ToListAsync
        return await _TodoItem.Find(todo => todo.status == status).ToListAsync();
    }


    public async Task<List<TodoItem>> GetByPriority(TodoPriority priority)
    {
        return await _TodoItem.Find(todo => todo.priority == priority).ToListAsync();
    }





}

