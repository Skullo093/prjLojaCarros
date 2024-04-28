using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Marcas : Form
    {
        int veiculoAtual = 0;
        int totalVeiculos = 0;
        DataTable dtMarcas = new DataTable();
        String connectionString = @"Server=darnassus\motorhead;Database=db_230110;Password=luar1317";
        bool novo;
        public Marcas()
        {
            InitializeComponent();
        }

        private void navegar()
        {
            txtcodMarca.Text = dtMarcas.Rows[veiculoAtual][0].ToString();
            txtnomeMarca.Text = dtMarcas.Rows[veiculoAtual][1].ToString();
        }
        private void Carregar()
        {
            string sql = "Select * from ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtMarcas.Load(reader);
                    totalVeiculos = dtMarcas.Rows.Count;
                    veiculoAtual = 0;
                    navegar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void Marcas_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtcodMarca.Enabled = false;
            txtnomeMarca.Enabled = false;
            string sql = "Select * from ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtMarcas.Load(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            txtcodMarca.Clear();
            txtcodMarca.Enabled = true;
            txtnomeMarca.Clear();
            txtnomeMarca.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            txtcodMarca.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(novo)
            {
                string sql = "INSERT INTO  ()" + "VALUES('" + txtcodMarca.Text + "', '" +
                txtnomeMarca.Text + "')";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if(i > 0)
                    {
                        MessageBox.Show("Marca cadastrada com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
            else
            {
                string sql = "UPDATE  set ='" + txtnomeMarca.Text + "' WHERE =" +
                txtcodMarca.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Marca atualizada com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            btnSalvar.Enabled = false;
            txtcodMarca.Enabled = false;
            txtnomeMarca.Enabled = false;
            btnNovo.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnPrimeiro.Enabled = true;
            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
            btnUltimo.Enabled = true;
            Carregar();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE from  WHERE  =" + txtcodMarca.Text;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Marca atualizada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            Carregar();

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            btnSalvar.Enabled = true;
            txtnomeMarca.Enabled = true;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (veiculoAtual > 0)
            {
                veiculoAtual = 0;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (veiculoAtual > 0)
            {
                veiculoAtual -= 1;
                navegar();
            }

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (veiculoAtual == totalVeiculos - 1)
            {
                veiculoAtual += 1;
                navegar();
            }

        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (veiculoAtual < totalVeiculos - 1)
            {
                veiculoAtual = totalVeiculos - 1;
                navegar();
            }

        }
    }
}
