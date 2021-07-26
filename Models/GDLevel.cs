using Newtonsoft.Json;
namespace LiphiBot2.Models
{
    public class GDLevel
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public int Stars { get; set; }
        public bool Featured { get; set; }
        public bool Epic { get; set; }
        public string SongName { get; set; }
        public string SongAuthor { get; set; }

    }
}