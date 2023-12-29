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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CadastroFuncionario
{
    public partial class frmEditar : Form
    {
        public frmEditar( string id, string nome, string email, string cargo, string sexo)
        {
            InitializeComponent();

            //Preencho os campos com os dados recebidos da nossa lista quando dou um duplo click
            txtID.Text = id;
            txtNome.Text = nome;
            txtEmail.Text = email;
            cmbCargo.Text = cargo;
            cmbSexo.Text = sexo;
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
                
                int id = int.Parse(txtID.Text);
                string nome = txtNome.Text;
                string email = txtEmail.Text;
                string cargo = cmbCargo.SelectedItem.ToString();
                string sexo = cmbSexo.SelectedItem.ToString();

                //Cria a query para alterar as informações no banco
                string query = "UPDATE Contatos SET Nome = '" + nome + "', Email = '" + email + "', Cargo = '" + cargo + "', Sexo = '" + sexo + "' WHERE Id LIKE '" + id + "'";
                comando.CommandText = query;


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

                MessageBox.Show("Dados alterados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Fecha o formulário
                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao alterar os salvar os dados: " + ex.Message, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                //Fecha a conexão com banco de dados
                conexao.Close();
            }

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {

            string baseDados = Application.StartupPath + @"\db\BancoDadosQLite.db";

            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            SQLiteConnection conexao = new SQLiteConnection(strConection);

            int idSelecionado = int.Parse(txtID.Text);

            try
            {

                //Abre a conexão com banco de dados
                conexao.Open();


                //Cria um comando sql para inserção de dados
                SQLiteCommand comando = new SQLiteCommand();
                comando.Connection = conexao;




                //Cria a query para alterar as informações no banco
                comando.CommandText = "DELETE From Contatos WHERE Id = @id";
                comando.Parameters.AddWithValue("@id", idSelecionado);


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

                MessageBox.Show("Registro deletado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Fecha o formulário
                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao alterar os salvar os dados: " + ex.Message, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                //Fecha a conexão com banco de dados
                conexao.Close();
            }

        }
    }
}
