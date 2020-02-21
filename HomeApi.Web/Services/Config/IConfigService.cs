using System.Threading.Tasks;

namespace HomeApi.Web.Services.Config
{
    public interface IConfigService
    {
        Configuration Config { get; }

        bool IsDevelopmentMode { get; }

        void Save();

        Task SaveAsync();
    }
}
