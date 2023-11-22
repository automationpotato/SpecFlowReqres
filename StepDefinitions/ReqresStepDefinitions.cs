using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowReqres.DataModel;
using SpecFlowReqres.Support;
using System;
using System.Net;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowReqres.StepDefinitions
{
    [Binding]
    public class ReqresStepDefinitions
    {
        private readonly ReqresHttpClient reqresHttpClient;
        private HttpResponseMessage response;


        public ReqresStepDefinitions()
        {
            this.reqresHttpClient = new ReqresHttpClient();
        }

        [Given(@"the user sends a (.*) request with url as Reqres (.*)")]
        public async Task GivenTheUserSendsAGetRequestWithUrlAsReqres(string httpMethod, string uri)
        {
            switch(httpMethod.ToLower())
            {
                case "get":
                    response = await reqresHttpClient.SendGetRequestAsync(uri);
                    break;
                case "get user":
                    response = await reqresHttpClient.SendGetRequestAsync(uri);
                    break;
                case "post":
                    UserDataModel createUser = new(name: TestConfiguration.Name, job: TestConfiguration.Job);
                    response = await reqresHttpClient.SendPostRequestAsync(uri, createUser);
                    break;
                case "put":
                    UserDataModel putUser = new UserDataModel(name: TestConfiguration.Name, job: TestConfiguration.Job);
                    response = await reqresHttpClient.SendPutRequestAsync(uri, putUser);
                    break;
                case "patch":
                    UserDataModel patchUser = new UserDataModel(name: TestConfiguration.Name, job: TestConfiguration.Job);
                    response = await reqresHttpClient.SendPatchRequestAsync(uri, patchUser);
                    break;
                default:
                    throw new PendingStepException();
                    TestContext.WriteLine("Invalid input. Kindly select a valid HTTP Method: GET, POST, PUT or PATCH");
                    break;
            }
        }

        [Then(@"the request should return (.*) status code")]
        public void ThenTheRequestShouldReturnStatusCode(string statusCode)
        {
            switch(statusCode)
            {
                case "OK":
                    response.EnsureSuccessStatusCode();
                    Assert.IsTrue(response.IsSuccessStatusCode);
                    Assert.AreEqual(HttpStatusCode.OK.ToString(), statusCode);
                    break;
                case "NotFound":
                    response.EnsureSuccessStatusCode();
                    Assert.IsTrue(response.IsSuccessStatusCode);
                    Assert.AreEqual(HttpStatusCode.NotFound.ToString(), statusCode);
                    break;
                default:
                    throw new PendingStepException();
                    TestContext.WriteLine("Invalid input. Kindly select a valid HTTP Method: GET, POST, PUT or PATCH");
                    break;
            }

        }

        [Then(@"the response message for the (.*) request is as expected")]
        public async Task ThenTheResponseMessageForTheGETRequestIsAsExpected(string httpMethod)
        {

            switch (httpMethod.ToLower())
            {
                case "get":
                    await reqresHttpClient.AssertResponseBodyForListUser(response);
                    break;
                case "get user":
                    await reqresHttpClient.AssertResponseBodyForSingleUser(response);
                    break;
                case "post":
                    await reqresHttpClient.AssertResponseBodyForCreateUser(response);
                    break;
                case "put":
                    await reqresHttpClient.AssertResponseBodyForUpdateUser(response);
                    break;
                case "patch":
                    await reqresHttpClient.AssertResponseBodyForUpdateUser(response);
                    break;
                default:
                    throw new PendingStepException();
                    TestContext.WriteLine("Invalid input. Kindly select a valid HTTP Method: GET, POST, PUT or PATCH");
                    break;
            }
        }

    }
}
