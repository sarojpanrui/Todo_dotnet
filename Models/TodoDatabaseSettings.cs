using System;

namespace TodoApi.Models
{
    public class TodoDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public string TodoCollection { get; set; } = string.Empty;
    }
}