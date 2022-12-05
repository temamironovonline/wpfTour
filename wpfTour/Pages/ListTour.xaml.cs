using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Text.RegularExpressions;
using System;

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
            priceOfTours.SelectedIndex = 0;
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


        List<Tour> tours = DataBaseConnection.tourEntities.Tour.ToList();
        private void SearchData()
        {
            int indexTypeOfTours = typeOfTours.SelectedIndex;
            int indexPriceOfTours = priceOfTours.SelectedIndex;
            
            List<Tour> toursFromDB = new List<Tour>();
            
            Regex regexName = new Regex($@".*{searchNameTextBox.Text.ToLower()}.*");
            Regex regexDescription = new Regex($@".*{searchDescriptionTextBox.Text.ToLower()}.*");

            if (indexTypeOfTours > 0)
            {
                List<TypeOfTour> typeOfTours = DataBaseConnection.tourEntities.TypeOfTour.Where(x => x.TypeId == indexTypeOfTours).ToList();
                foreach(TypeOfTour typeOfTour in typeOfTours)
                {
                    toursFromDB.Add(tours.FirstOrDefault(x => x.Id == typeOfTour.TourId));
                }

            }
            else
            {
                toursFromDB = DataBaseConnection.tourEntities.Tour.ToList();
            }

            toursFromDB = toursFromDB.Where(x => regexName.IsMatch(x.Name.ToLower())).ToList();
            toursFromDB = toursFromDB.Where(x => regexDescription.IsMatch(x.Description.ToLower())).ToList();

            if (actualToursCheckBox.IsChecked == true)
                toursFromDB = toursFromDB.Where(x => x.IsActual == true).ToList();

            if (indexPriceOfTours == 1)
                toursFromDB = toursFromDB.OrderBy(x => x.Price).ToList();
            else if (indexPriceOfTours == 2)
                toursFromDB = toursFromDB.OrderByDescending(x => x.Price).ToList();

            int countPrice = 0;
            foreach(Tour tour in toursFromDB)
            {
                countPrice += Convert.ToInt32(tour.Price);
            }

            countAllTours.Text = countPrice.ToString();
            tourList.ItemsSource = toursFromDB;
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
