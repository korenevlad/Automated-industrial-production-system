﻿using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.Models;

namespace KafkaConsumer.DataAccess.Repository.Implementation;

public class MoldingAndInitialExposureProcessRepository : Repository<Molding_and_initial_exposure_process>, IMoldingAndInitialExposureProcessRepository
{
    private readonly ApplicationDbContext _db;
    public MoldingAndInitialExposureProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}