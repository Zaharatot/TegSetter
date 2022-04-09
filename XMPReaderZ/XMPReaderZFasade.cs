using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPReaderZ.Clases.DataClases;
using XMPReaderZ.Clases.WorkClases;

namespace XMPReaderZ
{
    /// <summary>
    /// Фасадный класс библиотеки
    /// </summary>
    public class XMPReaderZFasade
    {
        /// <summary>
        /// Класс чтения/записи xmp
        /// </summary>
        private XmpReader _xmpReader;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XMPReaderZFasade()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем используемые классы
            _xmpReader = new XmpReader();
        }



        /// <summary>
        /// Метод считывания тегов для файла
        /// </summary>
        /// <param name="filePath">Путь к файлу изображения</param>
        /// <returns>Список тегов файла изображения</returns>
        public List<string> ReadTags(string filePath) =>
            _xmpReader.ReadTags(filePath);

        /// <summary>
        /// Метод записи тегов для файла
        /// </summary>
        /// <param name="filePath">Путь к файлу изображения</param>
        /// <param name="tags">Список тегов файла изображения</param>
        public void WriteTags(string filePath, List<string> tags) =>
            _xmpReader.WriteTags(filePath, tags);
    }
}
