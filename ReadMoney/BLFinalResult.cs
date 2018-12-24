using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadMoney
{
    public class BLFinalResult
    {
        #region List

        public List<MLFinalResult> List()
        {
            string query = "SELECT * FROM FinalResult";
            List<MLFinalResult> result = new List<MLFinalResult>();

            try
            {
                using(MySqlConnection conn = new DBConnect().connection)
                {
                    conn.Open();

                    using (MySqlDataReader dataReader = new MySqlCommand(query, conn).ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            result.Add(new MLFinalResult()
                            {
                                Ativo = dataReader["Ativo"].ToString(),
                                TipoAtivo = Convert.ToInt32(dataReader["TipoAtivo"]),
                                Nome = dataReader["Nome"].ToString(),
                                LT = Convert.ToInt32(dataReader["LT"]),
                                CFA = Convert.ToDouble(dataReader["CFA"]),
                                CAb = Convert.ToDouble(dataReader["CAb"]),
                                CFA_STP = Convert.ToInt32(dataReader["CFA_STP"]),
                                CAb_STP = Convert.ToInt32(dataReader["CAb_STP"]),
                                VFA = Convert.ToDouble(dataReader["VFA"]),
                                VAb = Convert.ToDouble(dataReader["VAb"]),
                                VFA_STP = Convert.ToInt32(dataReader["VFA_STP"]),
                                VAb_STP = Convert.ToInt32(dataReader["VAb_STP"]),
                                VarS100 = Convert.ToDouble(dataReader["VarS100"]),
                                VarSBal = Convert.ToDouble(dataReader["VarSBal"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        #endregion

        #region Insert

        public void Insert(List<MLFinalResult> lstAcoes)
        {
            string query = "INSERT INTO FinalResult VALUES";
            string values = "";

            foreach (var acao in lstAcoes)
            {
                values += String.Format(", ('{0}',{1},'{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},'{14}')",
                    acao.Ativo,
                    acao.TipoAtivo,
                    acao.Nome,
                    acao.LT,
                    acao.CFA.ToString().Replace(',','.'),
                    acao.CAb.ToString().Replace(',', '.'),
                    acao.CFA_STP,
                    acao.CAb_STP,
                    acao.VFA.ToString().Replace(',', '.'),
                    acao.VAb.ToString().Replace(',', '.'),
                    acao.VFA_STP,
                    acao.VAb_STP,
                    acao.VarS100.ToString().Replace(',', '.'),
                    acao.VarSBal.ToString().Replace(',', '.'),
                    acao.DtUltimaLeitura.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            query += values.TrimStart(',');

            try
            {
                using (MySqlConnection conn = new DBConnect().connection)
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Update

        public void Update(List<MLFinalResult> lstAcoes)
        {
            string query = "";

            foreach (var acao in lstAcoes)
            {
                query += String.Format(@"UPDATE FinalResult SET 
                                            TipoAtivo = {1}, 
                                            Nome = '{2}', 
                                            LT = {3},
                                            CFA = {4},
                                            CAb = {5},
                                            CFA_STP = {6},
                                            CAb_STP = {7},
                                            VFA = {8},
                                            VAb = {9},
                                            VFA_STP = {10},
                                            VAb_STP = {11},
                                            VarS100 = {12},
                                            VarSBal = {13},
                                            DtUltimaLeitura = '{14}' 
                                        WHERE Ativo = '{0}'",
                                        acao.Ativo,
                                        acao.TipoAtivo,
                                        acao.Nome,
                                        acao.LT,
                                        acao.CFA.ToString().Replace(',', '.'),
                                        acao.CAb.ToString().Replace(',', '.'),
                                        acao.CFA_STP,
                                        acao.CAb_STP,
                                        acao.VFA.ToString().Replace(',', '.'),
                                        acao.VAb.ToString().Replace(',', '.'),
                                        acao.VFA_STP,
                                        acao.VAb_STP,
                                        acao.VarS100.ToString().Replace(',', '.'),
                                        acao.VarSBal.ToString().Replace(',', '.'),
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            try
            {
                using (MySqlConnection conn = new DBConnect().connection)
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM FinalResult WHERE name='John Smith'";

            try
            {
                using (MySqlConnection conn = new DBConnect().connection)
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-----------------------

        #region List Trader Names

        public List<MLTraderName> ListTraderNames()
        {
            string query = "SELECT * FROM TraderName WHERE IsAtivo = 1";
            List<MLTraderName> result = new List<MLTraderName>();

            try
            {
                using (MySqlConnection conn = new DBConnect().connection)
                {
                    conn.Open();

                    using (MySqlDataReader dataReader = new MySqlCommand(query, conn).ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            result.Add(new MLTraderName()
                            {
                                Nome = dataReader["Nome"].ToString(),
                                IsAtivo = Convert.ToBoolean(dataReader["IsAtivo"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        #endregion
    }
}
