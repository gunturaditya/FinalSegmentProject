using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class Employee
{
    public string Nik { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime HiringDate { get; set; }

    public Gender Gender { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? DepartmentId { get; set; }
    [JsonIgnore]
    public virtual Account? Account { get; set; }
    [JsonIgnore]
    public virtual Department? Department { get; set; }
    [JsonIgnore]
    public virtual Profiling? Profiling { get; set; }
    [JsonIgnore]
    public virtual ICollection<Status>? Statuses { get; set; } = new List<Status>();
}
public enum Gender
{
    Male, Female
}
