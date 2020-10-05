using biljar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biljar.Mapper;
using biljar.Models;


namespace biljar.Services
{
    public class KorisnikService
    {
        public KorisnikRepository _KorisnikRepository;

        public KorisnikService(KorisnikRepository KorisnikRepository)
        {
            _KorisnikRepository = KorisnikRepository;
        }
        public IEnumerable<Korisnik> GetAll(int KorisnikId)
        {
            return _KorisnikRepository.GetAll(KorisnikId);
        }
        public async Task<Korisnik> GetAsync(int id)
        {
            return await _KorisnikRepository.GetAsync(id);
        }
        public void Save(Korisnik k)
        {
            _KorisnikRepository.Save(k);
        }

        public  Models.Korisnik GetKorisnik(int id)
        {
            return _KorisnikRepository.GetKorisnik(id);
        }

        public void Edit(Models.Korisnik k) 
        {
            _KorisnikRepository.Edit(k);
        }

        public DbModels.Korisnici GetKorisnikDB(int id)
        {
            
            return _KorisnikRepository.GetKorisnikDB(id);
        }

        public IEnumerable<DbModels.Korisnici> GetAll2()
        {
          
            return _KorisnikRepository.GetAll2();
        }

        public IEnumerable<Models.Klubovi> GetKlubovi()
        {
           return _KorisnikRepository.GetKlubovi();
        }

    }
}
