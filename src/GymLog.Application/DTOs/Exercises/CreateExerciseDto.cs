using System.ComponentModel.DataAnnotations;
using GymLog.Domain.Enums;

namespace GymLog.Application.DTOs.Exercises;

public record CreateExerciseDto
{
    public string Name { get; init; } = string.Empty;
    
    public string? Description { get; init; }
    
    public MuscleGroup MuscleGroup { get; init; }
    
    public ExerciseType ExerciseType { get; init; }
}