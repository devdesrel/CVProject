﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;
using INTRANET.Service.Interfaces;

namespace INTRANET.Service
{
    public class HrCvRelativeService : BaseService<HrCvRelative>, IHrCvRelativeService
    {
        public HrCvRelativeService(IHrCvRelativeRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Save(IList<HrCvRelative> models, int cvDetailId)
        {
            //to keep things simple 
            //and not to work out all possible insert/update/delete cases
            //first delete all previous awards, then save new ones
            this._repository.Delete(c=> c.HrCvDetailId == cvDetailId);
            foreach (var model in models)
            {
                model.HrCvDetailId = cvDetailId;
                this._repository.Add(model);
            }
            Save();
        }

        public IEnumerable<HrCvRelative> GetForCvDetail(int cvDetailId)
        {
            return this._repository.GetAny().Where(c => c.HrCvDetailId == cvDetailId);
        }
    }
}
