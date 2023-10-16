﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories.Base
{
    public class FactoryBase
    {
        private string _cnnStr = string.Empty;
        private string _baseUri = string.Empty;
        private HttpClient _APIClient = null;

        public string CnnStr
        {
            get
            {
                if (_cnnStr == string.Empty)
                {
                    _cnnStr = "Server=sql.decinfo-cchic.ca;Port=33306;Database=a23_web3_2133133;Uid=dev-2133133;Pwd=Mangamac21frizer";
                }
                return _cnnStr;
            }
        }

        public string BaseUri
        {
            get 
            {
                if (_baseUri == string.Empty)
                {
                    _baseUri = "https://sugoku.onrender.com/";
                }
                return _baseUri; 
            }
        }

        public HttpClient APIClient
        {
            get
            {
                if (_APIClient == null)
                {
                    _APIClient = new HttpClient();
                    _APIClient.DefaultRequestHeaders.Accept.Clear();
                    _APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                return _APIClient;
            }
        }
    }
}