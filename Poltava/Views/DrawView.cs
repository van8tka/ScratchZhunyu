using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Poltava.Utils;

namespace Poltava.Views
{
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
}