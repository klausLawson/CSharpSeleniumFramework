using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpSeleniumFramework.utilities
{
    class JsonReader
    {
        public JsonReader()
        {

        }
        public String extractData(String tokenName)
        {
            String myJsonString = File.ReadAllText("utilities/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public String[] extractDataArray(String tokenName)
        {
            String myJsonString = File.ReadAllText("utilities/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            List<String> productList =  jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productList.ToArray();
        }
    }
    
}
