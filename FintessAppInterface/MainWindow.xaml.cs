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
        UserController userController;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (UserGender.Visibility == Visibility.Hidden)
            {
                userController = new UserController(UserName.Text);
                if (userController.IsNewUser)
                {
                    ShowFields();
                }
                else
                {
                    MessageBox.Show(userController.CurrentUser.ToString());
                }
            }
            else
            {
                userController.SetNewUserData(UserGender.Text,
                                              DateTime.Parse(UserBirthday.Text),
                                              Double.Parse(UserWeight.Text),
                                              Double.Parse(UserHeight.Text));
                MessageBox.Show(userController.CurrentUser.ToString());
            }
            
                                                   
            

        }
        private void ShowFields()
        {
            UserGender.Visibility = Visibility.Visible;
            UserGenderLabel.Visibility = Visibility.Visible;
            UserBirthday.Visibility = Visibility.Visible;
            UserBirthdayLabel.Visibility = Visibility.Visible;
            UserHeight.Visibility = Visibility.Visible;
            UserHeightLabel.Visibility = Visibility.Visible;
            UserWeight.Visibility = Visibility.Visible;
            UserWeightLabel.Visibility = Visibility.Visible;
            Apply.Content = "Добавить";
        }
    }
}
