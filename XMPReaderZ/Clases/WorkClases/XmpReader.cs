using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPReaderZ.Clases.DataClases;

namespace XMPReaderZ.Clases.WorkClases
{
    /// <summary>
    /// Класс выполнения считывания/записи xmp-файлов
    /// </summary>
    internal class XmpReader
    {
        /// <summary>
        /// Константа дефолтного заголовка XML-документа
        /// </summary>
        private const string DEFAULT_XML_HEADER = "<?xml version=\"1.0\"?>";
        /// <summary>
        /// Константа заголовка XMP-документа
        /// </summary>
        private const string XMP_HEADER = "<?xpacket begin=\"\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>";
        /// <summary>
        /// Константа окончания XMP-документа
        /// </summary>
        private const string XMP_FOOTER = "<?xpacket end=\"w\"?>";


        /// <summary>
        /// Кдасс сериализации xml
        /// </summary>
        private XmlSerializer _serializer;
        /// <summary>
        /// Класс пространств имён xmp-файла
        /// </summary>
        XmlSerializerNamespaces _namespaces;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmpReader()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем используемые классы
            _serializer = new XmlSerializer(typeof(XmpDocument));
            _namespaces = new XmlSerializerNamespaces();
            //Добавляем пространства имён
            _namespaces.Add("x", "adobe:ns:meta/");
        }

        /// <summary>
        /// Получаем путь к XMP-файлу
        /// </summary>
        /// <param name="filePath">Путь к файлу изображения</param>
        /// <returns>Строка пути к XMP-файлу</returns>
        private string GetXmpPath(string filePath)
        {
            //Инициализируем класс информации о файле
            FileInfo file = new FileInfo(filePath);
            //Формируем строку из пути к родительской папки м имени файла с заменённым расширением
            return $"{file.Directory.FullName}\\{file.Name.Replace(file.Extension, ".xmp")}";
        }

        /// <summary>
        /// Форматируем XMP-файл в xml
        /// </summary>
        /// <param name="xml">Исходный XML-файл</param>
        /// <returns>Строка XMP-файла</returns>
        private string FormatXmlToXmp(string xml) =>
            //Заменяем заголовок xml-файла, и доабвляем футер в конец файла
            $"{xml.Replace(DEFAULT_XML_HEADER, XMP_HEADER)}{XMP_FOOTER}";


        /// <summary>
        /// Метод считывания тегов для файла
        /// </summary>
        /// <param name="filePath">Путь к файлу изображения</param>
        /// <returns>Список тегов файла изображения</returns>
        public List<string> ReadTags(string filePath)
        {
            //Инициализируем пустой список
            List<string> ex = new List<string>();
            //Получаем путь к XMP-файлу
            string xmpPath = GetXmpPath(filePath);
            //Если xmp-файл существует
            if(File.Exists(xmpPath))
            {
                //Считываем все байты XMP-файла
                byte[] bytes = File.ReadAllBytes(xmpPath);
                //Инициализируем поток в памяти из байт файла
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    //Десериализуем XMP-файл в класс данных
                    XmpDocument xmp = (XmpDocument)_serializer.Deserialize(ms);
                    //Добавляем в выходной массив считанные из XMP-файла теги
                    ex.AddRange(xmp.GetTags());
                }
            }
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Метод записи тегов для файла
        /// </summary>
        /// <param name="filePath">Путь к файлу изображения</param>
        /// <param name="tags">Список тегов файла изображения</param>
        public void WriteTags(string filePath, List<string> tags)
        {
            string xmpDoc;
            //Получаем путь к XMP-файлу
            string xmpPath = GetXmpPath(filePath);
            //Инициализируем XMP-документ из списка тегов
            XmpDocument xmp = new XmpDocument(tags.ToArray());
            //Инициаализируем поток в памяти
            using (MemoryStream ms = new MemoryStream())
            {
                //Сериализуем класс в xmp-документ
                _serializer.Serialize(ms, xmp, _namespaces);
                //Получаем xmp-документ в виде строки
                xmpDoc = Encoding.UTF8.GetString(ms.ToArray());
            }
            //Форматируем xmp-документ, и сохраняем его в файл
            File.WriteAllText(xmpPath, FormatXmlToXmp(xmpDoc));
        }
    }
}
