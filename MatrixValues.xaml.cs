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
using System.Windows.Shapes;

namespace ProblemPlecakowy
{
    /// <summary>
    /// Logika interakcji dla klasy MatrixValues.xaml
    /// </summary>
    public partial class MatrixValues : Window
    {
        public MatrixValues()
        {
            InitializeComponent();
            text1.Text = "0";
        }
        public int text
        {
                get { return Convert.ToInt32(text1.Text); }
        
        } 



        private void Ok(object sender, RoutedEventArgs e)

        {
            this.Close();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
