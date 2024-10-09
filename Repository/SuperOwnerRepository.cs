using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class SuperOwnerRepository : ISuperOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/superowner.csv";

        private readonly Serializer<SuperOwner> _serializer;

        private List<SuperOwner> _owners;

        public SuperOwnerRepository()
        {
            _serializer = new Serializer<SuperOwner>();
            _owners = _serializer.FromCSV(FilePath);
        }

        public List<SuperOwner> GetAll()
        {
            return _owners;
        }

        public SuperOwner Save(SuperOwner owner)
        {
            owner.Id = NextId();
            _owners.Add(owner);
            _serializer.ToCSV(FilePath, _owners);
            return owner;
        }

        public int NextId()
        {
            if (_owners.Count < 1)
            {
                return 1;
            }
            return _owners.Max(r => r.Id) + 1;
        }

        public void Delete(SuperOwner owner)
        {
            SuperOwner founded = _owners.Find(r => r.Id == owner.Id);
            _owners.Remove(founded);
            _serializer.ToCSV(FilePath, _owners);
        }

        public SuperOwner Update(SuperOwner owner)
        {
            SuperOwner current = _owners.Find(r => r.OwnerId == owner.OwnerId);
            int index = _owners.IndexOf(current);
            _owners.Remove(current);
            _owners.Insert(index, owner);
            _serializer.ToCSV(FilePath, _owners);
            return owner;
        }

        public SuperOwner GetByOwnerId(int ownerId)
        {
            return _owners.Find(r => r.OwnerId == ownerId);
        }
    }
}
