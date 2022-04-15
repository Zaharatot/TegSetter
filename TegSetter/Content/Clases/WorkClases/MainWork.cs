using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.WorkClases.Keyboard;
using TegSetter.Content.Clases.WorkClases.Loaders;
using TegSetter.Content.Clases.WorkClases.Tags;
using XMPReaderZ;

namespace TegSetter.Content.Clases.WorkClases
{
    /// <summary>
    /// Основной рабочий класс
    /// </summary>
    internal class MainWork
    {
        /// <summary>
        /// Класс загрузки и сохранения изображений
        /// </summary>
        private ImageLoader _imageLoader;
        /// <summary>
        /// Класс работы с тегами
        /// </summary>
        private TagsWork _tagsWork;
        /// <summary>
        /// Класс загрузки/сохранения тегов
        /// </summary>
        private TagLoader _tagLoader;
        /// <summary>
        /// Класс назначения кнопок
        /// </summary>
        private KeyMapper _keyMapper;
        /// <summary>
        /// Класс обработки нажатий кнопок
        /// </summary>
        private KeyActionProcessor _keyActionProcessor;


        /// <summary>
        /// Список изображений для работы
        /// </summary>
        private List<ImageInfo> _images;
        /// <summary>
        /// Текущий список тегов
        /// </summary>
        private List<TagInfo> _tags;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MainWork()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Список изображений для работы
            _images = new List<ImageInfo>();
            _tags = new List<TagInfo>();
            //Инициализируем используемые классы
            _imageLoader = new ImageLoader();
            _tagsWork = new TagsWork();
            _tagLoader = new TagLoader();
            _keyMapper = new KeyMapper();
            _keyActionProcessor = new KeyActionProcessor();
            //Загружаем список тегов
            LoadTags();
        }


        /// <summary>
        /// Обрабатываем нажатие на кнопку клавиатуры
        /// </summary>
        /// <param name="e">Информация о нажатой кнопке</param>
        public void ProcessKeyPress(KeyEventArgs e) =>
            _keyActionProcessor.ProcessKeyPress(e);

        /// <summary>
        /// Формируем ключ-пары из тегов и клавишь
        /// </summary>
        /// <param name="tags">Список тегов</param>
        public void PairKeysToTags(ref List<TagInfo> tags) =>
            _keyMapper.PairKeysToTags(ref tags);

        /// <summary>
        /// Выполняем загрузку списка тегов
        /// </summary>
        public void LoadTags() =>
             _tags = _tagLoader.LoadTags();

        /// <summary>
        /// Выполняем сохранение списка тегов
        /// </summary>
        public void SaveTags() =>
            _tagLoader.SaveTags(_tags);

        /// <summary>
        /// Получаем новые теги из загруженных картинок
        /// </summary>
        /// <returns>Список новых считанных тегов</returns>
        public List<string> GetNewTags()
        {
            //Получаем теги от текущих изображений
            List<string> newTags = _tagsWork.GetImagesTags(_images);
            //Выбираем из списка только те теги, которых ещё не было доабвлено
            return _tagsWork.GetNewTags(_tags, newTags);
        }

        /// <summary>
        /// Добавляем теги в список
        /// </summary>
        /// <param name="tags">Список тегов для добавления</param>
        public void AddTags(List<TagInfo> tags)
        {
            //Прроходимся по добавляемым тегам
            tags.ForEach(newTag => {
                //Если такого тега нет в списке
                if (!_tags.Any(tag => tag.IsEquals(newTag.Name)))
                    //Добавляем его
                    _tags.Add(newTag);
            });
            //Сохраняем список тегов
            SaveTags();
        }
        
        /// <summary>
        /// Добавляем тег в список
        /// </summary>
        /// <param name="tagName">Тег для добавления</param>
        public void AddTag(string tagName)
        {
            //Если такого тега нет в списке
            if (!_tags.Any(tag => tag.IsEquals(tagName)))
                //Добавляем его
                _tags.Add(new TagInfo(tagName, ""));
            //Сохраняем список тегов
            SaveTags();
        }

        /// <summary>
        /// Удаляем тег из списка
        /// </summary>
        /// <param name="tagName">Тег для удаления</param>
        public void DeleteTag(string tagName)
        {
            //Удалеем тег из списка
            _tags.RemoveAll(tag => tag.IsEquals(tagName));
            //Сохраняем список тегов
            SaveTags();
        }

        /// <summary>
        /// Проставляем обновлённый список тегов
        /// </summary>
        /// <param name="tags">Список тегов для простановки</param>
        public void SetTags(List<TagInfo> tags)
        {
            //Проставляем список тегов
            _tags = tags;
            //Сохраняем список тегов
            SaveTags();
        }

        /// <summary>
        /// Выполняем загрузку изображений из папки
        /// </summary>
        /// <param name="info">Класс информации о загрузке</param>
        /// <returns>Список загруженных изображений</returns>
        public List<ImageInfo> LoadImages(LoadInfo info) =>
            //Выполняем загрузку изображений
            _images = _imageLoader.LoadImages(info);

        /// <summary>
        /// Сохраняем теги для изображений
        /// </summary>
        public void SaveImagesTags() =>
            //Сохраняем изображения
            _imageLoader.SaveImagesTags(_images);

        /// <summary>
        /// Получаем список тегов
        /// </summary>
        /// <returns>Полный список тегов</returns>
        public List<TagInfo> GetTags() => _tags;

        /// <summary>
        /// Получение изображения по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор изображения</param>
        /// <returns>Класс информации об изображении</returns>
        public ImageInfo LoadImage(int id)
        {
            //Если id не в рамках допустимого
            if ((id < 0) || (id >= _images.Count))
                //Возвращаем Null
                return null;
            //Если всё ок - возвращаем картинку
            return _images[id];
        }

        /// <summary>
        /// Добавляем тег картинке
        /// </summary>
        /// <param name="id">Идентификатор изображения</param>
        /// <param name="tag">Тег для добавления</param>
        public void AddImageTag(int id, TagInfo tag)
        {
            //Получаем картинку по идентификатору
            ImageInfo image = LoadImage(id);
            //Если картинка найдена и у картинки ещё нет такого тега
            if((image != null) && !image.Tags.Contains(tag.Name))
                //Добавляем тег картинке
                image.Tags.Add(tag.Name);
        }

        /// <summary>
        /// Проставляем новый словарь тегов
        /// </summary>
        /// <param name="tags">Новый словарь тегов</param>
        public void SetNewTagsDict(List<TagInfo> tags) =>
            //Вызываем внутренний метод
            _keyActionProcessor.SetNewTagsDict(tags);


        /// <summary>
        /// Получаем системные теги по именам
        /// </summary>
        /// <param name="tagNames">Список имён тегов</param>
        /// <returns>Список тегов</returns>
        public List<TagInfo> GetSystemTags(List<string> tagNames) =>
            //Список имён тегов
            tagNames
                //Конвертируем в системные, путём поиска по имени целевых тегов
                .ConvertAll(tagName => _tags.FirstOrDefault(tag => tag.IsEquals(tagName)))
                //Выбираем только не пустые элементы
                .Where(tag => tag != null)
                //Возвращаем в виде списка
                .ToList();

        /// <summary>
        /// Возврат количества изображений
        /// </summary>
        /// <returns>Количество загруженных изображений</returns>
        public int GetImagesCount() => _images.Count;

    }
}
