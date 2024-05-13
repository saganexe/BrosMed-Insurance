namespace BrosMed_Insurance.Models.Reservation
{
    public class ReservationViewModel
    {
        
       // public IEnumerable<Godzina> Godzinki { get; set; }
       // public IEnumerable<Usluga> Uslugi { get; set; }
       // public Terminy Terminy { get; set; }

        public DateOnly Data {  get; set; }
        public int SelectedGodzinaId { get; set; } 
        public int SelectedUslugaId { get; set; }
    }
}
