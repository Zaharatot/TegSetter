using System;
using System.Collections.Generic;
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

namespace TegSetter.Content.Controls
{
    /// <summary>
    /// Логика взаимодействия для ViewImageControl.xaml
    /// </summary>
    public partial class ViewImageControl : UserControl
    {
        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public ViewImageControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загружаем картинку по строке пути
        /// </summary>
        /// <param name="path">Путь к файлу картинки на диске</param>
        /// <returns>Класс картинки</returns>
        private BitmapImage LoadImageByPath(string path)
        {
            BitmapImage ex = new BitmapImage();
            ex.BeginInit();
            //Считываем байты файла в поток в памяти
            ex.StreamSource = new MemoryStream(File.ReadAllBytes(path));
            ex.EndInit();
            return ex;
        }

        /// <summary>
        /// Удаляем старую картинку из контролла
        /// </summary>
        private void ClearOldImage()
        {
            //Если у нас есть картинка, и она имеет
            if ((LoadedImage.Source != null) && (LoadedImage.Source is BitmapImage))
            {
                //Получаем картинку-исходник
                BitmapImage source = LoadedImage.Source as BitmapImage;
                //Уничтожаем поток с картинкой
                source.StreamSource.Dispose();
            }
        }


        /// <summary>
        /// Метод загрузки изображения
        /// </summary>
        /// <param name="imagePath">Путь к файлу изображения</param>
        public void LoadImage(string imagePath)
        {
            //Удаляем старую картинку
            ClearOldImage();
            //Загружаем картинку в памяти и проставляем её в контролл
            LoadedImage.Source = LoadImageByPath(imagePath);
        }
    }
}
