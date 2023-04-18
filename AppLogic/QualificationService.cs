using Abstractions.Services;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace AppLogic
{
    public class QualificationService : IQualificationService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public QualificationService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateQualification(Qualification qualification)
        {
            try
            {
                await _repositoryWrapper.QualificationRepository.Create(qualification);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteQualification(Qualification qualification)
        {
            try
            {
                await _repositoryWrapper.QualificationRepository.Delete(qualification);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Qualification>> GetAllQualifications()
        {
            try
            {
                var queryable = await _repositoryWrapper.QualificationRepository.FindAll();
                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<Qualification> GetQualificationById(int id)
        {
            try
            {
                return await _repositoryWrapper.QualificationRepository.GetQualificationById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Qualification>> GetQualifications(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.QualificationRepository.GetQualifications(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateQualification(Qualification qualification)
        {
            try
            {
                await _repositoryWrapper.QualificationRepository.Update(qualification);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
