namespace BrosMed_Insurance.Models.Reservation
{
    public class Godzina
    {
        public int Id { get; set; }
        public string godzinaVM { get; set; }
        public IEnumerable<Godzina> Godzinki { get; set; }
    }
}
