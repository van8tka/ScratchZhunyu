﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poltava.Interfaces
{
    public interface IWebService
    {
        Task<bool> SendCodeConnect(string code);        
    }
}