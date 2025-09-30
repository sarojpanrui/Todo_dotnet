using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{

    public class TodoItem
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TodoStatus status { get; set; } = TodoStatus.InProgress;



        public TodoPriority priority { get; set; } = TodoPriority.Medium;
        public DateTime CreatedAt
        { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; } = "user";

    }

    public enum TodoStatus
    {
        pending,
        InProgress,

        Resolved

    }

    public enum TodoPriority
    {
        Low,
        Medium,
        High
    }


}