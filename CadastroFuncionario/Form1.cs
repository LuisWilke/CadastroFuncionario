using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Importa o SQlite
using System.Data.SQLite;
using System.Net.NetworkInformation;

namespace CadastroFuncionario
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            conectar_banco();

            listar_itens();


            //Prepara a datagridview para receber o duplo click
            lista.CellDoubleClick += new DataGridViewCellEventHandler(lista_CellDoubleClick);

        }

        private void lista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //Obtem a linha selecionada
            DataGridViewRow linhaSelecionada = lista.Rows[e.RowIndex];

            //Obtem os dados associados a linha
            string id = linhaSelecionada.Cells["Id"].Value.ToString();
            string nome = linhaSelecionada.Cells["Nome"].Value.ToString();
            string email = linhaSelecionada.Cells["Email"].Value.ToString();
            string cargo = linhaSelecionada.Cells["Cargo"].Value.ToString();
            string sexo = linhaSelecionada.Cells["Genero"].Value.ToString();

            frmEditar dados = new frmEditar(id, nome, email, cargo, sexo);
            dados.ShowDialog();
        }
        public void conectar_banco()
        {
            
            //Define o caminho do arquivo de banco de dados
            string baseDados = Application.StartupPath + @"\db\BancoDadosQLite.db";

            //Define a string de conexão com o banco de dados SQLite
            string strConection = @"Data Source = " + baseDados + "; Version = 3"; 

            //Cria uma nova conexão com o banco de dados usando a string de conexão 
            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {
                conexao.Open();

                lblMensagens.Text = "Conectado com sucesso!!";

            }catch (Exception ex)
            {

                lblMensagens.Text = "Erro ao conectado ao banco de dados!!" + ex;

            }
            finally { 

                //Fecha a conexão
                conexao.Close(); }
            }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void listar_itens()
        {

            //Limpa as linhas do datagrid
            lista.Rows.Clear();

            //Define o caminho do arquivo de banco de dados
            string baseDados = Application.StartupPath + @"\db\BancoDadosQLite.db";

            //Define a string de conexão com o banco de dados SQLite
            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            //Cria uma nova conexão com o banco de dados usando a string de conexão 
            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {

                //Consulta no banco de dados
                string query = "SELECT * FROM Contatos";

                //Cria uma datatable para armazenar os dados 
                DataTable dados = new DataTable();

                // Cria um adaptador SQLITE para executar a consulta e preencher o datable com os resultados
                SQLiteDataAdapter adaptador = new SQLiteDataAdapter(query, strConection);
                
                //Abre a conexão com o banco de dados
                conexao.Open();

                //Oreenche o dataTable com os resultados da consultados usando o método Fill
                adaptador.Fill(dados);


                //Adiciona as linhas do DATatable ao datagridview
                foreach(DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }

                //Obtem o numero de itens na lista e exibe no label
                int numeroItens = lista.Rows.Count -1;
                lblMensagens.Text = $"Total de itens: {numeroItens}";


            }
            catch (Exception ex)
            {

                lista.Rows.Clear();
                lblMensagens.Text = ex.Message;

            }
            finally {  

                //Fecha a cobexão com banco de dados
                conexao.Close(); 
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {   
            //Cria uma instancia do formulario de cadastro
            frmCadastrar abrirTela = new frmCadastrar ();

            //Abre a tela de cadastro
            abrirTela.ShowDialog ();    
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            filtrar();

        }

        public void filtrar()
        {

            //Limpa as linhas do datagrid
            lista.Rows.Clear();

            //Define o caminho do arquivo de banco de dados
            string baseDados = Application.StartupPath + @"\db\BancoDadosQLite.db";

            //Define a string de conexão com o banco de dados SQLite
            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            //Cria uma nova conexão com o banco de dados usando a string de conexão 
            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {

                //Consulta no banco de dados
                string query = "SELECT * FROM Contatos WHERE Nome LIKE '%' || @nome || '%'";

                //Cria uma datatable para armazenar os dados 
                DataTable dados = new DataTable();

                // Cria um adaptador SQLITE para executar a consulta e preencher o datable com os resultados
                SQLiteDataAdapter adaptador = new SQLiteDataAdapter(query, strConection);

                adaptador.SelectCommand.Parameters.AddWithValue("@nome", txtNome.Text);

                //Abre a conexão com o banco de dados
                conexao.Open();

                //Oreenche o dataTable com os resultados da consultados usando o método Fill
                adaptador.Fill(dados);


                //Adiciona as linhas do DATatable ao datagridview
                foreach (DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }

                //Obtem o numero de itens na lista e exibe no label
                int numeroItens = lista.Rows.Count - 1;
                lblMensagens.Text = $"Total de itens: {numeroItens}";


            }
            catch (Exception ex)
            {

                lista.Rows.Clear();
                lblMensagens.Text = ex.Message;

            }
            finally
            {

                //Fecha a cobexão com banco de dados
                conexao.Close();
            }
        }

    }
}
