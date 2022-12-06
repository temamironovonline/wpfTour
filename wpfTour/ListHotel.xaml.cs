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

namespace wpfTour
{
    /// <summary>
    /// Логика взаимодействия для ListHotel.xaml
    /// </summary>
    public partial class ListHotel : Page
    {
        public ListHotel()
        {
            InitializeComponent();
            hotelList.ItemsSource = DataBaseConnection.tourEntities.Hotel.ToList();
        }

        private void countHotelInToursTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            int index = Convert.ToInt32(textBlock.Uid);

            textBlock.Text = $"Количество имеющихся туров в отель: {DataBaseConnection.tourEntities.HotelOfTour.Where(x => x.HotelId == index).Count()}";
        }

        private void updateHotelButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Uid);
            Hotel hotel = DataBaseConnection.tourEntities.Hotel.FirstOrDefault(x => x.Id == index);
            
            AddHotelWindow adw = new AddHotelWindow(hotel);
            adw.ShowDialog();
            FrameClass.frameClass.Navigate(new ListHotel());
        }

        private void deleteHotelButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Uid);

            if (DataBaseConnection.tourEntities.HotelOfTour.Where(x => x.HotelId == index).Count() != 0)
            {
                MessageBox.Show("Невозможно удалить отель, так как он находится в числе подходящих отелей для актуальных туров");
            }
            else
            {
                Hotel hotel = DataBaseConnection.tourEntities.Hotel.FirstOrDefault(x => x.Id == index);
                DataBaseConnection.tourEntities.Hotel.Remove(hotel);
                DataBaseConnection.tourEntities.SaveChanges();
                FrameClass.frameClass.Navigate(new ListHotel());
                MessageBox.Show("Успешное удаление отеля");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frameClass.Navigate(new ListTour());
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddHotelWindow adw = new AddHotelWindow();
            adw.ShowDialog();
            FrameClass.frameClass.Navigate(new ListHotel());
        }
    }
}
