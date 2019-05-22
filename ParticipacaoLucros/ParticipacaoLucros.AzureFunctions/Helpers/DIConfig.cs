using Autofac;
using Autofac.Core;
using AzureFunctions.Autofac.Configuration;
using ParticipacaoLucros.Models;
using ParticipacaoLucros.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipacaoLucros.AzureFunctions.Helpers
{
    public class DIConfig
    {
        internal const string databaseAddress = "https://partipacaolucros.firebaseio.com/";

        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                var register = new RestClient(databaseAddress);
                builder.Register(x => register).As<RestClient>().Named<RestClient>("plFirebase")
                .SingleInstance();

                var restClient = new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(RestClient),
                    (pi, ctx) => ctx.ResolveNamed("plFirebase", typeof(RestClient)));

                builder.RegisterType<Repositories.Repository<Funcionario>>()
                        .As<Repositories.IRepository<Funcionario>>().WithParameter(restClient).InstancePerDependency();

                builder.RegisterType<FuncionariosService>().
                        As<IFuncionariosService>().InstancePerDependency();
            }, functionName);
        }
    }
}

//IRepository<Funcionario> funcionarioRepository