﻿using System;
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
                    _ConnectionString = "Server=localhost;Port=3306;Database=bd_dev_home;Uid=root;Pwd=Mateo235";
                }

                return _ConnectionString;
            }
        }
    }
}
