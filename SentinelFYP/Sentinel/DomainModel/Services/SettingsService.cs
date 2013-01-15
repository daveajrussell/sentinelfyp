using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;

namespace DomainModel.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            if (settingsRepository == null)
                throw new ArgumentNullException("Repository");

            _settingsRepository = settingsRepository;
        }

        public string BaseDirectory
        {
            get
            {
                return _settingsRepository.BaseDirectory;
            }
        }
    }
}