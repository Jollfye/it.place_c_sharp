using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter text file name:");
            string textFileName = Console.ReadLine();
            _Text text = new _Text(textFileName);

            Console.WriteLine("Please, enter dictionary file name:");
            string dictFileName = Console.ReadLine();
            _Dictionary dict = new _Dictionary(dictFileName);

            _Html html = new _Html("index.html");
            html.Fill(text, dict);

            Console.ReadLine();
        }
    }

    /// <summary>
    /// класс файл, содержит соответствующее свойство, определяющее имя файла
    /// </summary>
    public class _File
    {
        private string file;

        public string File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }
    }

    /// <summary>
    /// класс текст, определяет файл текст
    /// </summary>
    public class _Text : _File
    {
        public _Text(string file)
        {
            File = file;
        }
    }

    /// <summary>
    /// класс словарь, определяет файл словарь и содержит свойство, включающее данные словаря
    /// </summary>
    public class _Dictionary : _File
    {

        private Dictionary<string, string> keyValue;

        public _Dictionary(string file)
        {
            File = file;
        }

        public Dictionary<string, string> KeyValue
        {
            get
            {
                try
                {
                    keyValue = System.IO.File.ReadAllLines(File).ToDictionary(word => String.Format("<i><b>{0}</b></i>", word));
                }
                catch (Exception e)
                {
                    throw e;
                }
                return keyValue;
            }
        }
    }

    /// <summary>
    /// класс веб страница, определяет имя файла веб страницы и содержит метод ее заполнения согласно требованиям
    /// </summary>
    public class _Html : _File
    {
        public _Html(string file)
        {
            File = file;
        }

        public void Fill(_Text Text, _Dictionary Dictionary)
        {
            try
            {
                using (var html = new System.IO.StreamWriter(File))
                {
                    using (var text = new System.IO.StreamReader(Text.File))
                    {
                        string line;
                        html.WriteLine("<html>");
                        html.WriteLine("<head><meta charset=\"UTF-8\"></head>");
                        html.WriteLine("<body>");
                        while ((line = text.ReadLine()) != null)
                        {
                            foreach (var word in Dictionary.KeyValue)
                                line = line.Replace(word.Value, word.Key);
                            html.WriteLine(line + "</br>");
                        }
                        html.WriteLine("</body>");
                        html.WriteLine("</html>");

                        Console.WriteLine("index.html created!");
                    }
                }
            }
            //вывод возможных исключений с соответствующими пояснениями
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
