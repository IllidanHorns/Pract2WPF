using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Pract2WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime dateTime = DateTime.Now.Date;
        List<Note> Notes = new List<Note>();
        string note_name;
        string note_description;
        public MainWindow()
        {
            InitializeComponent();
            calendar.SelectedDate = dateTime;
            Notes = Save_File.Deserialization<Note>();
            list_box.ItemsSource = search_notes();
        }

        public void update_text()
        {
            text_box_name.Text = "";
            text_box_description.Text = "";
        }

        private void calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateTime = Convert.ToDateTime(calendar.SelectedDate);
            list_box.ItemsSource = search_notes();
            text_box_name.Text = "";
            text_box_description.Text = "";
        }


        private void button_create_Click(object sender, RoutedEventArgs e)
        {
            if (text_box_name.Text != "" && text_box_description.Text != "") 
            {
                Note actual_note = new Note(text_box_name.Text, text_box_description.Text, dateTime);
                Notes.Add(actual_note);
            };
            list_box.ItemsSource = search_notes();
            update_text();
            Save_File.Serialization(Notes);
        }


        public List<Note> search_notes()
        {
            var notes = Notes.Where(x => x.date.Date == dateTime.Date).ToList();
            return notes;
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            var notes = search_notes();
            int index = list_box.SelectedIndex;
            if (text_box_name.Text != "" && text_box_description.Text != "")
            {
                try
                {
                    search_notes()[index].name = text_box_name.Text;
                    search_notes()[index].description = text_box_description.Text;
                    Notes = Notes.Where(x => x.date.Date != dateTime.Date).ToList();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                    list_box.ItemsSource = search_notes();
                    update_text();
                    Save_File.Serialization(Notes);
                }
                catch (System.ArgumentOutOfRangeException)
                {

                }
            }
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            var notes = search_notes();
            int index = list_box.SelectedIndex;
            if (list_box.SelectedItem != null) 
            {
                notes.RemoveAt(index);
                Notes = Notes.Where(x => x.date.Date != dateTime.Date).ToList();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
                list_box.ItemsSource = search_notes();
                update_text();
                Save_File.Serialization(Notes);
            }
            else
            {

            }
            
        }

        private void list_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = list_box.SelectedIndex;
            try
            {
                text_box_name.Text = search_notes()[index].name;
                text_box_description.Text = search_notes()[index].description;
            }
            catch
            {

            }
        }
    }
}