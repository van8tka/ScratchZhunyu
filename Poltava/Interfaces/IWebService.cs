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
        /// <summary>
        /// установка кода сопряжения устройств 
        /// </summary>
        /// <param name="code">код сопряжения любое кол-во символов для связи игрового поля и джойстика в игре</param>
        /// <param name="type_controller">тип устройство: игровое поле, джойстик</param>
        /// <returns></returns>
        Task<bool> SendCodeConnect(string code,string type_controller);        
    }
}