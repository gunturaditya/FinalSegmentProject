﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class AccountStudentRole
{
    public string AccountStudentId { get; set; } = null!;

    public int? RoleStudentId { get; set; }
    [JsonIgnore]
    public virtual AccountStudent? AccountStudent { get; set; } = null!;
    [JsonIgnore]
    public virtual Role? RoleStudent { get; set; }
}
