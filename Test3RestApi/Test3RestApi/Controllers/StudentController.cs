using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Test3RestApi.Models;

namespace Test3RestApi.Controllers
{

    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        string xmlFile = "../Test3RestApi/data/StudentList.xml";

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            XDocument xdoc = XDocument.Load(xmlFile);
            var students = xdoc.Root.Descendants("Student").Select(x => new Student
            {
                Id = x.Attribute("Id").Value,
                Fullname = x.Element("Fullname").Value,
                Nationality = x.Element("Nationality").Value,
            }).ToList();
            return Ok(students);
        }

        //POST api/students
        [HttpPost]
        public ActionResult<Student> AddStudent([FromBody] Student student)
        {
            student.Id = Guid.NewGuid().ToString("N");
            XDocument xdoc = XDocument.Load(xmlFile);
            var xelement = new XElement("Student", new XAttribute("Id", student.Id),
                new XElement("Fullname", student.Fullname), new XElement("Nationality", student.Nationality));
            xdoc.Root.Add(xelement);
            xdoc.Save(xmlFile);
            return Ok(student);
        }

        //DELETE api/students/4
        [HttpDelete("{id}")]
        public ActionResult RemoveStudent(string id)
        {
            var xdoc = XDocument.Load(xmlFile);
            var selectedStudent = xdoc.Root.Descendants("Student").FirstOrDefault(x => x.Attribute("Id").Value == id);
            if (selectedStudent != null)
            {
                selectedStudent.Remove();
                xdoc.Save(xmlFile);
            }                
            return Ok("Record Deleted");
        }
    }
}
