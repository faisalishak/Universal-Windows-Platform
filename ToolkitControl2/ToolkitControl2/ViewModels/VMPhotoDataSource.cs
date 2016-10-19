using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitControl2.Models;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;

namespace ToolkitControl2.ViewModels
{
    [Bindable]
    class VMPhotoDataSource
    {
        private static ObservableCollection<MPhotoDataItem> _photos;
        private static ObservableCollection<IEnumerable<MPhotoDataItem>> _groupedPhotos;        

        public async Task<ObservableCollection<MPhotoDataItem>> GetItemsAsync(int maxCount= -1)
        {
            await LoadAsync(maxCount);
            return _photos;
        }

        private static async Task LoadAsync(int maxCount)
        {
            _photos = new ObservableCollection<MPhotoDataItem>();
            _groupedPhotos = new ObservableCollection<IEnumerable<MPhotoDataItem>>();

            foreach (MPhotoDataItem item in await GetPhotosAsync())
            {
                item.Thumbnail = "ms-appx://" + item.Thumbnail;
                _photos.Add(item);
                if (maxCount != -1)
                {
                    maxCount--;

                    if (maxCount == 0)
                    {
                        break;
                    }
                }
            }

            foreach (var group in _photos.GroupBy(x => x.Category))
            {
                _groupedPhotos.Add(group);
            }
        }

        private static async Task<IEnumerable<MPhotoDataItem>> GetPhotosAsync()
        {
            var uri = new Uri("ms-appx:///Assets/Photos/Photos.json");   
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            IRandomAccessStreamWithContentType randomStream = await file.OpenReadAsync();

            using (StreamReader r = new StreamReader(randomStream.AsStreamForRead()))
            {
                return Parse(await r.ReadToEndAsync());
            }
        }

        private static IEnumerable<MPhotoDataItem> Parse(string jsonData)
        {
             return JsonConvert.DeserializeObject<IList<MPhotoDataItem>>(jsonData);
        }
    }
}
