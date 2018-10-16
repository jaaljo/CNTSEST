using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;

namespace CNTS.ViewModels
{
    public class DirectionViewModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Id { get; set; }
    }
}