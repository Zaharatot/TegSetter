using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.DataClases.Info.Tag;

namespace TegSetter.Content.Clases.WorkClases.Tags
{
    /// <summary>
    /// Класс работы с тегами
    /// </summary>
    internal class TagsWork
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TagsWork()
        {

        }


        /// <summary>
        /// Получаем только новые теги
        /// </summary>
        /// <param name="currentCollection">Текущая коллекция тегов</param>
        /// <param name="scannedList">Считанный из файлов список тегов</param>
        /// <returns>Список новых считанных тегов</returns>
        public List<string> GetNewTags(TagsCollection currentCollection, List<string> scannedList)
        {
            //Получаем список имён тегов из списка классов тегов
            List<string> tags = currentCollection.GetTagNames();
            //Получаем теги, которые есть в считанном списке, но которых нет в общем
            return scannedList.Except(tags).ToList();
        }

        /// <summary>
        /// Получаем список тегов из изображений
        /// </summary>
        /// <param name="images">Список классов информации об изображении</param>
        /// <returns>Список уникальных тегов из изображений</returns>
        public List<string> GetImagesTags(List<ImageInfo> images)
        {
            //Инициализируем выходной список
            List<string> ex = new List<string>();
            //Добавляем теги из всех изображений в список
            images.ForEach(image => ex.AddRange(image.Tags));
            //Возвращаем только уникальные теги из списка
            return ex.Distinct().ToList();
        }
            

    }
}
