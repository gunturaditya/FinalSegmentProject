using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Models;

public partial class Profiling
{
    public string ProfilingId { get; set; } = null!;

    public int EducationId { get; set; }

    [JsonIgnore]
    public virtual Education Education { get; set; } = null!;

    [JsonIgnore]
    public virtual Employee ProfilingNavigation { get; set; } = null!;
}
