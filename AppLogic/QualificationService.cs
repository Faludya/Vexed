using Abstractions.Services;
using DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment _env;

        public QualificationService(IRepositoryWrapper repositoryWrapper, Logger logger, IWebHostEnvironment env)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _env = env;
        }

        public async Task CreateQualification(Qualification qualification, IFormFile attachmentFile)
        {
            try
            {
                // Save the file to disk or database
                var fileName = Path.GetFileName(attachmentFile.FileName);
                var filePath = Path.Combine("wwwroot", "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await attachmentFile.CopyToAsync(fileStream);
                }

                // Set the attachment URL property of the qualification
                qualification.AttachmentUrl = "/uploads/" + fileName;


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
                //Delete file 
                var filePath = qualification.AttachmentUrl;
                if (!string.IsNullOrEmpty(filePath))
                {
                    var fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/'));
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

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

        public async Task UpdateQualification(Qualification qualification, IFormFile attachmentFile)
        {
            try
            {
                if(attachmentFile != null)
                {
                    //Delete old file 
                    var oldFilePath = qualification.AttachmentUrl;
                    if (!string.IsNullOrEmpty(oldFilePath))
                    {
                        var fullPath = Path.Combine(_env.WebRootPath, oldFilePath.TrimStart('/'));
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    //Upload new file
                    var fileName = Path.GetFileName(attachmentFile.FileName);
                    var filePath = Path.Combine("wwwroot", "uploads", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await attachmentFile.CopyToAsync(fileStream);
                    }

                    // Set the attachment URL property of the qualification
                    qualification.AttachmentUrl = "/uploads/" + fileName;

                }


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
