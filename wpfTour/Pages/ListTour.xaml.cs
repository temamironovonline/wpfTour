using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Text.RegularExpressions;

namespace wpfTour
{
    /// <summary>
    /// Логика взаимодействия для ListTour.xaml
    /// </summary>
    public partial class ListTour : Page
    {
        public ListTour()
        {
            InitializeComponent();
            typeOfTours.SelectedIndex = 0;
            List<Type> types = DataBaseConnection.tourEntities.Type.ToList();
            foreach(Type type in types)
            {
                typeOfTours.Items.Add(type.Name);
            }
            List <Tour> tours = DataBaseConnection.tourEntities.Tour.ToList();
            string path;
            path = Directory.GetCurrentDirectory();
            path = path.Replace("\\wpfTour\\bin\\Debug", "");
            
            foreach (Tour tour in tours)
            {
                tour.ImagePreview = path+tour.ImagePreview;
            }
            tourList.ItemsSource = tours;
        }

        private void SearchData()
        {
            int index = tourList.SelectedIndex;
            List <Tour> tours = DataBaseConnection.tourEntities.Tour.ToList();
            List<TypeOfTour> typeOfTours = DataBaseConnection.tourEntities.TypeOfTour.ToList();

            Regex regex = new Regex($@".*{searchTextBox.Text.ToLower()}.*");

            tourList.ItemsSource = tours.Where(x => regex.IsMatch(x.Name.ToLower()));
            if (actualToursCheckBox.IsChecked == true)
                tourList.ItemsSource = tours.Where(x => x.IsActual == true);
            if (index != 0)
                tourList.ItemsSource = tours.Where(x => typeOfTours);


        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchData();
        }

        private void actualToursCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchData();
        }

        private void typeOfTours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchData();
        }
    }
}
