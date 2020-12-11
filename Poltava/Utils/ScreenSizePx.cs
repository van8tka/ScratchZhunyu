using Android.App;
using Android.Util;
using System;

namespace Poltava.Utils
{
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