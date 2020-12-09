using Poltava.Interfaces;
using System.Threading.Tasks;

namespace Poltava.Services
{
    public class WebService : IWebService
    {
        //todo implement SendCodeConnect
        public async Task<bool> SendCodeConnect(string code)
        {
            await Task.Yield();
            return true;
        }
    }
}