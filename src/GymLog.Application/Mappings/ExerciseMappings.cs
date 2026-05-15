using GymLog.Application.DTOs.Exercises;
using GymLog.Domain.Entities;

namespace GymLog.Application.Mappings;

public static class ExerciseMappings
{
    // Entity → DTO (to execute API response)
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Description = exercise.Description,
            MuscleGroup = exercise.MuscleGroup,
            ExerciseType = exercise.ExerciseType,
            IsCustom = exercise.IsCustom
        };
    }

    // CreateDto → Entity (to put in database)
    public static Exercise ToEntity(this CreateExerciseDto dto)
    {
        return new Exercise
        {
            Name = dto.Name,
            Description = dto.Description,
            MuscleGroup = dto.MuscleGroup,
            ExerciseType = dto.ExerciseType,
            IsCustom = true  // wszystko utworzone przez API = custom
        };
    }

    // UpdateDto → Entity (update existing entity)
    public static void UpdateEntity(this UpdateExerciseDto dto, Exercise exercise)
    {
        exercise.Name = dto.Name;
        exercise.Description = dto.Description;
        exercise.MuscleGroup = dto.MuscleGroup;
        exercise.ExerciseType = dto.ExerciseType;
    }
}