using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [JsonIgnore]
    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
