namespace HomeApi.Web.Services.Lighting.Models
{
    public class Light
    {
        public string Id { get; }

        public bool IsOn { get; }

        public Light(string id, bool isOn)
        {
            Id = id;
            IsOn = isOn;
        }
    }
}
