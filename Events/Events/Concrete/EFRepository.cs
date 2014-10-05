﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


using Events.Models;
using Events.Infrastructure;

namespace Events.Concrete
{
    public class EFRepository<T> where T : class
    {
        protected ApplicationDbContext context = new ApplicationDbContext();
        private string IdPropName;

        public EFRepository(string idpropname) 
        {
            IdPropName = idpropname;
        }
        public virtual Task<T> FindAsync(params object[] k)
        {
            return context.Set<T>().FindAsync(k);
        }

        public virtual IQueryable<T> Objects
        {
            get { return context.Set<T>(); }
        }

        public virtual async Task SaveInstance(T ev)
        {
            var id = (int)(typeof(T)).GetProperty(IdPropName).GetValue(ev);
            if (id == 0)
            {
                context.Set<T>().Add(ev);
            }
            else
            {
                T dbEntry = await context.Set<T>().FindAsync(id);
                foreach (var prop in typeof(T).GetProperties()) {
                    prop.SetValue(dbEntry, prop.GetValue(ev));
                }
                /*if (dbEntry != null)
                {
                    dbEntry.UserId = ev.UserId;
                    dbEntry.Latitude = ev.Latitude;
                    dbEntry.Longitude = ev.Longitude;
                }*/
            }
            await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}