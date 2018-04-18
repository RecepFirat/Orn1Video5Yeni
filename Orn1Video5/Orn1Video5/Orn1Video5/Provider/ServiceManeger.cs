using Java.Net;
using Newtonsoft.Json;
using Orn1Video5.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Orn1Video5.Provider
{
   public  class ServiceManeger
    {
        private string Url = "http://192.168.43.128/ServiceHub/api/aakademi/";
        //service katmanım sureklı async olmalı
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;


        }


        //simdi bu student modelı gondericek bıze
        public async Task<IEnumerable<StudentModel>> GetAll()
        {
            HttpClient client = await GetClient();
            var Result = await client.GetStringAsync(Url + "getall");
            var mobileResult = JsonConvert.DeserializeObject<MobileResult>(Result);
            return JsonConvert.DeserializeObject<IEnumerable<StudentModel>>
                (mobileResult.Data.ToString());
        }

        public  async Task<MobileResult> Insert(StudentModel Model)
        {
            #region Ozel OLarak yapmak ıstersen ve api tarafında httput faalan dıye ozellesmemıs get ve sadece put kullanıyosan kısaltmaları kullanabılırsın
            ///*  cliemnt olarak baglanılabılsın dıye koydum bunu*/
            //HttpClient client = await GetClient();

            ///*Gonderecegim nası gonderecegim hepsi burada*/
            //var response = await client.PostAsync(Url + "insert", new StringContent(JsonConvert.SerializeObject(Model),
            //    Encoding.UTF8, "application/json"
            //    ));

            ///*donecek mesajı alıp donduruyo<*/
            //var mobileResult =await response.Content.ReadAsStringAsync();
            ///*Insert ıslemının cevabını alıyoruz*/
            //var result = JsonConvert.DeserializeObject<MobileResult>(mobileResult);
            //return result;
            #endregion

            return await Process(Model, "insert");

        }

        private async Task<MobileResult> Process(StudentModel model, string processtype) {
            /*  cliemnt olarak baglanılabılsın dıye koydum bunu*/
            HttpClient client = await GetClient();

            /*Gonderecegim nası gonderecegim hepsi burada*/
            var response = await client.PostAsync(Url + processtype, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json"
                ));

            /*donecek mesajı alıp donduruyo<*/
            var mobileResult = await response.Content.ReadAsStringAsync();
            /*Insert ıslemının cevabını alıyoruz*/
            var result = JsonConvert.DeserializeObject<MobileResult>(mobileResult);
            return result;

        }


        public async Task<MobileResult> Delete(StudentModel model)
        {

            return await Process(model, "delete");//su isimler route belırleyen strıng 
        }


        public async Task<MobileResult> Update(StudentModel model)
        {

            return await Process(model, "update");


        }
    }
}
