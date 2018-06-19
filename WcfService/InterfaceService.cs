//Copyright 2011 Jared Faris

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
namespace WcfService
{
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using static WcfService.ExampleCompositeType;

    [ServiceContract]
    public interface InterfaceService
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
          UriTemplate = "getcash/",
          BodyStyle = WebMessageBodyStyle.WrappedRequest,
          ResponseFormat = WebMessageFormat.Json)]
          string getCash();

        [OperationContract]
        [WebGet(UriTemplate = "startSdkPaylink/",
        ResponseFormat = WebMessageFormat.Json)]
        string startSdkPaylink();

        [OperationContract]
        [WebGet(UriTemplate = "finishSdkPaylink/",
        ResponseFormat = WebMessageFormat.Json)]
        string finishSdkPaylink();

        [OperationContract]
        [WebGet(UriTemplate = "getRemainder/?val={value}",
    ResponseFormat = WebMessageFormat.Json)]
        string getRemainder(string value);


   
        [OperationContract]
        [WebGet(UriTemplate = "startCaspit/?val={price}",
        ResponseFormat = WebMessageFormat.Json)]
        string startCaspit(string price);


        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "printOrder/",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string PrintOrder(string input);


    }

}