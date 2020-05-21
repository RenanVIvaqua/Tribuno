using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Models;

namespace Tribuno3.Camadas.DTO
{
    public class RendimentoDTO : Operacao
    {
        private int id_Operacao;
        private DateTime dataRecebimento;
        public TipodeCalculo TipoCalculo;

        #region Encapsulamento
        public int Id_Usuario
        {
            get { return id_Usuario; }
            set { id_Usuario = value; }
        }
                     
        public int Id_Operacao
        {
            get { return id_Operacao; }
            set { id_Operacao = value; }
        }

        public string NomeOperacao
        {
            get { return nomeOperacao; }
            set { nomeOperacao = value; }
        }
        public double ValorOperacao
        {
            get { return valorOperacao; }
            set { valorOperacao = value; }
        }
        public double ValorParcela
        {
            get { return valorParcela; }
            set { valorParcela = value; }
        }

        public int QtdParcela
        {
            get { return qtdParcela; }
            set { qtdParcela = value; }
        }

        public string Descriacao
        {
            get { return descriacao; }
            set { descriacao = value; }
        }
        public DateTime DataCriacaoOper
        {
            get { return dataCriacaoOper; }
            set { dataCriacaoOper = value; }
        }

        public DateTime DataAltercao
        {
            get { return dataAltercao; }
            set { dataAltercao = value; }
        }

        public DateTime DataRecebimento
        {
            get { return dataRecebimento; }
            set { dataRecebimento = value; }
        }
        #endregion

    }

}