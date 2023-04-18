using RegisterTaskWPF.Command;
using RegisterTaskWPF.Helpers;
using RegisterTaskWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RegisterTaskWPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        interface IAdapter
        {
            void Write(Person person);
            void Read(string filename);
        }

        class JsonAdapter : IAdapter
        {
            private Json _json;

            public JsonAdapter(Json json)
            {
                _json = json;
            }

            public void Write(Person person)
            {
                _json.Write(person);
            }

            public void Read(string fileName)
            {
                _json.Read(fileName);
            }
        }

        class XmlAdapter : IAdapter
        {
            private XML _xml;

            public XmlAdapter(XML xml)
            {
                _xml = xml;
            }

            public void Write(Person person)
            {
                _xml.Write(person);
            }

            public void Read(string fileName)
            {
                _xml.Read(fileName);
            }
        }

        class Json
        {
            public Person Read(string fileName)
            {
                return FileHelper.ReadPersonJson(fileName);
            }

            public void Write(Person person)
            {
                FileHelper.WritePersonJson(person);
            }
        }

        class XML
        {
            public Person Read(string fileName)
            {
                return FileHelper.ReadPersonXml(fileName);
            }

            public void Write(Person person)
            {
                FileHelper.WritePersonXml(person);
            }
        }

        class Converter
        {
            private readonly IAdapter _adapter;

            public Converter(IAdapter adapter)
            {
                _adapter = adapter;
            }

            public void Write(Person person)
            {
                _adapter.Write(person);
            }

            public void Read(string fileName)
            {
                _adapter.Read(fileName);
            }
        }

        class Application
        {
            private readonly Converter _converter;

            public Application(IAdapter adapter)
            {
                _converter = new Converter(adapter);
            }

            public void Write(Person person)
            {
                _converter.Write(person);
            }

            public void Read(string fileName)
            {
                _converter.Read(fileName);
            }
        }

        private string name;
        private string surname;
        private int age;
        private string city;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged(); }
        }

        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); }
        }

        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged(); }
        }

        private bool jsonSave;
        private bool xmlSave;
        private bool jsonLoad;
        private bool xmlLoad;

        public bool JsonSaveChecked
        {
            get { return jsonSave; }
            set { jsonSave = value; OnPropertyChanged(); }
        }

        public bool XmlSaveChecked
        {
            get { return xmlSave; }
            set { xmlSave = value; OnPropertyChanged(); }
        }

        public bool JsonLoadChecked
        {
            get { return jsonLoad; }
            set { jsonLoad = value; OnPropertyChanged(); }
        }

        public bool XmlLoadChecked
        {
            get { return xmlLoad; }
            set { xmlLoad = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }

        public MainViewModel()
        {
            SaveCommand = new RelayCommand((obj) =>
            {
                if (Name == String.Empty)
                {
                    MessageBox.Show("Username section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    if (Surname == String.Empty)
                    {
                        MessageBox.Show("Surname section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else
                    {
                        if (Age == 0)
                        {
                            MessageBox.Show("Include a correct age.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        else
                        {
                            if (City == String.Empty)
                            {
                                MessageBox.Show("City section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            else
                            {
                                IAdapter adapter;
                                Person person = new Person
                                {
                                    Name = this.Name,
                                    Surname = this.Surname,
                                    Age = this.Age,
                                    City = this.City
                                };

                                if (JsonSaveChecked)
                                {
                                    Json json = new Json();
                                    adapter = new JsonAdapter(json);
                                    Application app = new Application(adapter);
                                    adapter.Write(person);
                                    MessageBox.Show("Person has been successfully saved!", "Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }

                                else if (XmlSaveChecked)
                                {
                                    XML xml = new XML();
                                    adapter = new XmlAdapter(xml);
                                    Application app = new Application(adapter);
                                    adapter.Write(person);
                                    MessageBox.Show("Person has been successfully saved!", "Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }

                                else
                                {
                                    MessageBox.Show("Please choose a save kind.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                }
            });

            LoadCommand = new RelayCommand((obj) =>
            {

            });
        }
    }
}