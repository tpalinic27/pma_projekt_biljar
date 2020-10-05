using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biljar.DbModels;
using biljar.Models;
using biljar.Mapper;

namespace biljar.Repositories
{
    public class AdminRepository
    {
        private readonly pma_biljarContext _dbContext;
        public AdminRepository(pma_biljarContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Models.Korisnik> GetAll()
        {

            return _dbContext.Korisnici.Select(x => KorisniciMapper.FromDatabase(x));

        }


        public IEnumerable<Models.Klubovi> GetAllKlubovi()
        {

            return _dbContext.Klubovi.Select(x => KluboviMapper.FromDatabase(x));

        }
        public async Task<Models.Korisnik> Getuser(int id)
        {
            var result = await _dbContext.Korisnici.SingleOrDefaultAsync(x => x.IdKorisnici == id);
            return KorisniciMapper.FromDatabase(result);
        }

        public Models.Klubovi GetKlub(string ime)
        {
            var result = _dbContext.Klubovi.Where(x => x.Ime == ime).SingleOrDefault();
            return KluboviMapper.FromDatabase(result);
        }

        public void UserEdit(Models.Korisnik k)
        {
            var dbUser = KorisniciMapper.ToDatabase(k);
            _dbContext.Korisnici.Update(dbUser);
            _dbContext.SaveChanges();
        }

        public void AddUser(Models.Korisnik k)
        {
            var korisnici = KorisniciMapper.ToDatabase(k);

            _dbContext.Korisnici.Add(korisnici);
            _dbContext.SaveChanges();

        }

        public void DeleteKlub(int id)
        {
            // Console.WriteLine("Repository is deleting user and his tasks...");
            while (_dbContext.Korisnici.Where(x => x.IdKlubovi.Equals(id)).FirstOrDefault() != null)
            {
                var korisnik = _dbContext.Korisnici.Where(x => x.IdKlubovi.Equals(id)).FirstOrDefault();
                korisnik.IdKlubovi = null;
                _dbContext.Korisnici.Update(korisnik);

                //_dbContext.Task.Remove(task);
                _dbContext.SaveChanges();
            }
            var k = _dbContext.Klubovi.Where(x => x.IdKlubovi == id).FirstOrDefault();
            _dbContext.Klubovi.Remove(k);
            _dbContext.SaveChanges();
        }
        public void DeleteKorisnik(int id)
        {
            //while (_dbContext.Korisnici.Where(x => x.IdKlubovi.Equals(id)).FirstOrDefault() != null)
            //{
            //    var task = _dbContext.Korisnici.Where(x => x.IdKorisnici.Equals(id)).FirstOrDefault();

            //    _dbContext.Korisnici.Remove(task);
            //    _dbContext.SaveChanges();
            //}
            var k = _dbContext.Korisnici.Where(x => x.IdKorisnici == id).FirstOrDefault();
            _dbContext.Korisnici.Remove(k);
            _dbContext.SaveChanges();
        }

        public void AddKlub(Models.Klubovi klub) 
        {
            var db_klub = KluboviMapper.ToDatabase(klub);
            _dbContext.Klubovi.Add(db_klub);
            _dbContext.SaveChanges();
            
        }

        public void EditKlub(Models.Klubovi klub)
        {
            var db_klub = KluboviMapper.ToDatabase(klub);
            _dbContext.Klubovi.Update(db_klub);
            _dbContext.SaveChanges();

        }
    }


}
