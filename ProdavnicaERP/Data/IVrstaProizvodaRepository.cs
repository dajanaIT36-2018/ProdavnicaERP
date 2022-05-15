using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IVrstaProizvodaRepository
    {
        List<VrstaProizvodum> GetVrstaProizvoda();

        VrstaProizvodum GetVrstaProizvodaById(int VrstaProizvodaID);

        VrstaProizvodum CreateVrstaProizvoda(VrstaProizvodum VrstaProizvoda);

        void UpdateVrstaProizvoda(VrstaProizvodum VrstaProizvoda);

        void DeleteVrstaProizvoda(int VrstaProizvodaID);
        public bool SaveChanges();
    }
}
