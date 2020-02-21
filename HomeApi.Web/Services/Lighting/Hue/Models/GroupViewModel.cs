using Q42.HueApi.Models.Groups;

namespace HomeApi.Web.Services.Lighting.Hue.Models
{
    public class GroupViewModel
    {
        public string Id => group.Id;

        public string Name => group.Name;

        public string[] LightIds => group.Lights.ToArray();

        private readonly Group group;

        public GroupViewModel(Group group)
        {
            this.group = group;
        }
    }
}
