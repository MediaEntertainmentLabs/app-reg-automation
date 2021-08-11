using System.Threading.Tasks;

namespace AppRegPortal.Services
{
    public interface IAppRegistrationService
    {
        Task<string> Test();
    }
}