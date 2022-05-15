using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
      public class VrstaProizvodaRepository : IVrstaProizvodaRepository
        {
            private readonly WEBSHOPContext context;
            public VrstaProizvodaRepository(WEBSHOPContext context)
            {

                this.context = context;
            }

            public bool SaveChanges()
            {
                return context.SaveChanges() > 0;
            }


            public List<VrstaProizvodum> GetVrstaProizvoda()
            {
                return (from sl in context.VrstaProizvoda select sl).ToList();
            }

            public VrstaProizvodum GetVrstaProizvodaById(int VrstaProizvodaID)
            {
                return context.VrstaProizvoda.FirstOrDefault(k => k.VrstaProizvodaId == VrstaProizvodaID);
            }

            public VrstaProizvodum CreateVrstaProizvoda(VrstaProizvodum VrstaProizvoda)
            {
                context.VrstaProizvoda.Add(VrstaProizvoda);
                return VrstaProizvoda;
            }

            public void UpdateVrstaProizvoda(VrstaProizvodum VrstaProizvoda)
            {
                throw new NotImplementedException();
            }

            public void DeleteVrstaProizvoda(int VrstaProizvodaID)
            {
                context.VrstaProizvoda.Remove(context.VrstaProizvoda.FirstOrDefault(sl => sl.VrstaProizvodaId == VrstaProizvodaID));
            }
        }
    
}
