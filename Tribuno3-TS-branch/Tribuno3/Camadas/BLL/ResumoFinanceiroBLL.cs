using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.DAL;
using Tribuno3.Processos;

namespace Tribuno3.Camadas.BLL
{
    public class ResumoFinanceiroBLL
    {
        private AcessoDados Acesso = new AcessoDados();
        private ResumoFinanceiroDAL resumoFinanceiroDAL = new ResumoFinanceiroDAL();

        public ReceitaDTO ConsultarResumoFinanceiro(int pidUsuario, int pMesReferente)
        {
           return resumoFinanceiroDAL.ConsultarResumoFinanceiro(pidUsuario, pMesReferente);        

        }
    }
}