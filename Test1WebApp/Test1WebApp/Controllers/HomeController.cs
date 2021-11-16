using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Test1WebApp.Models;

namespace Test1WebApp.Controllers
{
    public class HomeController : Controller
    {
        string xmlFile = "../Test1WebApp/Data/User.xml";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            XDocument xdoc = XDocument.Load(xmlFile);
            var users = xdoc.Root.Descendants("User").Select(x => new User
            {
                Id = x.Attribute("Id").Value,
                Username = x.Element("Username").Value,
                Surname = x.Element("Surname").Value,
                CellphoneNumber = x.Element("CellphoneNumber").Value,
            }).ToList();
            return View(users);
        }

        //GET create action
        public IActionResult Create()
        {
            return View();
        }

        //POST create action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            ////save user
            if (ModelState.IsValid)
            {
                XDocument xdoc = XDocument.Load(xmlFile);
                var existingUser = xdoc.Root.Descendants("User").FirstOrDefault(x => x.Element("Username").Value == user.Username);
                //Assuming that username must be Unique
                if (existingUser == null)
                {
                    user.Id = Guid.NewGuid().ToString("N");
                    var xelement = new XElement("User", new XAttribute("Id", user.Id),
                        new XElement("Username", user.Username),
                        new XElement("Surname", user.Surname), new XElement("CellphoneNumber", user.CellphoneNumber));
                    xdoc.Root.Add(xelement);
                    xdoc.Save(xmlFile);
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = $"Username {existingUser.Element("Username").Value} already exists!";
            }
            return View(user);
        }

        //Get delete
        public IActionResult Delete(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            XDocument xdoc = XDocument.Load(xmlFile);
            var selectedUser = xdoc.Root.Descendants("User").FirstOrDefault(x => x.Attribute("Id").Value == Id);
            if (selectedUser == null)
            {
                return NotFound();
            }
            return View(new User
            {
                Id = selectedUser.Attribute("Id").Value,
                Username = selectedUser.Element("Username").Value,
                Surname = selectedUser.Element("Surname").Value,
                CellphoneNumber = selectedUser.Element("CellphoneNumber").Value
            });
        }

        //Get update
        public IActionResult Update(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            XDocument xdoc = XDocument.Load(xmlFile);
            var selectedUser = xdoc.Root.Descendants("User").FirstOrDefault(x => x.Attribute("Id").Value == Id);
            if (selectedUser == null)
            {
                return NotFound();
            }
            return View(new User
            {
                Id = selectedUser.Attribute("Id").Value,
                Username = selectedUser.Element("Username").Value,
                Surname = selectedUser.Element("Surname").Value,
                CellphoneNumber = selectedUser.Element("CellphoneNumber").Value
            });
        }

        //POST update action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(User user)
        {
            ////save user
            if (ModelState.IsValid)
            {
                XDocument xdoc = XDocument.Load(xmlFile);
                var duplicatedUser = xdoc.Root.Descendants("User").FirstOrDefault(x => x.Element("Username").Value == user.Username && x.Attribute("Id").Value != user.Id);
                //Assuming that username must be Unique
                if (duplicatedUser == null)
                {
                    var userToUpdate = xdoc.Root.Descendants("User").FirstOrDefault(x =>  x.Attribute("Id").Value == user.Id);
                    userToUpdate.Element("Username").Value = user.Username;
                    userToUpdate.Element("Surname").Value = user.Surname;
                    userToUpdate.Element("CellphoneNumber").Value = user.CellphoneNumber;
                    xdoc.Save(xmlFile);
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = $"Username {duplicatedUser.Element("Username").Value} already exists!";
            }
            return View(user);
        }

        //POST delete action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string? Id)
        {
            XDocument xdoc = XDocument.Load(xmlFile);
            var selectedUser = xdoc.Root.Descendants("User").FirstOrDefault(x => x.Attribute("Id").Value == Id);
            if (selectedUser == null)
            {
                return NotFound();
            }
            selectedUser.Remove();
            xdoc.Save(xmlFile);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
