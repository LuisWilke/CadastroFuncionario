using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CadastroFuncionario
{
    public partial class frmCadastrar : Form
    {
        public frmCadastrar()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\BancoDadosQLite.db";

            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {

                //Abre a conexão com banco de dados
                conexao.Open();


                //Cria um comando sql para inserção de dados
                SQLiteCommand comando = new SQLiteCommand();
                comando.Connection = conexao;
                

                //Cria as variaveis e pega os valores dos campos
                string nome = txtNome.Text;
                string email = txtEmail.Text;
                string cargo = cmbCargo.SelectedItem.ToString();
                string sexo = cmbSexo.SelectedItem.ToString();

                //Define a consulta sql par inserção dos dados na tabela
                comando.CommandText = "INSERT INTO Contatos (nome, email, cargo, sexo) VALUES (@nome, @email, @cargo, @sexo)";
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@cargo", cargo);
                comando.Parameters.AddWithValue("@sexo", sexo);

                //Executa a consulta no banco de dados 
                comando.ExecuteNonQuery();

                //Libera recursos do objeto comando
                comando.Dispose();

                //Obtem uma referencia a uma instancia existente
                frmPrincipal principal = (frmPrincipal)Application.OpenForms["frmPrincipal"];
                
                //Chama o metodo par atualizar a lista de itens do formulario principal
                principal.listar_itens();

                //Força a atualização da tela do formulario principal
                principal.Refresh();

                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Fecha o formulário
                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao salvar os dados: " + ex.Message, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally { 

                //Fecha a conexão com banco de dados
                conexao.Close();
            }

        }

        private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
