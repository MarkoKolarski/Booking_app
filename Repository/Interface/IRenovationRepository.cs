using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    internal interface IRenovationRepository
    {

        List<Renovation> GetActiveByAccommodationId(int accommodationId);
        List<Renovation> GetAll();
        Renovation GetById(int id);
        Renovation Save(Renovation renovation);
        int NextId();
        void LoadRenovations();
        Renovation GetByAccommodation(int accommodationId);
        Renovation Update(Renovation renovation);
    }
}
