using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IStavkaKorpeRepository
    {
        List<StavkaKorpe> GetStavkaKorpe();

        StavkaKorpe GetStavkaKorpeById(int StavkaKorpeID);
        List<StavkaKorpe> GetStavkaKorpeByKorpa(int korpaId);

        StavkaKorpe CreateStavkaKorpe(StavkaKorpe StavkaKorpe);

        void UpdateStavkaKorpe(StavkaKorpe StavkaKorpe);

        void DeleteStavkaKorpe(int StavkaKorpeID);
        public bool SaveChanges();
    }
}
