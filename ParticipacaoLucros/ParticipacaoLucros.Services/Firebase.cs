using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipacaoLucros.Services
{
    public class Firebase
    {
        internal const string databaseAddress = "https://partipacaolucros.firebaseio.com/";
        internal RestClient client;
    }
}
