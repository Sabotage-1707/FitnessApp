using FintnessAppBusinessLogic.Control;
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

namespace FitnessAppInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userController = new UserController(UserName.Text,
                                                    UserGender.Text,
                                                    DateTime.Parse(UserBirthday.Text),
                                                    Double.Parse(UserWeight.Text),
                                                    Double.Parse(UserHeight.Text));
            userController.Save();
            var MyUser = new UserController();
            MessageBox.Show(MyUser.User.Name);
        }
    }
}
