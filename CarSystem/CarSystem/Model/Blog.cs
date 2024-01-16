namespace CarSystem.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string ImgUrl { get; set; }
        public string? Description { get; set; }
        public string Quote { get; set; }


    }
}
