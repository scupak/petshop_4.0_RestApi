using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.ApplicationServices
{
   public interface IUserService
   {
       public FilteredList<User> GetAll();

    

      
   }
}
