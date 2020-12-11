using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Poltava.Utils;

namespace Poltava.Views
{
    public class SheepView : View
    {
        private int _positionX, _positionY;
        private int HeightScreen, WidthScreen;
        double imageHeight;
        double imageWidth;
        private Context _context;
        public int PositionX
        {
            get => _positionX; set
            {
                if (value >= 0 && value <= WidthScreen - imageWidth)
                    _positionX = value;
                else
                    BorderMsg();
            }
        }

        public int PositionY
        {
            get => _positionY; set
            {
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

        public SheepView(Context context) : base(context)
        {
            _context = context;
            var screen = ScreenSizePx.GetSize(context as Activity);
            HeightScreen = screen.Item1;
            WidthScreen = screen.Item2;
            PositionY = HeightScreen / 2;
            PositionX = WidthScreen / 2;
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
}