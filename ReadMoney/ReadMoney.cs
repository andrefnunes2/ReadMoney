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
        private int ColumnIndex(string coluna)
        {
            var lstColunas = new string[] {
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                "AA","AB","AC","AD","AE","AF","AG","AH","AI","AJ","AK","AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV","AW","AX","AY","AZ",
                "BA","BB","BC","BD","BE","BF","BG","BH","BI","BJ","BK","BL","BM","BN","BO","BP","BQ","BR","BS","BT","BU","BV","BW","BX","BY","BZ",
                "CA","CB","CC","CD","CE","CF","CG","CH","CI","CJ","CK","CL","CM","CN","CO","CP","CQ","CR","CS","CT","CU","CV","CW","CX","CY","CZ"
            };
            return lstColunas.ToList().FindIndex(f => f == coluna) +1;
        }
        public ReadMoney()
        {
            InitializeComponent();
        }
        private bool GCloseRecursive;
        private string GFilePath = @"D:\Projetos\Outros\ReadMoney\Files\PRINCIPAL-25-09.xlsm";

        private int countTrader = 0;

        private void ReadMoney_Load(object sender, EventArgs e)
        {
            
        }
       
        private void AnalyzeFile(object sender, EventArgs e)
        {
            List<MLFinalResult> lstAcoes = new List<MLFinalResult>();
            List<MLTraderName> lstTraders = new BLFinalResult().ListTraderNames();
            
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(GFilePath);
            Excel.Worksheet xlWs = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item("Análise");
            Excel.Range xlRange = xlWs.UsedRange;

            for (var rw = 3; rw < xlRange.Rows.Count; rw++)
            {
                var lote = Convert.ToInt32(xlRange.Cells[rw, ColumnIndex("X")].Value2.ToString());

                //TODO: Precisa listar os Ativos que já foram inceridos HOJE e somente atualiza-los.
                if (lote > 0)
                {
                    lstAcoes.Add(new MLFinalResult()
                    {
                        Ativo = xlRange.Cells[rw, ColumnIndex("A")].Value2.ToString(),
                        TipoAtivo = 0,
                        Nome = lstTraders[countTrader].Nome,
                        LT = lote,
                        CFA = Math.Round(Convert.ToDouble(xlRange.Cells[rw, ColumnIndex("AB")].Value2.ToString()), 2),
                        CAb = Math.Round(Convert.ToDouble(xlRange.Cells[rw, ColumnIndex("AD")].Value2.ToString()), 2),
                        CFA_STP = Convert.ToInt32(xlRange.Cells[rw, ColumnIndex("AH")].Value2.ToString()),
                        CAb_STP = Convert.ToInt32(xlRange.Cells[rw, ColumnIndex("AK")].Value2.ToString()),
                        VFA = Math.Round(Convert.ToDouble(xlRange.Cells[rw, ColumnIndex("AW")].Value2.ToString()), 2),
                        VAb = Math.Round(Convert.ToDouble(xlRange.Cells[rw, ColumnIndex("AY")].Value2.ToString()), 2),
                        VFA_STP = Convert.ToInt32(xlRange.Cells[rw, ColumnIndex("BC")].Value2.ToString()),
                        VAb_STP = Convert.ToInt32(xlRange.Cells[rw, ColumnIndex("BF")].Value2.ToString()),
                        VarS100 = Math.Round(Convert.ToDouble(xlRange.Cells[rw, ColumnIndex("CC")].Value2.ToString()), 2),
                        VarSBal = Math.Round(Convert.ToDouble(xlRange.Cells[rw, ColumnIndex("CD")].Value2.ToString()), 2),
                        DtUltimaLeitura = DateTime.Now
                    });

                    //Repete a lista de traders, para dividir as ordens listadas
                    countTrader = countTrader >= lstTraders.Count ? 0 : countTrader + 1;
                }
            }
            
            GC.Collect();
            GC.WaitForPendingFinalizers();

            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);


            new BLFinalResult().Insert(lstAcoes);

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
