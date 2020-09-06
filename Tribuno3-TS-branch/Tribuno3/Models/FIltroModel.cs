using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tribuno3.Models
{
    public class GridModel 
    {
        public TipoGrid TipoGrid { get; set; }

        public TipoFiltro Filtro { get; set; }

    }

    public enum TipoGrid
    {
        [Description("Grid de Rendimento")]
        GridRendimento,

        [Description("Grid de Passivos")]
        GridPassivo
    }

    public enum TipoFiltro
    {
        [Description("Exibir apenas as parcelas referente ao mês")]
        Mes,

        [Description("Exibir todas as parcelas")]
        Todos
    }

   
}