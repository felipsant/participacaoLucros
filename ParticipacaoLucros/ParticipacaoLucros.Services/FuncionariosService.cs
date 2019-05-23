using ParticipacaoLucros.Models;
using ParticipacaoLucros.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ParticipacaoLucros.Services
{
    public interface IFuncionariosService
    {
        Task<bool> AddOrUpdate(IEnumerable<Funcionario> lFuncionarios);
        Task<IEnumerable<Funcionario>> GetAll();
        Task<RetornoLucros> CalculaLucros(decimal totalDisponibilizado);
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

        public async Task<IEnumerable<Funcionario>> GetAll()
        {
            try
            {
                return _funcionarioRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region CalculaLucros
        public async Task<RetornoLucros> CalculaLucros(decimal totalDisponibilizado)
        {
            try
            {
                Dictionary<string, int> PesosPorArea = GetPesosPorArea();
                IEnumerable<Funcionario> LFuncionarios = _funcionarioRepository.GetAll();
                var participacoes = LFuncionarios.Select(s => new Participacao() {
                    valor_da_participação = CalculaParticipacao(ConvertStringDecimal(s.salario_bruto),
                            PesosPorArea[s.area],
                            calcula_PTA(Convert.ToDateTime(s.data_de_admissao)),
                            calcula_PSF(ConvertStringDecimal(s.salario_bruto), s.cargo)
                        ),
                    matricula = s.matricula,
                    nome = s.nome
                }).ToList();

                var result = new RetornoLucros()
                {
                    participacoes = participacoes,
                    total_disponibilizado = totalDisponibilizado,
                    total_distribuido = participacoes.Sum(c => c.valor_da_participação),
                    total_de_funcionarios = participacoes.Count,
                };
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private int calcula_PSF(decimal salario_bruto, string cargo)
        {
            const decimal salario_minimo = 998;
            int qtdSalarioMinimos = Convert.ToInt32(salario_bruto / salario_minimo);
            bool eEstagiario = (cargo == "Estagiário");
            if (!eEstagiario) { 
                if (qtdSalarioMinimos > 8)
                    return 5;
                if (qtdSalarioMinimos > 5)
                    return 3;
                if (qtdSalarioMinimos > 3)
                    return 2;
            }
            return 1;
        }

        private int calcula_PTA(DateTime admissao)
        {
            int anos = Convert.ToInt32((DateTime.Now.Subtract(admissao).TotalDays / 365));
            if(anos < 1)
                return 1;
            if (anos < 3)
                return 2;
            if (anos < 8)
                return 3;
            return 5;
        }
        private decimal ConvertStringDecimal(string salario_bruto)
        {
            return Convert.ToDecimal(salario_bruto.Replace("R$ ", "").Replace(".", "").Replace(",", "."));
        }
        private decimal CalculaParticipacao(decimal salario_bruto, int PAA, int PTA, int PFS)
        {
            return (((salario_bruto * PTA) + (salario_bruto * PAA)) / PFS) * 12;
        }
        private Dictionary<string, int> GetPesosPorArea()
        {
            Dictionary<string, int> Pesos = new Dictionary<string, int>();
            Pesos.Add("Diretoria", 1);
            Pesos.Add("Contabilidade", 2);
            Pesos.Add("Financeiro", 2);
            Pesos.Add("Tecnologia", 2);
            Pesos.Add("Serviços Gerais", 3);
            Pesos.Add("Relacionamento com o Cliente", 5);
            return Pesos;
        }
        #endregion

    }
}
