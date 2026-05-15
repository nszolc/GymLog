using GymLog.Application.DTOs.Exercises;
using GymLog.Application.Mappings;
using GymLog.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymLog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly GymLogDbContext _db;

    public ExercisesController(GymLogDbContext db)
    {
        _db = db;
    }

    // GET /api/exercises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
    {
        var exercises = await _db.Exercises
            .AsNoTracking()
            .ToListAsync();
        
        var dtos = exercises.Select(e => e.ToDto());
        return Ok(dtos);
    }

    // GET /api/exercises/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExerciseDto>> GetById(int id)
    {
        var exercise = await _db.Exercises
            .AsNoTracking() // no modification
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (exercise is null)
            return NotFound();
        
        return Ok(exercise.ToDto());
    }

    // POST /api/exercises
    [HttpPost]
    public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseDto dto)
    {
        var exercise = dto.ToEntity();
        
        _db.Exercises.Add(exercise);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = exercise.Id },
            exercise.ToDto()
        );
    }

    // PUT /api/exercises/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateExerciseDto dto)
    {
        var exercise = await _db.Exercises.FindAsync(id);
        
        if (exercise is null)
            return NotFound();
        
        dto.UpdateEntity(exercise);
        await _db.SaveChangesAsync();
        
        return NoContent();
    }

    // DELETE /api/exercises/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exercise = await _db.Exercises.FindAsync(id);
        
        if (exercise is null)
            return NotFound();
        
        _db.Exercises.Remove(exercise);
        await _db.SaveChangesAsync();
        
        return NoContent();
    }
}