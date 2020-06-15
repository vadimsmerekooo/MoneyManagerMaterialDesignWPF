using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MonyeManager
{
    [Serializable]
    public class PaysStonks
    {
        public MaterialDesignThemes.Wpf.PackIconKind Kind
        {
            get
            {
                return MainWindow.categoryList[Category];
            }
            set
            {

            }
        }
        public string Pay { get; set; }
        public string Category { get; set; }
        public string ShopName { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }

        public PaysStonks()
        {

        }
    }
    public class PaysStonksChildren : PaysStonks
    {
        public Brush Brushs
        {
            get
            {
                return Pay == "-" ? new SolidColorBrush(Color.FromRgb(235, 93, 98)) : new SolidColorBrush(Color.FromRgb(127, 205, 81));
            }
            set
            {

            }
        }
        public PaysStonksChildren()
        {

        }
    }
}
