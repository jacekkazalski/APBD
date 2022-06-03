namespace probne_kolokwium01.Models
{
    public class Prescription_Medicament
    {
        public int  IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }


    }
}
