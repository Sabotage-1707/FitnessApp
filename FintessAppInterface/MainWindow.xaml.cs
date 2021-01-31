using FintnessAppBusinessLogic.Control;
using FintnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
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
        ExerciseController exerciseController;
        public MainWindow()
        {
            InitializeComponent();
            HideAllUserFields();
            FillAllFields();

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
                        ShowAllUserFields();
                    }
                    else
                    {
                        MessageBox.Show(userController.CurrentUser.ToString());
                        UserArea.Visibility = Visibility.Hidden;
                        ToDoArea.Visibility = Visibility.Visible;
                        eatingController = new EatingController(userController.CurrentUser);
                        exerciseController = new ExerciseController(userController.CurrentUser);
                    }
                }
                else
                {
                    userController.SetNewUserData(UserGender.Text,
                                                  DateTime.Parse(UserBirthday.Text),
                                                  double.Parse(UserWeight.Text),
                                                  double.Parse(UserHeight.Text));
                    MessageBox.Show(userController.CurrentUser.ToString());
                    UserArea.Visibility = Visibility.Hidden;
                    ToDoArea.Visibility = Visibility.Visible;
                    eatingController = new EatingController(userController.CurrentUser);
                    exerciseController = new ExerciseController(userController.CurrentUser);


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
        private void ShowAllUserFields()
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
        private void FillAllFields()
        {

            var culture = CultureInfo.CreateSpecificCulture("ru-ru");
            var resourseManager = new ResourceManager("FitnessAppInterface.Languages.Messages", typeof(MainWindow).Assembly);

            UserNameLabel.Content = resourseManager.GetString("userName", culture);
            UserGenderLabel.Content = resourseManager.GetString("userGender", culture);
            UserWeightLabel.Content = resourseManager.GetString("userWeight", culture);
            UserHeightLabel.Content = resourseManager.GetString("userHeight", culture);
            UserBirthdayLabel.Content = resourseManager.GetString("userBirthday", culture);
            ExerciseNameLabel.Content = resourseManager.GetString("exerciseName", culture);
            ExerciseEnergyLabel.Content = resourseManager.GetString("exerciseEnergy", culture);
            ExerciseStartLabel.Content = resourseManager.GetString("exerciseStart", culture); 
            ExerciseFinishLabel.Content= resourseManager.GetString("exerciseFinish", culture);
        }
        private void HideAllUserFields()
        {
            UserGender.Visibility = Visibility.Hidden;
            UserGenderLabel.Visibility = Visibility.Hidden;
            UserBirthday.Visibility = Visibility.Hidden;
            UserBirthdayLabel.Visibility = Visibility.Hidden;
            UserHeight.Visibility = Visibility.Hidden;
            UserHeightLabel.Visibility = Visibility.Hidden;
            UserWeight.Visibility = Visibility.Hidden;
            UserWeightLabel.Visibility = Visibility.Hidden;
        }

        private void AddExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            var exe = EnterExercise();

            exerciseController.Add(exe.Activity, exe.Begin, exe.End);
           
        }
        private (DateTime Begin, DateTime End, Activity Activity) EnterExercise()
        {
            try
            {
                var name = ExerciseEnergy.Text;
                var energy = double.Parse(ExerciseEnergy.Text);
                var begin = DateTime.Parse(ExerciseStart.Text);
                var end = DateTime.Parse(ExerciseFinish.Text);

                var activity = new Activity(name, energy);
                return (begin, end, activity);
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message, "Некорректный формат поля");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            throw new ArgumentException("");
        }

        private void ShowExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in exerciseController.Exercises)
            {
                MessageBox.Show($"{item.Activity} c {item.Start.ToShortTimeString()} до {item.Finish.ToShortTimeString()}");
            }
        }

        private void ShowFoodAreaButton_Click(object sender, RoutedEventArgs e)
        {
            FoodArea.Visibility = Visibility.Visible;
            ExerciseArea.Visibility = Visibility.Hidden;
        }

        private void ShowExersiceAreaButton_Click(object sender, RoutedEventArgs e)
        {
            FoodArea.Visibility = Visibility.Hidden;
            ExerciseArea.Visibility = Visibility.Visible;
        }
    }
}
