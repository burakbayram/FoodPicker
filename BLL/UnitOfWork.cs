using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
       
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
        }
            public void Save()
        {
            db.SaveChanges();
        }
    }
}
