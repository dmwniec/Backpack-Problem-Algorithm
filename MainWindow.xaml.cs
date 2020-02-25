using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using static ProblemPlecakowy.Solution;

namespace ProblemPlecakowy
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

      

        private void Generate(object sender, RoutedEventArgs e)
        {
            MatrixValues value = new MatrixValues();
            value.Owner = this;
            value.ShowDialog();
            int n = 0;
            n = value.text;
            Matrix matrix = Matrix.GenerateRandomMatrix(n);
            DataTable datatable = Matrix.MatrixToDataTable(matrix);
            data.ItemsSource = datatable.DefaultView;
            

        }


        private void Load(object sender, RoutedEventArgs e)
        {
            MatrixValues value = new MatrixValues();
            value.Owner = this;
            value.ShowDialog();
            int n = 0;
            n = value.text;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Text documents (.txt)|*.txt";
            

            
            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
      
            if (result == true)
            {
               
                filename = dlg.FileName;
            }
            Matrix matrix = Matrix.LoadFromFile(n, filename);
            DataTable datatable = Matrix.MatrixToDataTable(matrix);
            data.ItemsSource = datatable.DefaultView;
            

        }

        private void Create(object sender, RoutedEventArgs e)
        {
            MatrixValues itemCount = new MatrixValues();
            itemCount.Owner = this;
            itemCount.ShowDialog();
            int n = 0;
            n = itemCount.text;
            Matrix matrix = new Matrix(n);
            DataTable datatable = Matrix.MatrixToDataTable(matrix);
            data.ItemsSource = datatable.DefaultView;

            
        }

       

        private void Solutions(object sender, RoutedEventArgs e)
        {
            DataTable datatable = ((DataView)data.ItemsSource).ToTable();
            Matrix matrix = Matrix.DatatableToMatrix(datatable);
            int maxweight = Convert.ToInt32(maxWeight.Text);
            greedyresult.Content = Solution.Greedy(matrix, maxweight);
            Result result = Solution.Dynamic(matrix, maxweight);
            dynamicresult.Content = result.BackpackValue;
            DataTable dynamicItems = new DataTable();
            dynamicItems.Columns.Add("Lista Przedmiotów");
            
            int i = 0;
            DataRow itemRow = dynamicItems.NewRow();
            foreach (int item in result.ItemList)
            {
                dynamicItems.Columns.Add("" + (i + 1));
                itemRow[i + 1] = item;
                i++;
            }
            itemRow[0] = "Nr przedmiotu";
            dynamicItems.Rows.Add(itemRow);
            helpdata.ItemsSource = dynamicItems.DefaultView;

        }

        private void FileSave(object sender, RoutedEventArgs e)
        {
            
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                DataTable datatable = ((DataView)data.ItemsSource).ToTable();
                Matrix matrix = Matrix.DatatableToMatrix(datatable);
                Matrix.PrintToFile(matrix, saveFileDialog1.FileName);
            }


            
        }


       
    }
}
