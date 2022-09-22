using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace CustomerDemo
{
    public class CustomerSPDB
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        public CustomerSPDB()
        {
            string conStr = "server=.;database=SoleraEmployees;user id=sa;pwd=sa";
            con = new SqlConnection(conStr);
        }      

        public void AddCustomer(Customer c)
        {
            string insStr = $"AddCustomer";
            cmd = new SqlCommand(insStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cname", c.CNAME));
            cmd.Parameters.Add(new SqlParameter("@gender", c.CGENDER));
            cmd.Parameters.Add(new SqlParameter("@address", c.ADDRESS));
            cmd.Parameters.Add(new SqlParameter("@mobile", c.MOBILE));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public string UpdateCustomer(int cid, Customer c)
        {
            string updtStr = $"UpdateCustomer";
            cmd = new SqlCommand(updtStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", cid));
            cmd.Parameters.Add(new SqlParameter("@cname", c.CNAME));
            cmd.Parameters.Add(new SqlParameter("@gender", c.CGENDER));
            cmd.Parameters.Add(new SqlParameter("@address", c.ADDRESS));
            cmd.Parameters.Add(new SqlParameter("@mobile", c.MOBILE));
            SqlParameter sp = new SqlParameter("@sts",SqlDbType.VarChar,100);
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            string returnData = "";
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                returnData = cmd.Parameters[5].Value.ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return returnData;
        }
        public string DeleteCustomer(int cid)
        {
            string dltStr = $"DeleteCustomer";
            cmd = new SqlCommand(dltStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", cid));
            SqlParameter sp = new SqlParameter("@sts", SqlDbType.VarChar, 100);
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            string returnData = "";
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                returnData = cmd.Parameters[1].Value.ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return returnData;
        }
        public Customer FindCustomer(int cid)
        {
            string sltStr = $"FindCustomer";
            cmd = new SqlCommand(sltStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", cid));
            SqlDataReader dr = null;
            Customer cr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    cr = new Customer
                    {
                        CID = dr.GetInt32(0),
                        CNAME = dr.GetString(1),
                        CGENDER = dr.GetString(2),
                        ADDRESS = dr.GetString(3),
                        MOBILE = dr.GetString(4)
                    };
                }
                else
                {
                    cr = null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return cr;
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> lcst = new List<Customer>();
            string sltStr = $"CustomerSummary";
            cmd = new SqlCommand(sltStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = null;
            Customer cr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cr = new Customer
                    {
                        CID = dr.GetInt32(0),
                        CNAME = dr.GetString(1),
                        CGENDER = dr.GetString(2),
                        ADDRESS = dr.GetString(3),
                        MOBILE = dr.GetString(4)
                    };
                    lcst.Add(cr);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return lcst;
        }
    }
}
