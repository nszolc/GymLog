using System.ComponentModel.DataAnnotations;
using GymLog.Domain.Enums;

namespace GymLog.Application.DTOs.Exercises;

public record UpdateExerciseDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; init; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; init; }
    
    [Required]
    public MuscleGroup MuscleGroup { get; init; }
    
    [Required]
    public ExerciseType ExerciseType { get; init; }
}