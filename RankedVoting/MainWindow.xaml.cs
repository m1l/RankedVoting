using Microsoft.Win32;
using Ranked_voting;
using System;
using System.Collections.Generic;
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

namespace RankedVoting
{
    public partial class MainWindow : Window
    {
        string _csvFilePath = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                _csvFilePath = openFileDialog.FileName;
                string[] columns = ParseCSVColumnsFromFile(_csvFilePath);
                if (columns != null)
                {
                    PopulateListBoxWithColumns(columns);
                    PreselectColumns();
                }
                else
                    MessageBox.Show("Couldn't find columns");
            }
        }

        private void PreselectColumns()
        {
            if (ColumnsLB.Items.Count > 2)
                for (int i = 1; i < ColumnsLB.Items.Count; i++)
                    ColumnsLB.SelectedItems.Add(ColumnsLB.Items[i]);
        }

        private string[] ParseCSVColumnsFromFile(string filepath)
        {
            using (var fileStream = File.OpenRead(filepath))
            using (var streamReader = new StreamReader(fileStream, true))
            {
                string? line = streamReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    return line.Split(',');
            }
            return null;
        }

        private void PopulateListBoxWithColumns(string[] columns)
        {
            ColumnsLB.Items.Clear();
            foreach (var column in columns)
                ColumnsLB.Items.Add(column);
        }

        private void ProcessBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_csvFilePath))
            {
                MessageBox.Show("Load CSV file first");
                return;
            }
            if (ColumnsLB.SelectedItems.Count > 1)
            {
                List<int> columnsWithChoices = new List<int>();
                foreach (var item in ColumnsLB.SelectedItems)
                    columnsWithChoices.Add(ColumnsLB.Items.IndexOf(item));
                Voting.ProcessBallotsFromCSV(_csvFilePath, columnsWithChoices);
            } 
            else if (ColumnsLB.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select all columns contaning voting choices. You can select one, hold shift and click last that has a choice.");
            }
            else if (ColumnsLB.SelectedItems.Count == 1)
            {
                MessageBox.Show("Select all columns contaning voting choices. Ranked-Choice voting must have more than one voting preference");
            }
        }
    }
}
