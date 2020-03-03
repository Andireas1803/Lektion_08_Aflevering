using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace Lektion_08_Aflevering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class Person : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
            get { return _id; }
        }
        private string _name;
        public string Name
        {
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
            get { return _name; }
        }
        private int _age;
        public int Age
        {
            set
            {
                _age = value;
                NotifyPropertyChanged(nameof(Age));
            }
            get { return _age; }
        }
        private int _score;
        public int Score
        {
            set
            {
                _score = value;
                NotifyPropertyChanged(nameof(Score));
            }
            get { return _score; } }

        public Person(string S)
        {
            var sp = S.Split(';');
            this.Name = sp[1];
            int a = -1;
            int i = -1;
            int s = -1;
            if (int.TryParse(sp[0], out i))
            {
                this.Id = i;
            }
            if (int.TryParse(sp[2], out a))
            {
                this.Age = a;
            }
            if (int.TryParse(sp[3], out s))
            {
                this.Score = s;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }

        public static List<Person> ReadCSVFile(string filename)
        {
            List<Person> l = new List<Person>();
            Console.WriteLine(filename);
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    l.Add(new Person(line));
                }
            }
            return l;
        }
    }
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> people;
        public MainWindow()
        {
            InitializeComponent();
            people = new ObservableCollection<Person>();
            users.ItemsSource = people;
            outerGrid.DataContext = people;
        }

        private void Menu_OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSF files(*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var l = Person.ReadCSVFile(openFileDialog.FileName);
                foreach (var item in l)
                {
                    people.Add(item);
                }
                listItemsNo.Content = "List Items: " + people.Count;
                listItemsLoaded.Content = "Last Loaded: " + DateTime.Now.ToString();
            }
        }

        private void UserChanged(object sender, TextChangedEventArgs e)
        {
            users.Items.Refresh();
        }
    }
}
