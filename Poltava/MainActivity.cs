using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Poltava.Activities;
using Poltava.Activities.Fragments;
using System.Threading.Tasks;

namespace Poltava
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private Button btnField, btnGamePad;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);          
            SetContentView(Resource.Layout.activity_main);
            btnField = FindViewById<Button>(Resource.Id.btnField);
            btnGamePad = FindViewById<Button>(Resource.Id.btnGamePad);
            btnField.Click += BtnField_Click;
            btnGamePad.Click += BtnGamePad_Click;
        }

        private void BtnGamePad_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(GamePadActivity));
            StartActivity(intent);
        }

        private async void BtnField_Click(object sender, System.EventArgs e)
        {
            var fragment = CodeConnectFragment.NewInstance(null);
            fragment.Show(FragmentManager.BeginTransaction(), "code_dialog");
            await Task.Run(() => {
                while (!fragment.IsDestruct)
                {
                    Task.Delay(500);
                };
            });
           
            var intent = new Intent(this, typeof(FieldActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}