namespace template.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Family { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
