using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class ReceitaDTO : Operacao
    {
        private double? rendimento;
        private double? despesa;
        private double? receita;
        private double? lucro;
        private string mes_ref;

        #region Encapsulamento
        public int Id_Usuario
        {
            get { return id_Usuario; }
            set { id_Usuario = value; }
        }
        public double? Rendimento
        {
            get { return rendimento; }
            set { rendimento = value; }
        }
        public double? Despesa
        {
            get { return despesa; }
            set { despesa = value; }
        }
        public double? Receita
        {
            get { return receita; }
            set { receita = value; }
        }
        public double? Lucro
        {
            get { return lucro; }
            set { lucro = value; }
        }
        public string Mes_ref
        {
            get { return mes_ref; }
            set { mes_ref = value; }
        }
        #endregion


    }
}