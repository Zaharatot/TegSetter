using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.WorkClases.Loaders;
using XMPReaderZ;

namespace TegSetter.Content.Clases.WorkClases.Loaders
{
    /// <summary>
    /// Класс загрузки изображений из папки
    /// </summary>
    internal class ImageLoader
    {
        /// <summary>
        /// Класс считывания XMP
        /// </summary>
        private XMPReaderZFasade _xmpReader;

        /// <summary>
        /// Список поддерживаемых расширений
        /// </summary>
        private List<string> _allowedExtensions;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ImageLoader()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем используемые классы
            _xmpReader = new XMPReaderZFasade();
            //Получаем список поддерживаемых расширений
            _allowedExtensions = GetExtensions();
        }

        /// <summary>
        /// Метод получения списка поддерживаемых расширений
        /// </summary>
        /// <returns>Список расширений</returns>
        private List<string> GetExtensions() =>
            new List<string>() { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        /// <summary>
        /// ЗАгружаем информацию об изображении
        /// </summary>
        /// <param name="file">Класс информации о файле</param>
        /// <returns>Класс информации об изображении</returns>
        private ImageInfo LoadImageInfo(FileInfo file) =>
            new ImageInfo() { 
                Name = file.Name,
                Path = file.FullName,
                Tags = _xmpReader.ReadTags(file.FullName)
            };

        /// <summary>
        /// Проверка наличия расширения файла в списке дозволенных
        /// </summary>
        /// <param name="extension">Строка расширения файла</param>
        /// <returns>True - расширение можно использовать</returns>
        private bool IsAllowExtension(string extension) =>
           //Переводим расширение в нижний регистр, и ищем его в списк едозволенных
           _allowedExtensions.Contains(extension.ToLower());




        /// <summary>
        /// Выполняем загрузку изображений из папки
        /// </summary>
        /// <param name="path">Путь к папке с файлами</param>
        /// <returns>Список загруженных изображений</returns>
        public List<ImageInfo> LoadImages(string path)
        {
            //Инициализируем выходной список
            List<ImageInfo> ex = new List<ImageInfo>();
            //Если путь вообще указан
            if (!string.IsNullOrEmpty(path))
            {
                //Инициализируем класс информации о папке
                DirectoryInfo directory = new DirectoryInfo(path);
                //Если папка существует
                if (directory.Exists)
                    //Получаем из него классы информации о дочерних файлах
                    ex = directory.GetFiles()
                    //Выбираем из них только те, что имеют корректное расширение
                    .Where(file => IsAllowExtension(file.Extension))
                    //Приводим выбранные элементы к списку
                    .ToList()
                    //Конвертируем элементы в информацию об изображениях
                    .ConvertAll(file => LoadImageInfo(file));
            }
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Сохраняем теги для изображений
        /// </summary>
        /// <param name="images">Список изображений для сохранения</param>
        public void SaveImagesTags(List<ImageInfo> images) =>
            //Проходимся по списку изображений, и для каждого из них сохраняем новые теги
            images.ForEach(image => _xmpReader.WriteTags(image.Path, image.Tags));
        
    }
}
