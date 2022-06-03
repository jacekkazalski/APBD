namespace probne_kolokwium01.Models
{
    public class Prescription
    {
        public int IdPrescription { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
    }
}
