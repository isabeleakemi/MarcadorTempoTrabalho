using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace MarcadorTempoTrabalho
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        DAL dal = new DAL();

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
            horaAtualTextBox.Text = DateTime.Now.ToShortTimeString();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var telaCadastro = new CadastroMarcador();
            telaCadastro.ShowDialog();
        }

        private void button2_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dal.Excluir(int.Parse(codigoTextBox.Text));
                MessageBox.Show("Sucesso!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            horaAtualTextBox.Text = DateTime.Now.ToShortTimeString();
        }

        private void pesquisaButton_Click(object sender, RoutedEventArgs e)
        {
            PesquisaMarcador telaPesquisa = new PesquisaMarcador();
            telaPesquisa.ShowDialog();

            var linha = telaPesquisa.LinhaDataGrid();

            codigoTextBox.Text = linha.Row.Field<long>("id_marcador").ToString();
            descricaoTextBox.Text = linha.Row.Field<string>("descricao");
        }

        private void iniciarButton_Click(object sender, RoutedEventArgs e)
        {
            if (iniciarButton.Content.ToString().Equals("Iniciar"))
            {
                dal.SalvarTempo(int.Parse(codigoTextBox.Text), DateTime.Parse(horaAtualTextBox.Text));
                iniciarButton.Content = "Finalizar";
            }
            else
            {
                dal.AtualizarTempo(int.Parse(codigoTextBox.Text), DateTime.Parse(horaAtualTextBox.Text));
                iniciarButton.Content = "Iniciar";
            }    
        }

        private void tempoTotalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tempoTotalTextBox.Text = dal.ObterTempoTotal(int.Parse(codigoTextBox.Text));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
