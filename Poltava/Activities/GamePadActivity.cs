using Android.App;
using Android.OS;
using Android.Widget;
using Poltava.Enumerables;
using Poltava.Interfaces;
using Poltava.Services;

namespace Poltava.Activities
{
    [Activity(Label = "GamePadActivity")]
    public class GamePadActivity : Activity, IPlayerAction
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

        public async void MoveDown()
        {
           await _webService.SendStep(Dirrection.Down);
        }

        public async void MoveUp()
        {
            await _webService.SendStep(Dirrection.Up);
        }

        public async void MoveRight()
        {
            await _webService.SendStep(Dirrection.Right);
        }

        public async void MoveLeft()
        {
            await _webService.SendStep(Dirrection.Left);
        }
    }
}