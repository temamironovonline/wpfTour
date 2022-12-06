﻿using System;
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
using System.Windows.Shapes;

namespace wpfTour
{
    /// <summary>
    /// Логика взаимодействия для AddHotelWindow.xaml
    /// </summary>
    public partial class AddHotelWindow : Window
    {
        public AddHotelWindow()
        {
            InitializeComponent();
            countStarsComboBox.SelectedIndex = 0;
            countryComboBox.Items.Add("Не выбрано");
            countryComboBox.SelectedIndex = 0;

            List<Country> countries = DataBaseConnection.tourEntities.Country.ToList();

            foreach (Country country in countries)
            {
                countryComboBox.Items.Add(country.Name);
            }


        }

        public AddHotelWindow(Hotel hotel)
        {
            InitializeComponent();

            List<Country> countries = DataBaseConnection.tourEntities.Country.ToList();

            int index = countries.FindIndex(x => x.Code == hotel.CountryCode)+1;

            countryComboBox.Items.Add("Не выбрано");

            foreach (Country country in countries)
            {
                countryComboBox.Items.Add(country.Name);
            }

            nameHotelTextBox.Text = hotel.Name;
            countStarsComboBox.SelectedIndex = hotel.CountOfStars;
            descriptionTextBox.Text = hotel.Description;
            countryComboBox.SelectedIndex = index;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameHotelTextBox.Text != "")
            {
                if (descriptionTextBox.Text != "")
                {
                    if (countryComboBox.SelectedIndex != 0)
                    {
                        Country country = DataBaseConnection.tourEntities.Country.FirstOrDefault(x => x.Name == countryComboBox.Text);
                        Hotel hotel = new Hotel()
                        {
                            Name = nameHotelTextBox.Text,
                            CountOfStars = countStarsComboBox.SelectedIndex,
                            Description = descriptionTextBox.Text,
                            CountryCode = country.Code,

                        };
                    }
                    else
                    {
                        MessageBox.Show("Выберите страну");
                    }
                }
                else
                {
                    MessageBox.Show("Поле с описанием не может быть пустым");
                }
            }
            else
            {
                MessageBox.Show("Поле с наименованием не может быть пустым");
            }

        }
    }
}