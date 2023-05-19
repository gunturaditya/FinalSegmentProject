using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Magang_API.Model;

public partial class Student
{
    public string Nim { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string Major { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public decimal? Gpa { get; set; }

    public bool? IsApproval { get; set; } = null!;

    public int? Score { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? UniversitasId { get; set; }
    [JsonIgnore]
    public virtual AccountStudent? AccountStudent { get; set; }
    [JsonIgnore]
    public virtual Status? Status { get; set; }
    [JsonIgnore]
    public virtual University? Universitas { get; set; }
}
