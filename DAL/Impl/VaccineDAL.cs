using Commons.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class VaccineDAL : BaseDAL<Vaccine>, ICRUD<Vaccine>
    {
        public VaccineDAL(DataBaseDbContext dbContext, ILogger<BaseDAL<Vaccine>> logger) : base(dbContext, logger)
        {
        }
    }
}