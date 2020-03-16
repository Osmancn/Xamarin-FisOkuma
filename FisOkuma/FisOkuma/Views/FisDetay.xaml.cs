using FisOkuma.Common;
using FisOkuma.Models;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FisOkuma.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FisDetay : ContentPage
    {
        ReceiptStr receipt;
        public FisDetay(ReceiptStr fis)
        {
            receipt = fis;

            InitializeComponent();
            if(receipt==null)
            {
                DisplayAlert("File Location", "Fiş Yüklenemedi", "OK");
                return;
            }
            lblCompany.Text = receipt.Company;
            lblAdress.Text = receipt.Adress;
            lblTarih.Text = receipt.Date;
            lblSaat.Text = receipt.Time;
            lblFisNo.Text = receipt.DocumentNumber;
            for (int i = 0; i < receipt.RecipeItems.Count; i++)
            {
                RecipeItemStr item = receipt.RecipeItems[i];

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
            lblToplamKdv.Text = receipt.TaxTotal;
            lblToplamTutar.Text = receipt.GrandTotal;
            lblTaxNumber.Text = receipt.TaxNumber;

        }

        private void btnGeri_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}