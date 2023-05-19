using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    public string? Password { get; set; }
    [JsonIgnore]
    public virtual Employee? AccountNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual AccountRole? AccountRole { get; set; }
}
