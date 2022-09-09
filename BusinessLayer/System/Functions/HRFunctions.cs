using BusinessLayer.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HRFunctions : BaseFunctions
    {
        private static HRFunctions instance = null;
        public static HRFunctions Instance
        {
            get
            {
                if (instance == null)
                    instance = new HRFunctions();
                return instance;
            }
        }

        /// <summary>
        /// UC100: TIm kiem tinh thanh pho theo ten
        /// </summary>
        /// <param name="nameValue">Ten tinh hoach thanh pho</param>
        /// <returns></returns>
        public List<City> SearchByName(string nameValue)
        {
            //Ghi loc, kiem tra quyen, kiem du lieu co hop le khong?
            return CityExt.Instance.City_Select_By("Name", nameValue);
        }

        public List<City> SelectAllCity()
        {
            //Ghi loc, kiem tra quyen, kiem du lieu co hop le khong?
            return CityExt.Instance.City_Select_All();
        }
    }
}
