using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface ITipKorisnikaRepository
    {
        List<TipKorisnika> GetTipKorisnika();

        TipKorisnika GetTipKorisnikaById(int TipKorisnikaID);

        TipKorisnika CreateTipKorisnika(TipKorisnika TipKorisnika);

        void UpdateTipKorisnika(TipKorisnika TipKorisnika);

        void DeleteTipKorisnika(int TipKorisnikaID);
        public bool SaveChanges();
    }
}
