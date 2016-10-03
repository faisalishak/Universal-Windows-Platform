using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static bool _isOnlineCached;

        public async Task<ObservableCollection<MPhotoDataItem>> GetItemsAsync(bool online = false, int maxCount = -1)
        {
            CheckCacheState(online);

            if (_photos == null)
            {
                await LoadAsync(online, maxCount);
            }

            return _photos;
        }

        public async Task<ObservableCollection<IEnumerable<MPhotoDataItem>>> GetGroupedItemsAsync(bool online = false, int maxCount = -1)
        {
            CheckCacheState(online);

            if (_groupedPhotos == null)
            {
                await LoadAsync(online, maxCount);
            }

            return _groupedPhotos;
        }

        private static async Task LoadAsync(bool online, int maxCount)
        {
            _isOnlineCached = online;
            _photos = new ObservableCollection<MPhotoDataItem>();
            _groupedPhotos = new ObservableCollection<IEnumerable<MPhotoDataItem>>();

            foreach (var item in await GetPhotosAsync(online))
            {
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

        private static async Task<IEnumerable<MPhotoDataItem>> GetPhotosAsync(bool online)
        {
            var prefix = online ? "Online" : string.Empty;
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

        private static void CheckCacheState(bool online)
        {
            if (_isOnlineCached != online)
            {
                _photos = null;
                _groupedPhotos = null;
            }
        }
    }
}
