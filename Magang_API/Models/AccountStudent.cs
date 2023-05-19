using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class AccountStudent
{
    public string AccountStudentId { get; set; } = null!;

    public string? Password { get; set; }
    [JsonIgnore]
    public virtual Student? AccountStudentNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual AccountStudentRole? AccountStudentRole { get; set; }
}
