using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories.Base
{
    public abstract class FactoryBase
    {
        private string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                if (_ConnectionString == string.Empty)
                {
                    _ConnectionString = "Server=sql.decinfo-cchic.ca;Port=33306;Database=a23_déploiement_a23_2133752;Uid=dev-2133752;Pwd=Mateo235";
                }

                return _ConnectionString;
            }
        }
    }
}
