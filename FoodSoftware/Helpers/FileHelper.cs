using FoodSoftware.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSoftware.Helpers
{
    public interface IFileHelper : IHelper<Files>
    {
        public Task<string> DeleteAsync2(int id);
    }
    public class FileHelper : IFileHelper
    {
        IWebHostEnvironment _env;
        IGenericRepository<Files> repository;
        public FileHelper(IWebHostEnvironment env, IGenericRepository<Files> repo)
        {
            this.repository = repo;
            this._env = env;
        }

        public async Task<List<Files>> GetAllAsync()
        {

            var query = "sp_Files_GetAll";
            var result = await repository.GetAllAsync(query);
            foreach (var item in result)
            {
                //null checker must always done
                item.ThumbnailBase64 = item.ThumbnailByte != null ? Convert.ToBase64String(item.ThumbnailByte) : string.Empty;
                //result.Add(item);
            }
            //var folderName = Path.Combine(_env.WebRootPath, "Files");

            //foreach (var item in result)
            //{
            //    //   path= $"/Files/{item.Title}";
            //   contentType = Path.GetExtension(item.Thumbnail.FileName);
            //item.


            //var fileBytes = item.ThumbnailByte.ToArray();
            //string base64File = Convert.ToBase64String(fileBytes);
            //// act on the Base64 data
            //list.Add(base64File);


            //var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName, item.Title);

            //    var fileList = new List<Files>();

            //    //var folderPath = Path.Combine(_env.WebRootPath, "Files");
            //    if (Directory.Exists(folderName))
            //    {
            //        var provider = _env.ContentRootFileProvider;
            //        foreach (string fileName in Directory.GetFiles(folderName))
            //        {
            //            var fileInfo = provider.GetFileInfo(fileName);
            //            fileList.Add(fileInfo.);
            //        }
            //    }
            //    return fileList;
            //}
            return result;
        }

        private async Task<string> GetFileName(int id)
        {
            var query1 = "[sp_Files_GetName]";
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("id", id);
            var model = await repository.GetOneField(id, query1, "Id");
            return model.Title;
            //return fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
            //    || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
            //    || fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<List<Files>> GetAllForDrpDn()
        {
            var query = "sp_Files_GetAll";
            var result = await repository.GetAllAsync(query);
            
            return result.ToList();
        }

        public async Task<PaginationResultModel<Files>> GetAllWithPaginationAsync(int page, int limit)
        {
            var query = "sp_Files_GetAllWithPagination";
            var result = await repository.GetAllWithPagination(query, page, limit);
            return result;
        }

        public async Task<Files> GetByIdAsync(object id)
        {
            var query = "sp_Files_GetById";
            var model = await repository.GetByIdAsync(id, query, "Id");
            model.ThumbnailBase64 = model.ThumbnailByte != null ? Convert.ToBase64String(model.ThumbnailByte) : string.Empty;
            return model;
        }

        public async Task InsertAsync(Files model)
        {
            try
            {
                //model.ThumbnailBase64 = Convert.ToBase64String();
                var query = "[sp_Files_Insert]";
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("Url", model.Url);
                dictionary.Add("Alt", model.Alt);
                dictionary.Add("ThumbnailByte", model.ThumbnailByte);
                dictionary.Add("ThumbnailFileExtenstion", model.ThumbnailFileExtenstion);
                dictionary.Add("Title", model.Title);

                await repository.InsertAsync(query, dictionary);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public async Task UpdateAsync(Files model)
        {
            try
            {
                var query = "sp_Files_Update";
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("Url", model.Url);
                dictionary.Add("Alt", model.Alt);
                dictionary.Add("ThumbnailByte", model.ThumbnailByte);
                dictionary.Add("ThumbnailFileExtenstion", model.ThumbnailFileExtenstion);
                dictionary.Add("Title", model.Title);

                dictionary.Add("Id", model.Id);
                await repository.UpdateAsync(query, dictionary);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> UpdateWithIdAsync(Files t)
        {
            throw new NotImplementedException();
        }

        public Task<int> InserWithIdAsync(Files t)
        {
            throw new NotImplementedException();
        }
     

        public async Task DeleteAsync(int id)
        {
          
            try
            {
                var name = await GetFileName(id);
                if (name!=null || name !="")
                {
                    //string fName = name.Substring(sourceDir.Length + 1);
                    string folderPath = Path.Combine(_env.WebRootPath, "Files");
                    //System.IO.DirectoryInfo di = new DirectoryInfo(folderPath + @"\" + name);
                    System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);
                    foreach (FileInfo file in di.GetFiles())
                    {
                        if (file.Name== name)
                        {
                            var query = "sp_Files_Delete";
                            await repository.DeleteAsync(query, id, "Id");
                            file.Delete();
                           
                        }
                    }
                }
               

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteAsync2(int id)
        {
            var message = "حذف با موفقیت انجام شد";
            try
            {
                var name = await GetFileName(id);
                if (name != null || name != "")
                {
                    //string fName = name.Substring(sourceDir.Length + 1);
                    string folderPath = Path.Combine(_env.WebRootPath, "Files");
                    //System.IO.DirectoryInfo di = new DirectoryInfo(folderPath + @"\" + name);
                    System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);
                    foreach (FileInfo file in di.GetFiles())
                    {
                        if (file.Name == name)
                        {
                            var query = "sp_Files_Delete";
                            await repository.DeleteAsync(query, id, "Id");
                            file.Delete();

                        }
                    }
                }


            }
            catch (SqlException ex)
            {
                message = ex.Message;
                
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;

        }
    }
}
