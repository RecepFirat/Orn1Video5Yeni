using Orn1Video5.Models;
using Orn1Video5.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Orn1Video5.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListPage : ContentPage
	{
        readonly ServiceManeger maneger = new ServiceManeger();
        readonly IList<StudentModel> Model = new ObservableCollection<StudentModel>();

		public ListPage ()
		{
           //bıkere burda binding doldurdum public te burada ısleme sokuyorum
            BindingContext = Model;
			InitializeComponent ();
            LoadData();
        }

        private async  void LoadData()
        {
            this.IsBusy = true;
            try
            {
                /*Bu Page isim vermeden once */
                var collection = await maneger.GetAll();
                //lstStudents.BindingContext = collection;
                Model.Clear();
                foreach (StudentModel item in collection)
                {
                    Model.Add(item);
                }
            }
            catch
            {
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private void onNew(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Insert());
        }

        private void onRefresh(object sender, EventArgs e)
        {
            //bak asekron bi fonk normal olarakta atamasını yapabılıyorsun
            LoadData();
        }


        private void onSelected (object sender,SelectedItemChangedEventArgs e)
        {
            ListView lstview = (ListView)sender;
            var selectedStudent = (StudentModel)e.SelectedItem;
            Navigation.PushAsync(new DetailPage(selectedStudent));
            lstview.SelectedItem = null;
        }

        private async void onDeleted(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            //binding kkısmına bu ıstenılenı koyduktan sonra gelıyo parametre
            var selectedStudent = (StudentModel)menuItem.CommandParameter;


            /*Bak sistem diyoki bu islemi bekle calıssın ondan sonra dıgerıne gecıs olsun */
            var isok = await DisplayAlert("", "Are you sure", "Yes", "No");
            if (isok)
            {
                //ama async  bı fonka bı async bı fon cagrıcaksan ozaman await kullanmak zorundasın
                await maneger.Delete(selectedStudent);
                Model.Remove(selectedStudent);
            }
        }
    }
}