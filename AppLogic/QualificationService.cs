using Abstractions.Services;
using DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Repositories.Abstractions;

namespace AppLogic
{
    public class QualificationService : IQualificationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Logger _logger;
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
                var fileName = await AddOrReplaceFileToDisk(attachmentFile);

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
                DeleteFileFromDisk(qualification.AttachmentUrl);

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
                    DeleteFileFromDisk(qualification.AttachmentUrl);

                    //Upload new file
                    var fileName = await AddOrReplaceFileToDisk(attachmentFile);

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

        public static async Task<string> AddOrReplaceFileToDisk(IFormFile attachmentFile)
        {
            var fileName = Path.GetFileName(attachmentFile.FileName);
            var filePath = Path.Combine("wwwroot", "uploads", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await attachmentFile.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public void DeleteFileFromDisk(string? filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}
