using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class StavkaPorudzbineRepository : IStavkaPorudzbineRepository
    {
        private readonly WEBSHOPContext context;
        public StavkaPorudzbineRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<StavkaPorudzbine> GetStavkaPorudzbine()
        {
            return (from sl in context.StavkaPorudzbines select sl).ToList();
        }

        public StavkaPorudzbine GetStavkaPorudzbineById(int StavkaPorudzbineID)
        {
            return context.StavkaPorudzbines.FirstOrDefault(k => k.StavkaPorudzbineId == StavkaPorudzbineID);
        }

        public StavkaPorudzbine CreateStavkaPorudzbine(StavkaPorudzbine StavkaPorudzbine)
        {
            context.StavkaPorudzbines.Add(StavkaPorudzbine);
            return StavkaPorudzbine;
        }

        public void UpdateStavkaPorudzbine(StavkaPorudzbine StavkaPorudzbine)
        {
            throw new NotImplementedException();
        }

        public void DeleteStavkaPorudzbine(int StavkaPorudzbineID)
        {
            context.StavkaPorudzbines.Remove(context.StavkaPorudzbines.FirstOrDefault(sl => sl.StavkaPorudzbineId == StavkaPorudzbineID));
        }
    }
}
