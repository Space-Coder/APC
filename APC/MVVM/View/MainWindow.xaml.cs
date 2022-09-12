using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace APC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InfoBar.ShowMessage("Test1 sdkfjsdkg hgadjghajdg Test1 sdkfjsdkg hgadjghajdg Test1 sdkfjsdkg hgadjghajdg" +
                "Test1 sdkfjsdkg hgadjghajdgTest1 sdkfjsdkg hgadjghajdgTest1 sdkfjsdkg hgadjghajdgTest1 sdkfjsdkg", InfoBarStatus.CRITICAL);
            
        }
    }
}
