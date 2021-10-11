using BOS.Entities;
using BOS.Entities.Models;
using DAL.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomRepositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        IdentityContext _identityContext;
        public CountryRepository(IdentityContext identityContext) : base(identityContext)
        {
            _identityContext = identityContext;
        }
        public string CountryAge(int id)
        {
            return _identityContext.Countries.SingleOrDefault(i => i.ID == id).Name;
        }

    }
}
