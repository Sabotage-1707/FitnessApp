using FintnessAppBusinessLogic.Control;
using FintnessAppBusinessLogic.Model;
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
        EatingController eatingController;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserGender.Visibility == Visibility.Hidden)
                {
                    userController = new UserController(UserName.Text);
                    if (userController.IsNewUser)
                    {
                        ShowUserFields();
                    }
                    else
                    {
                        MessageBox.Show(userController.CurrentUser.ToString());
                        FoodArea.Visibility = Visibility.Visible;
                        UserArea.Visibility = Visibility.Hidden;
                        eatingController = new EatingController(userController.CurrentUser);
                    }
                }
                else
                {
                    userController.SetNewUserData(UserGender.Text,
                                                  DateTime.Parse(UserBirthday.Text),
                                                  double.Parse(UserWeight.Text),
                                                  double.Parse(UserHeight.Text));
                    MessageBox.Show(userController.CurrentUser.ToString());
                }
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message,"Некорректный формат");
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Некорректный Аргумент");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Непредвиденная ошибка");
            }



        }
        private void ShowUserFields()
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

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eatingController.Add(new Food(FoodName.Text,
                                                      double.Parse(FoodProteins.Text),
                                                      double.Parse(FoodCallories.Text),
                                                      double.Parse(FoodFats.Text),
                                                      double.Parse(FoodCarbohydrates.Text)),
                                                      double.Parse(FoodWeight.Text));
                MessageBox.Show("Успешно добавлено!");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Некорректный формат");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Некорректный Аргумент");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Непредвиденная ошибка");
            }


        }

        private void ShowAllFoods_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in eatingController.Eating.Foods)
            {
                MessageBox.Show($"Блюдо {item.Key} - {item.Value} грамм)");
            }
        }
    }
}
