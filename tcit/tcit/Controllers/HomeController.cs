﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tcit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult RedirectToSwagger()
        {
            return Redirect("~/swagger");
        }
    }
}
