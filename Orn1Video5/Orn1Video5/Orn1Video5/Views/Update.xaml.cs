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
	public partial class Update : ContentPage
	{
        private int studentID = 0;
		public Update (StudentModel model)
		{
			InitializeComponent ();

            TxtName.Text = model.Name;
            TxtSurname.Text = model.Surname;
            dtpckrBirthDate.Date = model.BirthDate;
            TxtAbout.Text = model.About;

            //bindig ettimya sayfanın ıcerısıne studenıdyı sımdı student ıdyı ısteyen bu degerı bındıngı vasıtasıyla alacak
            BindingContext = model.StudentID;

        }

        private async void OnUpdate(object sender, EventArgs e) {
            Button mybutton = (Button)sender;
            var UpdatedStudent = (StudentModel)mybutton.CommandParameter;
            StudentModel model = new StudentModel
            {
               Name = TxtName.Text,
               Surname = TxtSurname.Text,
               About  = TxtAbout.Text,
               BirthDate= dtpckrBirthDate.Date,
               StudentID=UpdatedStudent.StudentID


            };

            ServiceManeger maneger = new ServiceManeger();
            MobileResult result= await maneger.Update(model);
            if (result.Result)
            {
              await  DisplayAlert("Succes", result.Message, "ok", "Cancel");

                await Navigation.PopAsync();
            }

        }

    }
}