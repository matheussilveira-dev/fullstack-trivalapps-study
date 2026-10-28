using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrivalTasks.Api.Models;

public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("completed")]
    public bool Completed { get; set; }
}