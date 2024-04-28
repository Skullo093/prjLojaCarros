using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Veiculos : Form
    {
        int veiculoAtual = 0;
        int totalVeiculos = 0;
        DataTable dtVeiculos = new DataTable();
        String connectionString = @"Server=darnassus\motorhead;Database=db_230110;Password=luar1317";
        bool novo;
        public Veiculos()
        {
            InitializeComponent();
        }

        private void navegar()
        {
            carregaComboVeiculos();
            txtCodVeiculo.Text = dtVeiculos.Rows[veiculoAtual][0].ToString();
            txtAno.Text = dtVeiculos.Rows[veiculoAtual][1].ToString();
            txtModelo.Text = dtVeiculos.Rows[veiculoAtual][2].ToString();
            cbbMarca.Text = dtVeiculos.Rows[veiculoAtual][3].ToString();
            cbbTipoVeiculo.Text = dtVeiculos.Rows[veiculoAtual][4].ToString();
        }

        private void carregaTudoVeiculos()
        {
            dtVeiculos = new DataTable();
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
                    dtVeiculos.Load(reader);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            cbbMarca.DataSource = dtVeiculos;
            cbbMarca.DisplayMember = "txtnomeMarca";
            cbbMarca.ValueMember = "txtcodMarca";
        }

        private void carregaComboVeiculos()
        {
            dtVeiculos = new DataTable();
            string sql = "Select * from  WHERE codMarca=" +
            dtVeiculos.Rows[veiculoAtual][3].ToString();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtVeiculos.Load(reader);
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
            cbbMarca.DataSource = dtVeiculos;
            cbbMarca.DisplayMember = "txtnomeMarca";
            cbbMarca.ValueMember = "txtcodMarca";

        }


        private void Veiculos_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodVeiculo.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            cbbMarca.Enabled = false;
            cbbTipoVeiculo.Enabled = false;
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
                    dtVeiculos.Load(reader);
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            txtCodVeiculo.Clear();
            txtModelo.Enabled = true;
            txtModelo.Clear();
            txtAno.Enabled = true;
            txtAno.Clear();
            cbbMarca.Enabled = true;
            cbbTipoVeiculo.SelectedIndex = 0;
            cbbMarca.Enabled = true;
            cbbMarca.SelectedIndex = 0;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            carregaTudoVeiculos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(novo)
            {
                string sql= "INSERT INTO  (, " +
                ") VALUES ('" + txtAno.Text + "', " + txtModelo.Text +
                ", " + cbbMarca.SelectedValue.ToString() + ", '" +
                cbbTipoVeiculo.SelectedValue.ToString() + "')";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if(i > 0)
                    {
                        MessageBox.Show("Veículo cadastrado com sucesso!");
                        this.Veiculos_Load(this, e);
                    }
                }
                catch(Exception ex)
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
                string sql = "UPDATE  set  ='" + txtAno.Text + "',  ="
                + txtModelo.Text + ",  =" + cbbMarca.SelectedValue.ToString()
                + ",  ='" + cbbTipoVeiculo.SelectedValue.ToString() +
                "'WHERE  =" + txtCodVeiculo.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Veículo alterado com sucesso!");
                        this.Veiculos_Load(this, e);
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

                btnSalvar.Enabled = false;
                txtCodVeiculo.Enabled = false;
                txtModelo.Enabled = false;
                txtAno.Enabled = false;
                cbbMarca.Enabled = false;
                cbbTipoVeiculo.Enabled = false;
                btnNovo.Enabled = true;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                btnAnterior.Enabled = true;
                btnProximo.Enabled = true;
                btnPrimeiro.Enabled = true;
                btnUltimo.Enabled = true;
                dtVeiculos = new DataTable();
                Veiculos_Load(this, e);

            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE from  WHERE  =" + txtCodVeiculo.Text;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Veículo excluído com sucesso!");
                    this.Veiculos_Load(this, e);
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
            dtVeiculos = new DataTable();
            Veiculos_Load(this, e);

        }
        private void button4_Click(object sender, EventArgs e)
        {
            novo = false;
            txtModelo.Enabled = true;
            txtAno.Enabled = true;
            cbbMarca.Enabled = true;
            cbbMarca.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            carregaTudoVeiculos();

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
