using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Poltava.Enumerables;
using Poltava.Interfaces;
using Poltava.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poltava.Activities.Fragments
{
    public class CodeEnterFragment : DialogFragment
    {
        private EditText edCodeEnter;
        private Button btnContinueCodeEdt;
        private IWebService _webService;
        public bool IsDestruct { get; private set; }
        public static CodeEnterFragment NewInstance(Bundle bundle)
        {
            var fragment = new CodeEnterFragment();
            fragment.Arguments = bundle;
            return fragment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _webService = new WebService();
             var v = inflater.Inflate(Resource.Layout.fragment_code_enter, container, false);
            edCodeEnter = v.FindViewById<EditText>(Resource.Id.edCodeEnter);
            btnContinueCodeEdt = v.FindViewById<Button>(Resource.Id.btnContinueCodeEdt);
            edCodeEnter.TextChanged += (s, e) => btnContinueCodeEdt.Enabled = true;          
            btnContinueCodeEdt.Click += BtnContinueCodeEdt_Click;
            return v;
        }

        private async void BtnContinueCodeEdt_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(edCodeEnter.Text))
            {
               await _webService.SendCodeConnect(edCodeEnter.Text, TypeGameController.GamePad.ToString());
               IsDestruct = true; Dismiss(); 
            }
        }
    }
}