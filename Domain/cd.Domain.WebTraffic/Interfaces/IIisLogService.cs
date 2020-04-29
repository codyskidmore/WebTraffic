using System.Threading.Tasks;

namespace cd.Domain.WebTraffic.Interfaces
{
    public interface IIisLogService
    {
        Task ImportIisLogFiles();
    }
}
