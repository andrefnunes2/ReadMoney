using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;

namespace ReadMoney
{
    public partial class ReadMoney : Form
    {
        public ReadMoney()
        {
            InitializeComponent();
        }
        private bool GCloseRecursive;
        private string GFilePath = @"D:\Projetos\Outros\ReadMoney\Files\PRINCIPAL-25-09.xlsm";

        private int countTeste = 0;

        private void ReadMoney_Load(object sender, EventArgs e)
        {
            
        }
       
        private void AnalyzeFile(object sender, EventArgs e)
        {
            countTeste++;

            List<MLFinalResult> lstFinalResult = new BLFinalResult().Listar();

            /*Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(GFilePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[8];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            lblInfo.Text = countTeste.ToString() + "- " + xlRange.Cells[2, 2].Value2.ToString();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);*/

            if (!GCloseRecursive) AnalyzeFile(null, new EventArgs());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            GCloseRecursive = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GCloseRecursive = false;
            AnalyzeFile(null, new EventArgs());
        }
    }
}
