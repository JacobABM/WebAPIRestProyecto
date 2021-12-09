using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIRestProyecto.Models;


namespace WebAPIRestProyecto.Repositories
{
    public abstract class Repository<T> where T:class
    {
        public evaluacionalumnosContext Context { get; set; }
        public Repository(evaluacionalumnosContext context)
        {
            Context = context;
        }
        public virtual void Insert(T entidad)
        {
            Context.Add(entidad);
            Context.SaveChanges();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public IEnumerable<Evaluacion> GetEv(Usuario user)
        {
            return Context.Evaluacion.Include(x => x.IdAlumnoNavigation).Select(x => x).Where(x => x.IdAlumnoNavigation.Nombre == user.Nombre).ToList();
        }
        public virtual T Get(object id)
        {
            return Context.Find<T>(id);
        }
        public virtual void Update(T entidad)
        {
            Context.Update(entidad);
            Context.SaveChanges();
        }
        public virtual void Remove(T entidad)
        {
            Context.Remove(entidad);
            Context.SaveChanges();
        }

    }
}
