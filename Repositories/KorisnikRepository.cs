using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biljar.DbModels;
using biljar.Mapper;
using System.Security.Cryptography.X509Certificates;
using Task = System.Threading.Tasks.Task;
using Microsoft.EntityFrameworkCore;

namespace biljar.Repositories
{
    public class KorisnikRepository
    {
        private readonly pma_biljarContext _dbContext;

        public KorisnikRepository(pma_biljarContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Models.Korisnik> GetAll(int userId)
        {

            return _dbContext.Korisnici.Where(x => (x.IdKorisnici.Equals(userId))).Select(x => KorisniciMapper.FromDatabase(x));
        }
        public async Task<Models.Korisnik> GetAsync(int id)
        {
            var result = await _dbContext.Korisnici.SingleOrDefaultAsync(x => x.IdKorisnici == id);
            return KorisniciMapper.FromDatabase(result);
        }


        public  Models.Korisnik GetKorisnik(int id)
        {
            var result =  _dbContext.Korisnici.Where(x => x.IdKorisnici == id).SingleOrDefault();
            return KorisniciMapper.FromDatabase(result);
        }
        public void Save(Models.Korisnik k)
        {
            Console.WriteLine("Task: " + k);
            var DBk = KorisniciMapper.ToDatabase(k);
            _dbContext.Korisnici.Add(DBk);
            _dbContext.SaveChanges();
        }
        public async void Edit(Models.Korisnik k)
        {
            var DBk = KorisniciMapper.ToDatabase(k);
             _dbContext.Korisnici.Update(DBk);
            _dbContext.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _dbContext.Korisnici.SingleOrDefaultAsync(x => x.IdKorisnici == id);
            var dbCourse = KorisniciMapper.FromDatabase(result);
            _dbContext.Korisnici.Remove(result);
            _dbContext.SaveChanges();


        }

        public Korisnici GetKorisnikDB(int id)
        {
            var result = _dbContext.Korisnici.Include(x=>x.IdKluboviNavigation).Where(x => x.IdKorisnici == id).SingleOrDefault();
            return result;
        }


        public IEnumerable<Korisnici> GetAll2() 
        {
            var db = _dbContext.Korisnici.Include(c => c.IdKluboviNavigation);
            return db;
        }

        public IEnumerable<Models.Klubovi> GetKlubovi() 
        {
            var klubovi = _dbContext.Klubovi.Select(x => KluboviMapper.FromDatabase(x));
            return klubovi;
        }
    }
}
