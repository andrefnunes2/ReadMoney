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
        #region Listar

        public List<MLFinalResult> Listar()
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
                                VAb_STP = Convert.ToInt32(dataReader["VAb_STP"])
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

        public void Inserir(List<MLFinalResult> lstAcoes)
        {
            string query = "INSERT INTO FinalResult VALUES";
            string values = "";

            foreach(var acao in lstAcoes)
            {
                values += String.Format(",({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})",
                    acao.Ativo,
                    acao.TipoAtivo,
                    acao.Nome,
                    acao.LT,
                    acao.CFA,
                    acao.CAb,
                    acao.CFA_STP,
                    acao.CAb_STP,
                    acao.VFA,
                    acao.VAb,
                    acao.VFA_STP,
                    acao.VAb_STP,
                    acao.DtUltimaLeitura.ToString("yyyy-MM-dd HH:mm:ss"));
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

        //Update statement
        public void Update(List<MLFinalResult> lstAcoes)
        {
            string query = "UPDATE FinalResult SET name='Joe', age='22' WHERE name='John Smith'";

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
    }
}
