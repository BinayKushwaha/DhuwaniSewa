using DhuwaniSewa.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Repository
{
    public abstract class BaseRepository
    {
        private ApplicationContext ApplicationContext { get;  set; }

        public BaseRepository(ApplicationContext  applicationContext)
        {
            this.ApplicationContext = applicationContext;
        }
        public ApplicationContext DataContext
        {
            get 
            {
                return ApplicationContext;
            }
        }
    }
}
