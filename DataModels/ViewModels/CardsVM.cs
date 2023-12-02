namespace DataModels.ViewModels
{
    public class CardsVM
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public CardsVM() 
        {
            Name = string.Empty;
            Status = string.Empty;
        }
    }
}
