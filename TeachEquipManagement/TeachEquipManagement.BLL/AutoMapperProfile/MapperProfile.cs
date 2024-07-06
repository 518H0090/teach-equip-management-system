﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<User, UserDTOTest>();

            //CreateMap<UserDTOTest, User>();

            CreateMap<SupplierRequest, Supplier>();
        }
    }
}
