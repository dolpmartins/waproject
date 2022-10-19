using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaProject.App.API.Model;
using WaProject.Domain.Entities;
using WaProject.Domain.Interfaces;
using WaProject.Service.Validators;

namespace WaProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private IBaseService<Job> _baseJobService;
        private IJobService _jobService;

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        public TaskController(ILogger<TaskController> logger, IBaseService<Job> baseJobService, IJobService jobService)
        {
            _logger = logger;
            _baseJobService = baseJobService;
            _jobService = jobService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseJobService.Get<JobModel>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseJobService.GetById<JobModel>(id));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() =>
            {
                _baseJobService.Delete(id);
                return true;
            });
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateJobModel job)
        {
            if (job == null)
                return NotFound();

            return Execute(() => _baseJobService.Update<UpdateJobModel, JobModel, JobValidator>(job));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateJobModel job)
        {
            if (job == null)
                return NotFound();

            return Execute(() => _baseJobService.Add<CreateJobModel, JobModel, JobValidator>(job));
        }

        [HttpPut("Completed/{id}")]
        public IActionResult Completed(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _jobService.Completed(id));
        }
    }
}
