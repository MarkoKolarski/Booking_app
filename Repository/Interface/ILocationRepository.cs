using System.Collections.Generic;

namespace BookingApp.Repository.Interface
{
    public interface ILocationRepository
    {
        List<string> GetAll();
    }
}
