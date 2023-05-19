using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class AccountRole
{
    public string AccountId { get; set; } = null!;

    public int? RoleId { get; set; }
    [JsonIgnore]
    public virtual Account? Account { get; set; } = null!;
    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
