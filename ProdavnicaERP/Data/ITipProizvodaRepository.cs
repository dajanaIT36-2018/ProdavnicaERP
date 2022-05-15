using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface ITipProizvodaRepository
    {
        List<TipProizvodum> GetTipProizvoda();

        TipProizvodum GetTipProizvodaById(int TipProizvodaID);

        TipProizvodum CreateTipProizvoda(TipProizvodum TipProizvoda);

        void UpdateTipProizvoda(TipProizvodum TipProizvoda);

        void DeleteTipProizvoda(int TipProizvodaID);
        public bool SaveChanges();
    }
}
