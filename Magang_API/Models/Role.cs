using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();
    [JsonIgnore]
    public virtual ICollection<AccountStudentRole> AccountStudentRoles { get; set; } = new List<AccountStudentRole>();
}
