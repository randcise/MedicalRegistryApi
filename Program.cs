using Microsoft.EntityFrameworkCore;
using MedicalRegistryApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MedicalDbContext>(options =>
    options.UseInMemoryDatabase("MedicalDb"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MedicalDbContext>();

    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/patients", async (MedicalDbContext db) =>
{
    return await db.Patients.ToListAsync();
});

app.MapGet("/patients/{id}", async (int id, MedicalDbContext db) =>
{
    var patient = await db.Patients.FindAsync(id);

    return patient is not null
        ? Results.Ok(patient)
        : Results.NotFound();
});


app.MapPost("/patients", async (Patient patient, MedicalDbContext db) =>
{
    db.Patients.Add(patient);

    await db.SaveChangesAsync();

    return Results.Created($"/patients/{patient.Id}", patient);
});

app.MapPut("/patients/{id}", async (int id, Patient inputPatient, MedicalDbContext db) =>
{
    var patient = await db.Patients.FindAsync(id);

    if (patient is null)
    {
        return Results.NotFound();
    }

    patient.FullName = inputPatient.FullName;
    patient.Diagnosis = inputPatient.Diagnosis;
    patient.Treatment = inputPatient.Treatment;

    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("/patients/{id}", async (int id, MedicalDbContext db) =>
{
    var patient = await db.Patients.FindAsync(id);

    if (patient is null)
    {
        return Results.NotFound();
    }

    db.Patients.Remove(patient);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();