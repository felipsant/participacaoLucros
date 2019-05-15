using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipacaoLucros.IntegrationTests
{
    public class BaseTest
    {
        internal const string databaseAddress = " http://localhost:7071/api/";
        internal RestClient client;

        protected BaseTest()
        {
            client = new RestClient(databaseAddress);
        }
    }
}

