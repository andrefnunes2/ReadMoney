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

            List<MLFinalResult> lstAcoes = new List<MLFinalResult>();

            lstAcoes.Add(new MLFinalResult()
            {
                Ativo = "VLID3",
                TipoAtivo = 2,
                Nome = "André",
                LT = 2,
                CFA = 1.2,
                CAb = 1.2,
                CFA_STP = 2,
                CAb_STP = 2,
                VFA = 1.2,
                VAb = 1.2,
                VFA_STP = 2,
                VAb_STP = 2,
                DtUltimaLeitura = DateTime.Now
            });

            lstAcoes.Add(new MLFinalResult()
            {
                Ativo = "ITUB4",
                TipoAtivo = 3,
                Nome = "Pedro",
                LT = 3,
                CFA = 1.3,
                CAb = 1.3,
                CFA_STP = 3,
                CAb_STP = 3,
                VFA = 1.3,
                VAb = 1.3,
                VFA_STP = 3,
                VAb_STP = 3,
                DtUltimaLeitura = DateTime.Now
            });

            new BLFinalResult().Inserir(lstAcoes);

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

            //if (!GCloseRecursive) AnalyzeFile(null, new EventArgs());
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
