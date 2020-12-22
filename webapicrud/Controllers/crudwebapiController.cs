using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapicrud.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace webapicrud.Controllers
{
    public class crudwebapiController : ApiController
    {
        public static string mainconn = ConfigurationManager.ConnectionStrings["serverconn"].ConnectionString;
        SqlConnection con = new SqlConnection(mainconn);
        SqlCommand cmd;
        SqlDataReader sdr;
      
        [HttpGet]
        public IHttpActionResult getdata()
        {
            List<EmpClass> ec = new List<EmpClass>();
            //string mainconn = ConfigurationManager.ConnectionStrings["serverconn"].ConnectionString;
            //SqlConnection con = new SqlConnection(mainconn);

            con.Open();
            cmd = new SqlCommand("select * from tbl_emp",con);
            sdr = cmd.ExecuteReader();

            while(sdr.Read())
            {
                ec.Add(new EmpClass()
                {
                    no = Convert.ToInt32(sdr.GetValue(0)),
                    firstname = sdr.GetValue(1).ToString(),
                    lastname = sdr.GetValue(2).ToString(),
                    address = sdr.GetValue(3).ToString(),
                    contactno = sdr.GetValue(4).ToString(),
                    state = sdr.GetValue(5).ToString()
                });
            }
            con.Close();

            return Ok(ec);
        }

        [HttpPost]
        public IHttpActionResult insert(EmpClass ecobj)
        {

            /*cmd = new SqlCommand("insert into tbl_emp values('" + Convert.ToInt32(ecobj.no) + "'," + ecobj.firstname + "," + ecobj.lastname + "," + ecobj.address + "," + ecobj.contactno + "," + ecobj.state + ")", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();*/

            SqlCommand cmd1 = new SqlCommand();
            /*cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "insert into tbl_emp(no,firstname,lastname,address,contactno,state) values(@empno,@empfnm,@emplnm,@empadd,@contact,@empstate)";
            cmd1.Connection = con;*/

            //cmd1 = new SqlCommand("insert into tbl_emp(no,firstname,lastname,address,contactno,state) values(@empno,@empfnm,@emplnm,@empadd,@contact,@empstate)",con);
            cmd1 = new SqlCommand("insert into tbl_emp(no,firstname,lastname,address,contactno,state) values("+Convert.ToInt32(ecobj.no)+",'"+ecobj.firstname+"','"+ecobj.lastname+"','"+ecobj.address+"','"+ecobj.contactno+"','"+ecobj.state+"')", con);

            /*cmd1.Parameters.AddWithValue("@empno", Convert.ToInt32(ecobj.no));
            cmd1.Parameters.AddWithValue("@empfnm", ecobj.firstname);
            cmd1.Parameters.AddWithValue("@emplnm", ecobj.lastname);
            cmd1.Parameters.AddWithValue("@empadd", ecobj.address);
            cmd1.Parameters.AddWithValue("@contact", ecobj.contactno);
            cmd1.Parameters.AddWithValue("@empstate", ecobj.state);*/
            con.Open();
            int rowinserted = cmd1.ExecuteNonQuery();
            con.Close();

            return Ok(rowinserted);
        }

        
        /*public IHttpActionResult GetEmpid(int id)
        {
            List<EmpClass> ec = new List<EmpClass>();
            //ec = null;
            con.Open();
            cmd = new SqlCommand("select * from tbl_emp where no = "+Convert.ToInt32(id)+"", con);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ec.Add(new EmpClass()
                {
                    no = Convert.ToInt32(sdr.GetValue(0)),
                    firstname = sdr.GetValue(1).ToString(),
                    lastname = sdr.GetValue(2).ToString(),
                    address = sdr.GetValue(3).ToString(),
                    contactno = sdr.GetValue(4).ToString(),
                    state = sdr.GetValue(5).ToString()
                });
            }
            con.Close();
            webapiexampleEntities db = new webapiexampleEntities();
            EmpClass ec = null;
            ec = db.tbl_emp.Where(x => x.no == id).Select(x => new EmpClass()
            {
                no = x.no,
                firstname = x.firstname,
                lastname = x.lastname,
                address = x.address,
                contactno = x.contactno,
                state = x.state,
            }).FirstOrDefault<EmpClass>();

            if(ec==null)
            {
               return NotFound();
            }
            return Ok(ec);
        }*/

        [HttpPut]
        public IHttpActionResult put(EmpClass ecobj)
        {
            cmd = new SqlCommand("update tbl_emp set firstname = '"+ecobj.firstname+"',lastname = '"+ecobj.lastname+"',address = '"+ecobj.address+"',contactno = '"+ecobj.contactno+"',state = '"+ecobj.state+"' where no = "+ecobj.no+"",con);
            con.Open();
            int updaterecord = cmd.ExecuteNonQuery();
            con.Close();
            return Ok(updaterecord);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            cmd = new SqlCommand("delete from tbl_emp where no = " + id + "", con);
            con.Open();
            int delete = cmd.ExecuteNonQuery();
            con.Close();
            return Ok(delete);
        }

        [HttpGet]
        public IHttpActionResult idemp(int id)
        {
            List<EmpClass> ec = new List<EmpClass>();
            cmd = new SqlCommand("select * from tbl_emp where no = " + id + "", con);
            con.Open();
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ec.Add(new EmpClass()
                {
                    no = Convert.ToInt32(sdr.GetValue(0)),
                    firstname = sdr.GetValue(1).ToString(),
                    lastname = sdr.GetValue(2).ToString(),
                    address = sdr.GetValue(3).ToString(),
                    contactno = sdr.GetValue(4).ToString(),
                    state = sdr.GetValue(5).ToString()
                });
            }
            con.Close();

            return Ok(ec);
        }
        /*public IHttpActionResult getempid(EmpClass empobj)
        {
            webapiexampleEntities db = new webapiexampleEntities();
            EmpClass ec = null;
        
            ec = db.tbl_emp.Where(x => x.no == empobj.no).Select(x => new EmpClass()
            {
                no = Convert.ToInt32(x.no),
                firstname = x.firstname,
                lastname = x.lastname,
                address = x.address,
                contactno = x.contactno,
                state = x.state,
            }).FirstOrDefault<EmpClass>();

            if(ec == null)
            {
                return NotFound();
            }

            return Ok(ec);
        }*/
    }
}
