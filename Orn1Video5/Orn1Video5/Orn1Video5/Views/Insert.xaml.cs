using Orn1Video5.Models;
using Orn1Video5.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Orn1Video5.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Insert : ContentPage
	{
		public Insert ()
		{
			InitializeComponent ();
		}

        private async void OnSave(object sender,EventArgs e)
        {
            StudentModel model = new StudentModel
            {
                Name = TxtName.Text,
                Surname = TxtSurname.Text,
                About = TxtAbout.Text,
                BirthDate = dtpckrBirthDate.Date


            };
            ServiceManeger maneger = new ServiceManeger();

            MobileResult result = await maneger.Insert(model);
            /*bana burada mobileresulttan deger dondugu ıcın bak apide hep dondurdugum sey bı mobıle
             result var ona gore ıslemın dogrulugu hakkında bılgı sahıgı oluyorum
             */

            if (result.Result)
            {/* cevap bekliyo cevaba gore ıslem yapıyo awaıt koydum ya ondan*/
               await DisplayAlert("Succes", result.Message, "Ok", "Cancel");
              await   Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", result.Message, "Ok", "Cancel");
            }
        }
	}
}