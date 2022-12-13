using CumulativeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace CumulativeProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// creates object of teacherDatacontroller class and access the value of teacher.
        /// </summary>
        /// <param name="serchkey">it takes the value from dynamically from form element in list.cshtml page.</param>
        /// <returns>returns teachers maching with serch and if not provided the serch key it will display whole list of teachers vailable in the database</returns>
        public ActionResult List(string serchkey = null)
        {

            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> teachers = controller.ListTeacher(serchkey);


            return View(teachers);
        }
        /// <summary>
        /// it itakes the id value provided from ancor tag in the list.cshtml page and displyaes the data based on that. it takes
        /// the id and sends it to findteacher function using teacherdata controler which provides the data of teacher and it now returns to the 
        /// show.cshtml page.
        /// </summary>
        /// <param name="id">teaks id of teacher</param>
        /// <returns>data of the teacher based on id.</returns>
        public ActionResult Show(int id = -1)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        /// <summary>
        /// this function takes input of id and show the info of the teacher 
        /// the user wants to delete
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>teachers's data to serverrendered page</returns>

        public ActionResult ConfirmDelete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //Post : /Teacher/Delete/{id}
        /// <summary>
        /// takes the id as input and pass it through to Teachers data camtroller to delete 
        /// perticular teacher from database
        /// </summary>
        /// <param name="id">takes int id from server page</param>
        /// <returns>redired to list page to display list of teachers</returns>
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        public ActionResult New()
        {

            return View();
        }
        /// <summary>
        /// this function takes the inputs from the serverer rendered page and pass the 
        /// data to the teachers controller page.
        /// </summary>
        /// <param name="TeacherFName">Teacher First name thorugh the input feild</param>
        /// <param name="TeacherLName">Teacher lasr name thorugh the input feild</param>
        /// <param name="EmployeeNumber">Teacher employeenumber thorugh the input feild</param>
        /// <param name="Salary">Teacher's salary thorugh the input feild</param>
        /// <returns>redirect page to list page to show the new added teacher</returns>
        public ActionResult Create(string TeacherFName, string TeacherLName, string EmployeeNumber, decimal Salary)
        {

            Debug.WriteLine("this page is good");
            Debug.WriteLine(TeacherFName);
            Debug.WriteLine(TeacherLName);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        /// <summary>
        /// this method takes the id from the page and pass it to the find teacher method 
        /// to get the info of the teacher which user wants to u[padate
        /// </summary>
        /// <param name="id">takes id as input</param>
        /// <returns>data of the teacher from dfatabase</returns>
        // GET : /Teacher/Update/{id}
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        /// <summary>
        /// this function fetch the values from the server rendered page
        /// </summary>
        /// <param name="id">Teacher id</param>
        /// <param name="TeacherFName">teacher name</param>
        /// <param name="TeacherLName">teacher last name</param>
        /// <param name="EmployeeNumber">teacher Emplyoyee Number</param>
        /// <param name="Salary">Teacher Salary</param>
        /// <returns>the page back to the show method to show the updates that has been made</returns>
        [HttpPost]

        public ActionResult Update(int id, string TeacherFName, string TeacherLName, string EmployeeNumber, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();

            TeacherInfo.TeacherFName = TeacherFName;
            TeacherInfo.TeacherLName = TeacherLName;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id,TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}