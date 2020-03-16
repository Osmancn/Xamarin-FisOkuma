using FisOkuma.Models;
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
        List<string> imagesPath =new List<string>();
        List<ImageButton> images =new List<ImageButton>();
        ImageButton selectedImage;
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
            btnSil.IsEnabled = false;
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                CompressionQuality=50,
                PhotoSize=PhotoSize.Small
            });

            if (file == null)
                return;
            
            imagesPath.Add(file.Path);
            
            //await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            ImageButton newImage = new ImageButton() { Source=image.Source};
            newImage.Clicked += ImageButton_Clicked;
            images.Add(newImage);
            lytImages.Children.Add(newImage);
            selectedImage = newImage;
            btnDevam.IsEnabled = true;
            btnSil.IsEnabled = true;
        }

        private async void BtnDevam_Clicked(object sender, EventArgs e)
        {
            List<ImageForApiModel> imageForApiList = new List<ImageForApiModel>();
            foreach (var item in imagesPath)
            {
                imageForApiList.Add(new ImageForApiModel()
                {
                    imageArray = ImageToBinary(item),
                    fileName = Path.GetFileName(item)
                });

            }
            await Navigation.PushAsync(new FisOzet(imageForApiList));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            selectedImage = (ImageButton)sender;
            image.Source = selectedImage.Source;
        }

        private void BtnSil_Clicked(object sender, EventArgs e)
        {
            imagesPath.RemoveAt(images.IndexOf(selectedImage));
            images.Remove(selectedImage);
            lytImages.Children.Remove(selectedImage);
            if(lytImages.Children.Count==0)
            {
                selectedImage = null;
                image.Source = null;
                btnSil.IsEnabled = false;
                btnDevam.IsEnabled = false;
            }
            else
            {
                selectedImage = images.Last();
                image.Source = selectedImage.Source;
            }
        }
    }
}
