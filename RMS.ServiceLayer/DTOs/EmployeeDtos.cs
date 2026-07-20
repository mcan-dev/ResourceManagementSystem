using System.ComponentModel.DataAnnotations;

namespace RMS.ServiceLayer.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public int? TeamId { get; set; }
    public string? TeamName { get; set; }
    public int? TitleId { get; set; }
    public string? TitleName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? UserRole { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateEmployeeRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Surname { get; set; } = string.Empty;

    [StringLength(10)]
    public string? Gender { get; set; }

    public int TeamId { get; set; }

    public int TitleId { get; set; }

    [StringLength(100)]
    public string? Username { get; set; }

    [EmailAddress]
    [StringLength(250)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? Password { get; set; }

    [StringLength(250)]
    public string? UserRole { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "active";
}

public class UpdateEmployeeRequest
{
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Surname { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    public int? TeamId { get; set; }

    public int? TitleId { get; set; }

    [StringLength(100)]
    public string? Username { get; set; }

    [EmailAddress]
    [StringLength(250)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? Password { get; set; }

    [StringLength(250)]
    public string? UserRole { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }
}
