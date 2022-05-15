using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class StavkaKorpeRepository : IStavkaKorpeRepository
    {
        private readonly WEBSHOPContext context;
        public StavkaKorpeRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<StavkaKorpe> GetStavkaKorpe()
        {
            return (from sl in context.StavkaKorpes select sl).ToList();
        }

        public StavkaKorpe GetStavkaKorpeById(int StavkaKorpeID)
        {
            return context.StavkaKorpes.FirstOrDefault(k => k.StavkaKorpeId == StavkaKorpeID);
        }

        public StavkaKorpe CreateStavkaKorpe(StavkaKorpe StavkaKorpe)
        {
            context.StavkaKorpes.Add(StavkaKorpe);
            return StavkaKorpe;
        }

        public void UpdateStavkaKorpe(StavkaKorpe StavkaKorpe)
        {
            throw new NotImplementedException();
        }

        public void DeleteStavkaKorpe(int StavkaKorpeID)
        {
            context.StavkaKorpes.Remove(context.StavkaKorpes.FirstOrDefault(sl => sl.StavkaKorpeId == StavkaKorpeID));
        }
    }
}
