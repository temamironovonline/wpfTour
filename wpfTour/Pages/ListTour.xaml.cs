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
            int index = typeOfTours.SelectedIndex;
            List<Tour> toursFromDB = DataBaseConnection.tourEntities.Tour.ToList();
            List<Tour> tours = new List<Tour>();
            List<Tour> toursForType = new List<Tour>();

            Regex regex = new Regex($@".*{searchTextBox.Text.ToLower()}.*");

            tours = toursFromDB.Where(x => regex.IsMatch(x.Name.ToLower())).ToList();

            if (actualToursCheckBox.IsChecked == true)
                tours = tours.Where(x => x.IsActual == true).ToList();

            if (index > 0)
            {
                List<TypeOfTour> typeOfTours = DataBaseConnection.tourEntities.TypeOfTour.Where(x => x.TypeId == index).ToList();
                foreach (TypeOfTour typeOfTour in typeOfTours)
                {
                    toursForType.Add(tours.FirstOrDefault(x => x.Id == typeOfTour.TourId));
                }
                tours = toursForType;
            }

            tourList.ItemsSource = tours;
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
