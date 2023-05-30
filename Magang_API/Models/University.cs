using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Models;

public partial class University
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    [JsonIgnore]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
