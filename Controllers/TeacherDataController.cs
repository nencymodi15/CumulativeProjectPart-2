using CumulativeProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace CumulativeProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private ProjectDbContext project = new ProjectDbContext();


        /// <summary>
        /// this function uses mysql tools and connects to the databse named project, then it stores the qurey and 
        /// excutes it. this functions main aim is to read data from the database and if the serchkey is given then it will 
        /// read those value who matches to the serchkey parameter. after getting(reading) data from database will store it in the class object
        /// and then value will be added to the list of teacher class's.
        /// </summary>
        /// <param name="SerchKey">takes the parameter form user from server render page</param>
        /// <returns>the list of teachers from database.</returns>
        [HttpGet]
        [Route("api/{TeacherData}/{ListTeacher}/{SerchKey?}")]
        public IEnumerable<Teacher> ListTeacher(string SerchKey = null)
        {
            MySqlConnection Conn = project.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key)" +
                "or concat (teacherfname, ' ',teacherlname) like @key";

            cmd.Parameters.AddWithValue("@key", "%" + SerchKey + "%");

            MySqlDataReader reader = cmd.ExecuteReader();

            List<Teacher> Teacher = new List<Teacher> { };

            while (reader.Read())
            {
                string TeacherFName = (string)reader["teacherfname"];
                string TeacherLname = (string)reader["teacherlname"];
                int TeacherId = (int)reader["teacherid"];
                string Employeenumber = (string)reader["employeenumber"];
                decimal salary = (decimal)reader["salary"];
                DateTime Hiredate = (DateTime)reader["hiredate"];
                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLname;
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.hiredate = Hiredate;
                NewTeacher.Salary = salary;
                NewTeacher.EmployeeNumber = Employeenumber;

                Teacher.Add(NewTeacher);
            }

            Conn.Close();

            return Teacher;
        }
        /// <summary>
        /// this function displays the data of teachers when it is provided with a id, it will display the data of given id.
        /// </summary>
        /// <param name="id">it takes the id dynamically from server rendered page</param>
        /// <returns>gives stires the data in object of teacher class and returns object of teacher class type.</returns>
        [HttpGet]

        public Teacher FindTeacher(int id = -1)
        {
            Teacher Teacher = new Teacher();

            MySqlConnection Conn = project.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "select * from teachers where teacherid =@key";
            cmd.Parameters.AddWithValue("@key", id);


            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string TeacherFName = (string)reader["teacherfname"];
                string TeacherLname = (string)reader["teacherlname"];
                int TeacherId = (int)reader["teacherid"];
                string Employeenumber = (string)reader["employeenumber"];
                decimal salary = (decimal)reader["salary"];
                DateTime Hiredate = (DateTime)reader["hiredate"];

                Teacher.TeacherId = TeacherId;
                Teacher.TeacherFName = TeacherFName;
                Teacher.TeacherLName = TeacherLname;
                Teacher.hiredate = Hiredate;
                Teacher.Salary = salary;
                Teacher.EmployeeNumber = Employeenumber;

            }

            return Teacher;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [Route("api/{TeacherData}/{DeleteTeacher}/{id}")]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = project.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewTeacher"></param>
        [HttpPost]

        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            MySqlConnection Conn = project.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into Teachers (teacherfname,teacherlname,employeenumber,hiredate,salary) values (@teacherfname,@teacherlname,@employeenumber,CURRENT_DATE(),@slary)";
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@slary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}
