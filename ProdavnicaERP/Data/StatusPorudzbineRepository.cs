using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class StatusPorudzbineRepository : IStatusPorudzbineRepository
    {
        private readonly WEBSHOPContext context;
        public StatusPorudzbineRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<StatusPorudzbine> GetStatusPorudzbine()
        {
            return (from sl in context.StatusPorudzbines select sl).ToList();
        }

        public StatusPorudzbine GetStatusPorudzbineById(int StatusPorudzbineID)
        {
            return context.StatusPorudzbines.FirstOrDefault(k => k.StatusPorudzbineId == StatusPorudzbineID);
        }

        public StatusPorudzbine CreateStatusPorudzbine(StatusPorudzbine StatusPorudzbine)
        {
            context.StatusPorudzbines.Add(StatusPorudzbine);
            return StatusPorudzbine;
        }

        public void UpdateStatusPorudzbine(StatusPorudzbine StatusPorudzbine)
        {
            throw new NotImplementedException();
        }

        public void DeleteStatusPorudzbine(int StatusPorudzbineID)
        {
            context.StatusPorudzbines.Remove(context.StatusPorudzbines.FirstOrDefault(sl => sl.StatusPorudzbineId == StatusPorudzbineID));
        }
    }
}
