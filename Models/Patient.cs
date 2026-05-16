namespace MedicalRegistryApi.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Diagnosis { get; set; }

        public string? Treatment { get; set; }
    }
}