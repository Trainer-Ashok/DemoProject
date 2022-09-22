using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemo
{
    public class CustomerStore
    {
        private FileStream fs = null;
        private BinaryFormatter bf = null;
        public CustomerStore()
        {
            bf = new BinaryFormatter();
        }

        public void StoreCustomers(CustomerCollection cstData)
        {
            if (File.Exists("E:\\SoleraFiles\\CustData.txt"))
            {
                File.Delete("E:\\SoleraFiles\\CustData.txt");
            }
            fs = new FileStream("E:\\SoleraFiles\\CustData.txt", FileMode.Create, FileAccess.Write);
            bf.Serialize(fs,cstData);
        }

        public CustomerCollection RetrieveCustomers()
        {
            if (File.Exists("E:\\SoleraFiles\\CustData.txt"))
            {
                fs = new FileStream("E:\\SoleraFiles\\CustData.txt", FileMode.Open, FileAccess.Read);
                CustomerCollection cl = (CustomerCollection)bf.Deserialize(fs);
                fs.Flush();
                fs.Close();
                return cl;
            }
            else
            {
                return new CustomerCollection();
            }
        }
    }
}
