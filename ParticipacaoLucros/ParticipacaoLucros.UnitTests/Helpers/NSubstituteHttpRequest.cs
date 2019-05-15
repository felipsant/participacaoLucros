using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParticipacaoLucros.UnitTests.Helpers
{
    public static class NSubstituteHttpRequest
    {
        public static HttpRequest CreateMockRequest(object body)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(body);

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            var mockRequest = Substitute.For<HttpRequest>();
            mockRequest.Body.Returns(ms);

            return mockRequest;
        }
    }
}
