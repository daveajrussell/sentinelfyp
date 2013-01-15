using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;

namespace DomainModel.Services
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly string _baseDirectory;

        public SettingsRepository(string baseDirectory)
        {
            if(string.IsNullOrEmpty(baseDirectory))
                throw new ArgumentNullException("Base Directory");

            _baseDirectory = baseDirectory;
        }

        public string BaseDirectory
        {
            get
            {
                return _baseDirectory;
            }
        }
    }
}