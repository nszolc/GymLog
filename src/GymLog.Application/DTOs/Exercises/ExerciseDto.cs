using GymLog.Domain.Enums;

namespace GymLog.Application.DTOs.Exercises;

public record ExerciseDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public MuscleGroup MuscleGroup { get; init; }
    public ExerciseType ExerciseType { get; init; }
    public bool IsCustom { get; init; }
}