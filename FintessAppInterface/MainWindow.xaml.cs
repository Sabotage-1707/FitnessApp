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
        IDataSaver manager = new SerializableSaver();
        CultureInfo currentCulture = CultureInfo.CreateSpecificCulture("ru-ru");
        public MainWindow()
        {
            InitializeComponent();
            HideAllUserFields();
        
            FillAllFields(currentCulture);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserGender.Visibility == Visibility.Hidden)
                {
                    userController = new UserController(UserName.Text,manager);
                    if (userController.IsNewUser)
                    {
                        ShowAllUserFields();
                    }
                    else
                    {
                        MessageBox.Show(userController.CurrentUser.ToString());
                        UserArea.Visibility = Visibility.Hidden;
                        ToDoArea.Visibility = Visibility.Visible;
                        eatingController = new EatingController(userController.CurrentUser,manager);
                        exerciseController = new ExerciseController(userController.CurrentUser,manager);
                    }
                }
                else
                {
                    var resourseManager = new ResourceManager("FitnessAppInterface.Languages.Messages", typeof(MainWindow).Assembly);
                    userController.SetNewUserData(UserGender.Text,
                                                  DateTime.Parse(UserBirthday.Text),
                                                  double.Parse(UserWeight.Text),
                                                  double.Parse(UserHeight.Text));
                    MessageBox.Show(userController.CurrentUser.ToString() +" "+ userController.CurrentUser.Age +"("+ resourseManager.GetString("Years", currentCulture)+")");
                    UserArea.Visibility = Visibility.Hidden;
                    ToDoArea.Visibility = Visibility.Visible;
                    eatingController = new EatingController(userController.CurrentUser,manager);
                    exerciseController = new ExerciseController(userController.CurrentUser,manager);


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
        private void FillAllFields(CultureInfo culture)
        {

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
            ShowFoodAreaButton.Content = resourseManager.GetString("ShowFood", culture);
            ShowExerciseButton.Content = resourseManager.GetString("ShowCompletedExercises", culture);
            FoodNameLabel.Content = resourseManager.GetString("foodName", culture);
            FoodCalloriesLabel.Content = resourseManager.GetString("foodCallories", culture);
            FoodFatsLabel.Content = resourseManager.GetString("foodFats", culture);
            FoodCarbohydratesLabel.Content = resourseManager.GetString("foodCarbohydrates", culture);
            FoodProteinsLabel.Content = resourseManager.GetString("foodProteins", culture);
            FoodButton.Content = resourseManager.GetString("AddFood", culture);
            ShowAllFoods.Content = resourseManager.GetString("ShowEatenFood", culture);
            AddExerciseButton.Content = resourseManager.GetString("AddExercise", culture);
            ShowExersiceAreaButton.Content = resourseManager.GetString("ShowExercises", culture);
            Apply.Content = resourseManager.GetString("LogIn", culture);
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

        private void RussianLanguage_Click(object sender, RoutedEventArgs e)
        {
            var culture = CultureInfo.CreateSpecificCulture("ru-ru");
            currentCulture = culture;
            FillAllFields(culture);
        }
        private void EnglishLanguage_Click(object sender, RoutedEventArgs e)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            currentCulture = culture;
            FillAllFields(culture);
        }
        
        private void SaveAndLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            manager = new SerializableSaver();
            HideAdditionalAreas();

        }
        private void SaveAndLoadFromDataBase_Click(object sender, RoutedEventArgs e)
        {
            manager = new DatabaseSaver();
            HideAdditionalAreas();

        }
        private void HideAdditionalAreas()
        {
            FoodArea.Visibility = Visibility.Hidden;
            ExerciseArea.Visibility = Visibility.Hidden;
            ToDoArea.Visibility = Visibility.Hidden;
            UserArea.Visibility = Visibility.Visible;
            HideAllUserFields();
        }
    }
}
