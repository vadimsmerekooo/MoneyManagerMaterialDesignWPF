using System;
using System.Collections.Generic;

namespace MonyeManager
{
    [Serializable]
    public class Pays
    {
        public MaterialDesignThemes.Wpf.PackIconKind Kind { get; set; }
        public string ToolTip { get; set; }
        public string Name { get; set; }
        public string CountPay { get; set; }
        public string PaysMoney { get; set; }
        public string MaxPay { get; set; }
        public double Progress { get { return 100 / Convert.ToDouble(MaxPay) * Convert.ToDouble(PaysMoney); } set {  } }
        public Pays()
        {

        }
    }
}
