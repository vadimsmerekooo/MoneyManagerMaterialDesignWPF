using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MonyeManager
{
    public partial class StonksPays : Page
    {
        public StonksPays()
        {
            InitializeComponent();
            PaysStonksChildren[] stonks = MainWindow.listPayStonks.ToArray();
            Array.Reverse(stonks);
            MainItemsControl.ItemsSource = stonks;
        }
    }
}
