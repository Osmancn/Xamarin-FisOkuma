using FisOkuma.Common;
using FisOkuma.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FisOkuma.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FisOzet : ContentPage
    {
        List<ImageForApiModel> images;
        List<RecipeItemStr> urunList=new List<RecipeItemStr>();
        List<ImageButton> listImageButton = new List<ImageButton>();
        List<ReceiptStr> fisler = new List<ReceiptStr>();
        public decimal totalAmount { get; set; }
        public decimal totalTax { get; set; }

        public FisOzet(List<ImageForApiModel> images)
        {
            totalAmount = 0;
            totalTax = 0;
            this.images = images;
            InitializeComponent();
            getFisler();
            getImages();
            for (int i = 0; i < urunList.Count; i++)
            {
                RecipeItemStr item = urunList[i];

                Label lblLineDescription = new Label();
                lblLineDescription.Text = item.LineDescription;
                grdRecipeItems.Children.Add(lblLineDescription, 0, i);

                Label lblLineTax = new Label();
                lblLineTax.Text = item.LineTax;
                grdRecipeItems.Children.Add(lblLineTax, 1, i);

                Label lblLineTotal = new Label();
                lblLineTotal.Text = item.LineTotal;
                lblLineTotal.HorizontalOptions = LayoutOptions.End;
                grdRecipeItems.Children.Add(lblLineTotal, 2, i);
            }

            lblToplamTutar.Text = totalAmount.ToString();
            lblToplamKdv.Text = totalTax.ToString();
        }
        void getFisler()
        {
            urunList.Clear();
            foreach (ImageForApiModel item in images)
            {
                ReceiptStr fis = RemoteApi.GetFisFromApi(item.imageArray, item.fileName);
                fisler.Add(fis);
                totalAmount += Convert.ToDecimal(fis.GrandTotal);
                totalTax += Convert.ToDecimal(fis.TaxTotal);
                urunList.AddRange(fis.RecipeItems);
            }
        }

        void getImages()
        {
            foreach (var item in images)
            {
                ImageButton image = new ImageButton();
                image.Clicked += ImageButton_ClickedAsync;
                image.Source = ImageSource.FromStream(() => new MemoryStream(item.imageArray));
                listImageButton.Add(image);
                lytImages.Children.Add(image);
            }
            
        }

        private async void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new FisDetay(fisler[listImageButton.IndexOf((ImageButton)sender)]));
            
        }

        private void BtnGeri_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}