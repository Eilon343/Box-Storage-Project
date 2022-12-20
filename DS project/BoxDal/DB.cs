using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxModel;
using DataStructures;

namespace BoxDal
{
    public class DB
    {
        private static DB _instance;
        public static DB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DB();
                return _instance;
            }
        }
        private DB()
        {
        }
        public void Init()
        {
            //Adding Boxes 
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                var b = new Box(r.Next(1, 11), r.Next(1, 11));
                StorageManagment.Instance.AddBox(b);
            }
        }
    }
}
