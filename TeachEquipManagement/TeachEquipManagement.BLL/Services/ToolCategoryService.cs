using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.CommonModels;

namespace TeachEquipManagement.BLL.Services
{
    public class ToolCategoryService : IToolCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ToolCategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Create(ToolCategoryRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var tool = await _unitOfWork.ToolRepository.GetByIdAsync(request.ToolId);
                    var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

                    if (tool == null || category == null)
                    {
                        _logger.Warning($"Warning with : Not Found Object to mapping");
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Message = "Not Found Object to mapping";

                        return response;
                    }

                    var toolCategory = _mapper.Map<ToolCategory>(request);

                    var entity = await _unitOfWork.ToolCategoryRepository.InsertAsync(toolCategory);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new relationship tool-category successfully";
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

        public async Task<ApiResponse<List<ToolCategoryResponse>>> GetAll()
        {
            ApiResponse<List<ToolCategoryResponse>> response = new();

            var relationship = await _unitOfWork.QueryToolCategoryRepository.GetAllToolCategoryIncludeRelationship();

            if (!relationship.Any())
            {
                _logger.Warning("Warning: Not Found Any Relationship Tool - Category");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<ToolCategoryResponse>>(relationship);
                response.Data = dataResponses;
                response.Message = "List Tool Category";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<ToolCategoryResponse>> GetById(ToolCategoryRequest request)
        {
            ApiResponse<ToolCategoryResponse> response = new();

            var toolCategory = await _unitOfWork.QueryToolCategoryRepository.GetToolCategoryIncludeRelationship(request.ToolId, request.CategoryId);

            if (toolCategory != null)
            {
                var dataResponse = _mapper.Map<ToolCategoryResponse>(toolCategory);
                response.Data = dataResponse;
                response.Message = "Found Relationship";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Relationship");
                response.Data = null;
                response.Message = "Not Found Relationship";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Remove(ToolCategoryRequest request)
        {
            ApiResponse<bool> response = new();

            try
            {
                _unitOfWork.CreateTransaction();

                QueryModel<ToolCategory> query = new QueryModel<ToolCategory>
                {
                    QueryCondition = query => query.ToolId == request.ToolId
                    && query.CategoryId == request.CategoryId
                };

                var toolCategory = _unitOfWork.ToolCategoryRepository.GetQueryable(query).FirstOrDefault();

                if (toolCategory != null)
                {
                    _unitOfWork.ToolCategoryRepository.Delete(toolCategory!);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.Message = "Remove Relationship Tool Category";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Relationship Tool Category");
                    response.Message = "Not Found Relationship Tool Category";
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
    }
}
