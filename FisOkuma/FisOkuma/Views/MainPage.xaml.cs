using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FisOkuma.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string imagePath;
        public MainPage()
        {
            InitializeComponent();
            btnDevam.IsEnabled = false;
        }

        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream fS = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
        }
        private async void BtnKamera_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            image.Source = null;
            loadingGif.IsRunning = true;
            btnDevam.IsEnabled = false;
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize=PhotoSize.Small
            });

            if (file == null)
                return;
            
            imagePath = file.Path;
            
            //await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            btnDevam.IsEnabled = true;
        }

        private async void BtnDevam_Clicked(object sender, EventArgs e)
        {
            byte[] imgArray = ImageToBinary(imagePath);
            await Navigation.PushAsync(new FisDetay(imgArray, Path.GetFileName(imagePath)));
        }
    }
}
