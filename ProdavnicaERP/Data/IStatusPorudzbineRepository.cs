using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IStatusPorudzbineRepository
    {
        List<StatusPorudzbine> GetStatusPorudzbine();

        StatusPorudzbine GetStatusPorudzbineById(int StatusPorudzbineID);

        StatusPorudzbine CreateStatusPorudzbine(StatusPorudzbine StatusPorudzbine);

        void UpdateStatusPorudzbine(StatusPorudzbine StatusPorudzbine);

        void DeleteStatusPorudzbine(int StatusPorudzbineID);
        public bool SaveChanges();
    }
}
