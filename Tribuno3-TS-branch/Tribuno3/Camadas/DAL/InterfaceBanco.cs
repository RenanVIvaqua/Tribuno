using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace Tribuno3.Camadas.DAL
{
    public abstract class InterfaceBanco
    {
        internal abstract string Executar(string NomeProcedure, List<SqlParameter> parametros);

        internal abstract DataTable Consultar(string NomeProcedure, List<SqlParameter> parametros);

        internal abstract DataTable ConsultarQuery(string QuerySelect);

        








    }
}