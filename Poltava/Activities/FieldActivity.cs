using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Poltava.Interfaces;
using Poltava.Services;
using System;
using System.Threading.Tasks;

namespace Poltava.Activities
{
    [Activity(Label = "FieldActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class FieldActivity : Activity
    {
        private DrawView _drawField;
        private SheepView _sheepView;
        private IWebService _webService;
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

        private int stepSize = 50;
        private void MoveDown()
        {
            _sheepView.PositionY += stepSize;
            _sheepView.Invalidate();
        }

        private void MoveUp()
        {
            _sheepView.PositionY -= stepSize;
            _sheepView.Invalidate();
        }

        private void MoveRight()
        {
            _sheepView.PositionX += stepSize;
            _sheepView.Invalidate();
        }

        private void MoveLeft( )
        {
            _sheepView.PositionX -= stepSize;
            _sheepView.Invalidate();
        }
    }

    public class SheepView : View
    {
        private int _positionX, _positionY;
        private int HeightScreen, WidthScreen;
        double imageHeight;
        double imageWidth;
        private Context _context;
        public int PositionX { get => _positionX; set {
                if (value >= 0 && value <= WidthScreen - imageWidth )
                    _positionX = value;
                else
                    BorderMsg();
            }
        }

        public int PositionY
        {
            get => _positionY; set {
                if (value >= 0 && value <= HeightScreen - imageHeight)
                    _positionY = value;
                else
                    BorderMsg();
            }
        }

        private void BorderMsg()
        {
            Toast.MakeText(_context, "Вы достигли границы игрового поля!", ToastLength.Short).Show();
        }

        public SheepView(Context context):base(context)
        {
            _context = context;
            var screen = ScreenSizePx.GetSize(context as Activity);
            HeightScreen = screen.Item1;
            WidthScreen = screen.Item2;
            PositionY = HeightScreen/2;
            PositionX = WidthScreen/2;
        }

     

        protected override void OnDraw(Canvas canvas)
        {
            Bitmap bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.sheep);
            imageHeight = bitmap.Height;
            imageWidth = bitmap.Width;
            canvas.DrawBitmap(bitmap, PositionX, PositionY, null);
            base.OnDraw(canvas);
        }

    }


    public class DrawView : View
    {
        private int _width;
        private int _height;
        public DrawView(Context context) : base(context)
        {
            var screen = ScreenSizePx.GetSize(context as Activity);
            _width = screen.Item2;
            _height = screen.Item1;
        }
        protected override void OnDraw(Canvas canvas)
        {
            Bitmap bitmapRaw = BitmapFactory.DecodeResource(Resources, Resource.Drawable.see_map1024);
            Bitmap bitmap = Bitmap.CreateScaledBitmap(bitmapRaw, _width, _height, false);
            canvas.DrawBitmap(bitmap, 0, 0, null);
            base.OnDraw(canvas);
        }
    }

    public static class ScreenSizePx
    {
        public static Tuple<int, int> GetSize(Activity activity)
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            return new Tuple<int, int>(displayMetrics.HeightPixels, displayMetrics.WidthPixels);
        }
    }
}