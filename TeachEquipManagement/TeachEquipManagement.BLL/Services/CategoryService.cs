using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Create(CategoryRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var category = _mapper.Map<Category>(request);

                    var entity = await _unitOfWork.CategoryRepository.InsertAsync(category);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new Category successfully";
                }

                else
                {
                    response.Data = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = validation.ToString();
                }
            }

            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            };

            return response;
        }

        public async Task<ApiResponse<List<CategoryResponse>>> GetAll()
        {
            ApiResponse<List<CategoryResponse>> response = new();

            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (!categories.Any())
            {
                _logger.Warning("Warning: Not Found Any Category");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<CategoryResponse>>(categories);
                response.Data = dataResponses;
                response.Message = "List Categories";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<CategoryResponse>> GetById(int id)
        {
            ApiResponse<CategoryResponse> response = new();

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                var dataResponse = _mapper.Map<CategoryResponse>(category);
                response.Data = dataResponse;
                response.Message = "Found Category";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Category");
                response.Data = null;
                response.Message = "Not Found Category";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Remove(int id)
        {
            ApiResponse<bool> response = new();

            try
            {
                _unitOfWork.CreateTransaction();

                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                if (category != null)
                {
                    _unitOfWork.CategoryRepository.Delete(category!);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.Message = "Remove Category";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Category");
                    response.Data = false;
                    response.Message = "Not Found Category";
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Update(CategoryUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

                    if (category != null)
                    {
                        var updateItem = _mapper.Map(request, category);
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Category";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Category");
                        response.Message = "Not Found Category";
                        response.StatusCode = StatusCodes.Status404NotFound;
                    }
                }

                else
                {
                    response.Data = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = validation.ToString();
                }

            }
            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }
    }
}
