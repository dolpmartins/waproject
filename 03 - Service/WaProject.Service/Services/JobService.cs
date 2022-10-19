using AutoMapper;
using System;

using WaProject.Domain.Entities;
using WaProject.Domain.Interfaces;

namespace WaProject.Service.Services
{
    public class JobService : IJobService
    {
        private readonly IBaseRepository<Job> _baseRepository;
        private readonly IMapper _mapper;

        public JobService(IBaseRepository<Job> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public Job Completed(int id)
        {
            var job = _baseRepository.Select(id);

            job.Completed = true;
            job.CompletedDate = DateTime.Now;


            _baseRepository.Update(job);

            return job;
        }
    }
}
