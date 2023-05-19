using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class Education
{
    public int Id { get; set; }

    public string Major { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public decimal? Gpa { get; set; }

    public int? UniversityId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Profiling>? Profilings { get; set; } = new List<Profiling>();
    [JsonIgnore]
    public virtual University? University { get; set; }
}
