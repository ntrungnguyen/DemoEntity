using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CityExt : CityFuncs
    {
        private static CityExt instance = null;
        public static CityExt Instance
        {
            get
            {
                if (instance == null)
                    instance = new CityExt();
                return instance;
            }
        }

        //Cac phuong thuc mo rong cua city
    }
}
