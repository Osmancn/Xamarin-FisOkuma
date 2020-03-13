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
        public FisDetay(byte[] imageArray, string fileName)
        {
            receipt = GetFisFromApi(imageArray, fileName);

            InitializeComponent();

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
        ReceiptStr GetFisFromApi(byte[] imageArray, string fileName)
        {
            var client = new RestClient("https://coden-dev.azurewebsites.net/api/");

            var request = new RestRequest("SaveFileToBlobStorage", Method.POST);
            request.AddQueryParameter("code", "FKfPkeYWaahi3Rb0FI8wkyepzgEIDE9GqXsPOnYsFn/mLkXADUSmww==");
            request.AddFileBytes("file", imageArray, fileName);

            IRestResponse<SaveReceiptOperationResult> response = client.Execute<SaveReceiptOperationResult>(request);

            if (!response.IsSuccessful)
                Navigation.PopAsync();
            return response.Data.Receipt;
        }

    }
}