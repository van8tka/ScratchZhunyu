using Android.App;
using Android.OS;
using Android.Widget;
using Poltava.Activities.Fragments;
using Poltava.Interfaces;
using Poltava.Services;
using Poltava.Views;
using System.Threading.Tasks;

namespace Poltava.Activities
{
    [Activity(Label = "FieldActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class FieldActivity : Activity, IPlayerAction
    {
        private DrawView _drawField;
        private SheepView _sheepView;
        private IWebService _webService;
        private int stepSize = 50;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _drawField = new DrawView(this);
            SetContentView(Resource.Layout.FieldView);        
            var frame = FindViewById<FrameLayout>(Resource.Id.frFieldView);
            _drawField.LayoutParameters = new LinearLayout.LayoutParams(frame.LayoutParameters);         
            frame.AddView(_drawField);
            _sheepView = new SheepView(this);
            frame.AddView(_sheepView);
            _webService = new WebService();
            CheckStep();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            var fragment = CodeConnectFragment.NewInstance(null);
            fragment.Show(FragmentManager.BeginTransaction(), "code_dialog");          
        }
 
        private async void CheckStep()
        {
            while(true)
            {
                var dirrect = await _webService.GetStep();
                switch (dirrect)
                {
                    case Enumerables.Dirrection.Left:
                        MoveLeft();
                        break;
                    case Enumerables.Dirrection.Right:
                        MoveRight();
                        break;
                    case Enumerables.Dirrection.Up:
                        MoveUp();
                        break;
                    case Enumerables.Dirrection.Down:
                        MoveDown();
                        break;
                    default:
                        break;
                }
                await Task.Delay(1000);              
            }
                    
        }

       
        public void MoveDown()
        {
            _sheepView.PositionY += stepSize;
            _sheepView.Invalidate();
        }

        public void MoveUp()
        {
            _sheepView.PositionY -= stepSize;
            _sheepView.Invalidate();
        }

        public void MoveRight()
        {
            _sheepView.PositionX += stepSize;
            _sheepView.Invalidate();
        }

        public void MoveLeft( )
        {
            _sheepView.PositionX -= stepSize;
            _sheepView.Invalidate();
        }
    }
 
}