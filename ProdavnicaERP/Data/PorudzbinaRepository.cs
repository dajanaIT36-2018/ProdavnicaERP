using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly WEBSHOPContext context;
        public PorudzbinaRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Porudzbina> GetPorudzbina()
        {
            return (from sl in context.Porudzbinas select sl).ToList();
        }

        public Porudzbina GetPorudzbinaById(int PorudzbinaID)
        {
            return context.Porudzbinas.FirstOrDefault(k => k.PorudzbinaId == PorudzbinaID);
        }

        public Porudzbina CreatePorudzbina(Porudzbina Porudzbina)
        {
            context.Porudzbinas.Add(Porudzbina);
            return Porudzbina;
        }

        public void UpdatePorudzbina(Porudzbina Porudzbina)
        {
            throw new NotImplementedException();
        }

        public void DeletePorudzbina(int PorudzbinaID)
        {
            context.Porudzbinas.Remove(context.Porudzbinas.FirstOrDefault(sl => sl.PorudzbinaId == PorudzbinaID));
        }
    }
}
