﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Tasks
{
    public class TasksModel
    {
       
        public int id { get; set; }

        public string name { get; set; }

        public string ToQuery()
        {
            return $"INSERT INTO `tasks`(`id`, `name`) VALUES ('{id}','{name}')";
        }
    }
}
