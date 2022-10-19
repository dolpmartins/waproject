using WaProject.Domain.Entities;

namespace WaProject.Domain.Interfaces
{

    public interface IJobService
    {
        Job Completed(int id);

    }
}
