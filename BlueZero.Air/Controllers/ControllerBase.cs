using BlueZero.Air.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BlueZero.Air.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ILog _log;

        public ControllerBase(ILog log)
        {
            _log = log;
        }
    }
}