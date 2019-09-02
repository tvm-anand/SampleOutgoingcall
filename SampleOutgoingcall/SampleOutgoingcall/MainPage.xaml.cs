using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampleOutgoingcall
{
    public partial class MainPage : ContentPage
    {

        string CallStartTime;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Call_Clicked(object sender, EventArgs e)
        {
            PhoneDialer.Open(phonenumber.Text);


            //MessagingCenter.Subscribe<Object>(this, "CallConnectedAndroid", (sender) => {
               
            //    // I will store the current time here as call start time

            //});

            //MessagingCenter.Subscribe<Object>(this, "CallEnded", async (sender) => {

            //    // I will store the current time here as call end time

            //    // Call duration= call start time- call end time;
            //});

        }
    }
}
