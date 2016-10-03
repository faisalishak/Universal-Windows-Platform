using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Toolkit_Controls.Commons;

namespace Toolkit_Controls.Data
{
    class VMPhoto : ViewModelBase
    {
        public ObservableCollection<MPhoto> _photos;
        private ObservableCollection<IEnumerable<MPhoto>> _groupedPhotos;

        private static async Task<IEnumerable<MPhoto>> GetPhotos(bool online)
        {
            var prefix = online ? "Online" : string.Empty;
            var uri = new Uri($"ms-appx:///Assets/Photos/{prefix}Photos.json");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            IRandomAccessStreamWithContentType randomStream = await file.OpenReadAsync();

            using (StreamReader r = new StreamReader(randomStream.AsStreamForRead()))
            {
                return Parse(await r.ReadToEndAsync());
            }
        }

        private static IEnumerable<MPhoto> Parse(string jsonData)
        {
            return JsonConvert.DeserializeObject<IList<MPhoto>>(jsonData);
        }
    }
}
