using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class DateProvider
    {
        private static DateProvider instance = null;
        private static object syncRoot = new Object();

        public static DateProvider getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new DateProvider();
                }
            }
            return instance;
        }

        public DateTime now()
        {
            return DateTime.Now;
        }
    }
}
