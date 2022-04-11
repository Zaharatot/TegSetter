using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TegSetter.Content.Clases.DataClases.Info;

namespace TegSetter.Content.Clases.WorkClases.Loaders
{
    /// <summary>
    /// Класс загрузки и сохранения тегов
    /// </summary>
    internal class TagLoader
    {
        /// <summary>
        /// Путь к файлу хранения тегов
        /// </summary>
        private string _tagsPath;
        /// <summary>
        /// Класс сериализации в XML
        /// </summary>
        private XmlSerializer _serializer;


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TagLoader()
        {
            Init();
        }

        /// <summary>
        /// ИНициализатор класса
        /// </summary>
        private void Init()
        {
            //Проставляем дефолтные значения
            _tagsPath = CompilePath();
            _serializer = new XmlSerializer(typeof(TagsWrapper));
        }

        /// <summary>
        /// Формируем путь к файлу тегов
        /// </summary>
        /// <returns>Строка пути к файлу тегов</returns>
        private string CompilePath() =>
            $"{Environment.CurrentDirectory}\\tags.xml";


        /// <summary>
        /// Выполняем загрузку списка тегов
        /// </summary>
        /// <returns>Список тегов</returns>
        public List<TagInfo> LoadTags()
        {
            //Инициализируем класс-обёртку
            TagsWrapper ex = new TagsWrapper();
            //Если файл существует
            if (File.Exists(_tagsPath))
            {
                //Инициаализируем поток в памяти
                using (FileStream ms = File.OpenRead(_tagsPath))
                    //Десериализуем xml в объект
                    ex = (TagsWrapper)_serializer.Deserialize(ms);
            }
            //Возвращаем результат, отсортированный по имени
            return ex.Tags.OrderBy(tag => tag).ToList();
        }

        /// <summary>
        /// Выполняем сохранение списка тегов
        /// </summary>
        /// <param name="tags">Список тегов для сохранения</param>
        public void SaveTags(List<TagInfo> tags)
        {
            //Инициаализируем поток в памяти
            using (MemoryStream ms = new MemoryStream())
            {
                //Сериализуем класс в xml
                _serializer.Serialize(ms, new TagsWrapper(tags));
                //Сохраняем байты в файл
                File.WriteAllBytes(_tagsPath, ms.ToArray());
            }
        }
    }
}
