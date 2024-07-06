﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.Repositories
{
    public class ApprovalRequestRepository : GenericRepository<ApprovalRequest>, IApprovalRequestRepository
    {
        private readonly DataContext _context;

        public ApprovalRequestRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
