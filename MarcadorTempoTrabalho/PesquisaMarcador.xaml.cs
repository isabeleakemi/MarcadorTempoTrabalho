using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica interna para PesquisaMarcador.xaml
    /// </summary>
    public partial class PesquisaMarcador : Window
    {
        DAL dal = new DAL();
        DataRowView linha;

        public PesquisaMarcador()
        {
            InitializeComponent();

            dataGrid.ItemsSource = dal.ObterMarcadores().DefaultView;
        }

        public DataRowView LinhaDataGrid()
        {
            return linha;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            linha = (DataRowView)dataGrid.SelectedItem;
            Close();
        }
    }
}
