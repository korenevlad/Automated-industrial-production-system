﻿using ReportManager.DataAccess.Data;
using ReportManager.Models;

namespace ReportManager.DataAccess.Repository.Implementation;
public class ParametersMoldingAndInitialExposureProcessRepository : Repository<Parameters_molding_and_initial_exposure_process>, IParametersMoldingAndInitialExposureProcessRepository
{
    private readonly ApplicationDbContext _db;
    public ParametersMoldingAndInitialExposureProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}