namespace CarSystem.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string Phone { get; set; }

        public string? Description { get; set; }

        public double Total { get; set; }
        public int UserId { get; set; }

      

    }
}
