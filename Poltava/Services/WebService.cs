using Poltava.Enumerables;
using Poltava.Interfaces;
using System.Threading.Tasks;

namespace Poltava.Services
{
    public class WebService : IWebService
    {
      

        //todo implement SendCodeConnect
        public async Task<bool> SendCodeConnect(string code, string type_controller)
        {
            await Task.Yield();
            return true;
        }

        public async Task<Dirrection> GetStep()
        {
            await Task.Yield();
            return Dirrection.Unknown;
        }

        public async Task SendStep(Dirrection down)
        {
            await Task.Yield();            
        }
    }
}