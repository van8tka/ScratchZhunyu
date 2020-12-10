using Android.App;
using Android.OS;
using Android.Widget;
using Poltava.Enumerables;
using Poltava.Interfaces;
using Poltava.Services;
using System.Threading.Tasks;

namespace Poltava.Activities
{
    [Activity(Label = "GamePadActivity")]
    public class GamePadActivity : Activity
    {
        private IWebService _webService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GamePadView);
            var btnLeft = FindViewById<Button>(Resource.Id.btnStepLeft);
            var btnRight = FindViewById<Button>(Resource.Id.btnStepRight);
            var btnUp = FindViewById<Button>(Resource.Id.btnStepUp);
            var btnDown = FindViewById<Button>(Resource.Id.btnStepDown);
            btnLeft.Click += (s, e) => MoveLeft();
            btnRight.Click += (s, e) => MoveRight();
            btnUp.Click += (s, e) => MoveUp();
            btnDown.Click += (s, e) => MoveDown();
            _webService = new WebService();
        }

        private async Task MoveDown()
        {
           await _webService.SendStep(Dirrection.Down);
        }

        private async Task MoveUp()
        {
            await _webService.SendStep(Dirrection.Up);
        }

        private async Task MoveRight()
        {
            await _webService.SendStep(Dirrection.Right);
        }

        private async Task MoveLeft()
        {
            await _webService.SendStep(Dirrection.Left);
        }
    }
}