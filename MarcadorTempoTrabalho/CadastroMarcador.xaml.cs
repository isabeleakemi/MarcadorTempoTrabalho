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
using System.Windows.Shapes;

namespace MarcadorTempoTrabalho
{
    /// <summary>
    /// Lógica interna para CadastroMarcador.xaml
    /// </summary>
    public partial class CadastroMarcador : Window
    {
        DAL dal = new DAL();

        public CadastroMarcador()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dal.Salvar(int.Parse(codigoCadTextBox.Text), descricaoCadTextBox.Text);
                MessageBox.Show("Sucesso!");

                codigoCadTextBox.Text = dal.ObterUltimoId();
                descricaoCadTextBox.Clear();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                codigoCadTextBox.Text = dal.ObterUltimoId();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
