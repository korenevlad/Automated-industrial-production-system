﻿using ReportManager.DataAccess.Data;
using ReportManager.Models;

namespace ReportManager.DataAccess.Repository.Implementation;
public class CuttingArrayProcessRepository : Repository<Cutting_array_process>, ICuttingArrayProcessRepository
{
    private readonly ApplicationDbContext _db;
    public CuttingArrayProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}