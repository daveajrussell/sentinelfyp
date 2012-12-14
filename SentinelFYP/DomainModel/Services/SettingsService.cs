using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Interfaces.Services;

namespace DomainModel.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly string _baseDirectory;

        public SettingsService(string baseDirectory)
        {
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