using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IStavkaPorudzbineRepository
    {
        List<StavkaPorudzbine> GetStavkaPorudzbine();
        List<StavkaPorudzbine> GetStavkaPorudzbineByPorudzbina(int porudzbinaId);

        StavkaPorudzbine GetStavkaPorudzbineById(int StavkaPorudzbineID);

        StavkaPorudzbine CreateStavkaPorudzbine(StavkaPorudzbine StavkaPorudzbine);

        void UpdateStavkaPorudzbine(StavkaPorudzbine StavkaPorudzbine);

        void DeleteStavkaPorudzbine(int StavkaPorudzbineID);
        public bool SaveChanges();
    }
}
