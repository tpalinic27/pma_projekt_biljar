using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biljar.Models;
using biljar.Repositories;

namespace biljar.Services
{
    public class AdminService
    {
        public AdminRepository _adminRepository;

        public AdminService(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public IEnumerable<Korisnik> GetAll()
        {
            return _adminRepository.GetAll();
        }

        public IEnumerable<Models.Klubovi> GetAllKlubovi()
        {

            return _adminRepository.GetAllKlubovi();

        }

        public Klubovi GetKlub(string k)
        {
            return _adminRepository.GetKlub(k);
        }

        public void AddUser(Models.Korisnik k)
        {
            _adminRepository.AddUser(k);
        }

        //public IEnumerable<TaskToDo> GetTasks(int id)
        //{
        //    return _adminRepository.GetTasks(id);
        //}
        public async Task<Korisnik> Getuser(int id)
        {
            return await _adminRepository.Getuser(id);
        }
        public void UserEdit(Korisnik k)
        {
            _adminRepository.UserEdit(k);
        }

        public void DeleteKlub(int id)
        {
            _adminRepository.DeleteKlub(id);
        }
        public void DeleteKorisnik(int id)
        {
            _adminRepository.DeleteKorisnik(id);
        }

        public void AddKlub(Models.Klubovi klub)
        {
            _adminRepository.AddKlub(klub);

        }
        public void EditKlub(Models.Klubovi klub)
        {
            _adminRepository.EditKlub(klub);


        }
    }
}
