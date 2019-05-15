using Autofac;
using AzureFunctions.Autofac.Configuration;
using ParticipacaoLucros.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipacaoLucros.AzureFunctions.Helper
{
    public class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<FuncionariosService>().As<IFuncionariosService>().InstancePerDependency();
            }, functionName);
        }
    }
}
