using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class ParentIndexViewModel
    {
        public List<ChildInfoModel> ChildInfos = new List<ChildInfoModel>();
    }

    public class ChildInfoModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}