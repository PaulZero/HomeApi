namespace HomeApi.Web.Services.Lighting.Models
{
    public class Light
    {
        public string Id { get; }
        
        public string Name { get; }

        public Light(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
