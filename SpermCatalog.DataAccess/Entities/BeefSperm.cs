﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Entities
{
    public class BeefSperm
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string BREED { get; set; }
        public string NAME { get; set; }

        public double SCE { get; set; }
        public double CR { get; set; }
        public double DM { get; set; }
        public double PCAR { get; set; }
        public double RDT { get; set; }
        public double CONF { get; set; }
        public double COUL { get; set; }
        public double GRAS { get; set; }
        public double IAB { get; set; }
        public double ICRC { get; set; }

        public string SIRE { get; set; }
        public string MGS { get; set; } 
        public int Price { get; set; }

        public bool IsNew { get; set; }
        public int CustomOrder { get; set; } = 999;
    }
}
