using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Poltava.Interfaces;
using Poltava.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poltava.Activities.Fragments
{
    public class CodeConnectFragment : DialogFragment
    {
        private TextView tvCodeConnection;
        private IWebService _webService;

        public bool IsDestruct { get; internal set; }

        public static CodeConnectFragment NewInstance(Bundle bundle)
        {
            var fragment = new CodeConnectFragment();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _webService = new WebService();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {            
            var v = inflater.Inflate(Resource.Layout.fragment_code_connect, container, false);
            tvCodeConnection = v.FindViewById<TextView>(Resource.Id.tvCodeConn);
            var btnContinue = v.FindViewById<Button>(Resource.Id.btnContinueCodeConn);
            btnContinue.Click += (s, e) => { IsDestruct = true; Dismiss(); };
            return v;           
        }


        public override void OnStart()
        {
            base.OnStart();
            var code = GetRandomCode().ToString();
            tvCodeConnection.Text = code;
            _webService.SendCodeConnect(code, "battle_field");
        }
         
        private int GetRandomCode()
        {          
            Random rnd = new Random();
            return rnd.Next(100000000, 999999999);
        }

    }
}