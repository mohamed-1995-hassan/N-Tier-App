using BOS.Entities.Models;
using DAL.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operations
{
   public class CountryOperation
    {
        public readonly IBaseRepository<Country> _IbaseRepository;
      public  CountryOperation(IBaseRepository<Country> baseRepository) {

            _IbaseRepository = baseRepository;

        }
        public void Create(Country country) {

            _IbaseRepository.Insert(country);
            Console.WriteLine("mohamed hassan");
        }
    }
}
