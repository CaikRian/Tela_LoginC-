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
using System.Linq.Expressions;

namespace LoginCharpAverso
{
    public partial class LoginC : Form
    {
        //referencia de Conexão
        SqlConnection Conexao = new SqlConnection(@"Data Source=NOT-CAIKRIAN;Initial Catalog=LoginCharp;Integrated Security=True");
        public LoginC()
        {
            InitializeComponent();
        }
        //verifica as textbox vazias
        void verificar()
        {
            if(txtUsuario.Text=="" || txtSenha.Text == "")
            {
                MessageBox.Show("Preencha os campos Abaixo!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Botão Entrar
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Conexao.Open();//abri a conexão com o sql server
            verificar();
            string query = "SELECT*FROM Usuario WHERE Username = '" + txtUsuario.Text + "' AND Password = '" + txtSenha.Text + "'";
            SqlDataAdapter dp = new SqlDataAdapter(query, Conexao);
            DataTable dt = new DataTable();
            dp.Fill(dt);
            try
            {
                if (dt.Rows.Count == 1)
                {
                    FormPrincipal principal = new FormPrincipal();
                    this.Hide();
                    principal.Show();
                    
                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorreto!!! Tente Novamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Text = "";
                    txtSenha.Text = "";
                    txtUsuario.Select();//cursos irá focar na txt usuario
                }
            }
            catch(Exception erro)
            {
                MessageBox.Show("ERRO 0101."+erro);
                txtUsuario.Text = "";
                txtSenha.Text = "";
                txtUsuario.Select();
            }
            Conexao.Close();//fecha a conexão

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
