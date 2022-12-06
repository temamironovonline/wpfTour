using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace wpfTour
{
    /// <summary>
    /// Логика взаимодействия для ListHotel.xaml
    /// </summary>
    public partial class ListHotel : Page
    {

        Pagination pagination = new Pagination();  // создаем объект класса для отображения страниц
        List<Hotel> hotels = new List<Hotel>();

        public ListHotel()
        {
            InitializeComponent();

            hotels = DataBaseConnection.tourEntities.Hotel.ToList();
            hotelList.ItemsSource = DataBaseConnection.tourEntities.Hotel.ToList();
            pagination.CountPage = 10;
            pagination.Countlist = hotels.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            hotelList.ItemsSource = hotels.Skip(0).Take(pagination.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            DataContext = pagination;
            
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

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pagination.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pagination.CountPage = hotels.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pagination.Countlist = hotels.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            hotelList.ItemsSource = hotels.Skip(0).Take(pagination.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            pagination.CurrentPage = 1; // текущая страница - это страница 1
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)  // обработка нажатия на один из Textblock в меню с номерами страниц
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pagination.CurrentPage--;
                    break;
                case "next":
                    pagination.CurrentPage++;
                    break;
                default:
                    pagination.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            hotelList.ItemsSource = hotels.Skip(pagination.CurrentPage * pagination.CountPage - pagination.CountPage).Take(pagination.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
            // Skip(pc.CurrentPage* pc.CountPage - pc.CountPage) - сколько пропускаем записей
            // Take(pc.CountPage) - сколько записей отображаем на странице
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            pagination.CurrentPage = 1;

            try
            {
                pagination.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pagination.CountPage = hotels.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pagination.Countlist = hotels.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            hotelList.ItemsSource = hotels.Skip(0).Take(pagination.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
        }

    }
}
