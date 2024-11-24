using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HauteCuisine.BLL
{
    public class User
    {
        public event Action<string> WhatsForDinner;

        protected void CookingStarted(string text)
        {
            WhatsForDinner?.Invoke(text);
        }

        public void UseService()
        {
            
        }
    }
}
