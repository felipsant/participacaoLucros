using ParticipacaoLucros.Models;
using ParticipacaoLucros.Repositories;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using System;

namespace ParticipacaoLucros.Services
{
    public interface IFuncionariosService
    {
        Task<bool> AddOrUpdate(IEnumerable<Funcionario> lFuncionarios);
    }
    public class FuncionariosService : IFuncionariosService
    {
        public IRepository<Funcionario> _funcionarioRepository { get; private set; }
        public FuncionariosService(IRepository<Funcionario> funcionarioRepository)
        {
            this._funcionarioRepository = funcionarioRepository;
        }
        public async Task<bool> AddOrUpdate(IEnumerable<Funcionario> lFuncionarios)
        {
            try
            {
                return _funcionarioRepository.CreateList(lFuncionarios);

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<RetornoLucros> CalculaLucros()
        {
            //lFuncionarios.ToList()
            try
            {
                IEnumerable<Funcionario> LFuncionarios = await _funcionarioRepository.GetAll();
                //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<List<Funcionario>, IEnumerable<Funcionario>>()
                //.ForMember(c => c.salario_bruto, m => m.MapFrom(s => Convert.ToDecimal(s.salario_bruto.Replace("R$", "")))))
                //                .CreateMapper();
                //var models = mapper.Map<List<Funcionario>>(lFuncionarios);
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        

    }
}
