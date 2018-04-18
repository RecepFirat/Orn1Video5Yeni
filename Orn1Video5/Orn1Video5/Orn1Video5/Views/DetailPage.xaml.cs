using Orn1Video5.Models;
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
	public partial class DetailPage : ContentPage
	{
		public DetailPage (StudentModel model)
		{
			InitializeComponent ();

            lblName.Text = model.Name;
            lblSurname.Text = model.Surname;
            lblAbout.Text = model.About;
            lblBirthDate.Text=model.BirthDate.ToString("yyyy-MM-dd");
        }
	}
}