using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowReqres.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
using static SpecFlowReqres.DataModel.SingleUserDataModel;

namespace SpecFlowReqres.Support
{
    public class ReqresHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly string environment = TestConfiguration.TestEnvironment;
        private readonly string name = TestConfiguration.Name;
        private readonly string job = TestConfiguration.Job;
        private readonly string applicationType = "application/json";

        public ReqresHttpClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> SendGetRequestAsync(string uri)
        {
            return await httpClient.GetAsync(environment + uri);
        }

        public StringContent JSONtoString(UserDataModel json)
        {
            var jsonData = JsonConvert.SerializeObject(json);
            return new StringContent(jsonData, Encoding.UTF8, applicationType);
        }

        public async Task<HttpResponseMessage> SendPostRequestAsync(string uri, UserDataModel data)
        {
            return await httpClient.PostAsync(environment + uri, JSONtoString(data));
        }

        public async Task<HttpResponseMessage> SendPutRequestAsync(string uri, UserDataModel data)
        {
            return await httpClient.PutAsync(environment + uri, JSONtoString(data));
        }

        public async Task<HttpResponseMessage> SendPatchRequestAsync(string uri, UserDataModel data)
        {
            return await httpClient.PatchAsync(environment + uri, JSONtoString(data));
        }

        public async Task AssertResponseBodyForCreateUser(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            UserDataModel userData = JsonConvert.DeserializeObject<UserDataModel>(responseContent);

            Assert.AreEqual(name, userData.name);
            Assert.AreEqual(job, userData.job);
            Assert.IsNotNull(userData.createdAt);
        }
        public async Task AssertResponseBodyForUpdateUser(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            UserDataModel userData = JsonConvert.DeserializeObject<UserDataModel>(responseContent);

            Assert.AreEqual(name, userData.name);
            Assert.AreEqual(job, userData.job);
            Assert.IsNotNull(userData.createdAt);
        }

        public async Task AssertResponseBodyForListUser(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            ListUserDataModel listUserData = JsonConvert.DeserializeObject<ListUserDataModel>(responseContent);

            Assert.IsNotNull(listUserData.data);
        }

        public async Task AssertResponseBodyForSingleUser(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            SingleUserDataModel singleUserData = JsonConvert.DeserializeObject<SingleUserDataModel>(responseContent);

            Assert.IsNotNull(singleUserData.data);
        }
    }
}