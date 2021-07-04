using DhuwaniSewa.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Repository
{
    public abstract class BaseRepository
    {
        public ApplicationContext ApplicationContext { get; private set; }

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
