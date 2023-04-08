using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Tasks
{
    public class TasksModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
