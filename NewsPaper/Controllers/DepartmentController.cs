using Microsoft.AspNetCore.Mvc;
using NewsPaper.Models.Context;
using NewsPaper.Repository.Department;
using NewsPaper.Models.Entities;
using System.ComponentModel.DataAnnotations;
namespace NewsPaper.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository deptRepository;
        NewspaperContext context;
        public DepartmentController(IDepartmentRepository deptRepository, NewspaperContext context)
        {
            this.deptRepository = deptRepository;
            this.context = context;
        }
        public IActionResult GetAll()
        {
            var allDepartments = deptRepository.GetAll();
            ViewBag.department = allDepartments;
            Department dept = new Department();
            return View("GetAll", dept);
        }

        public IActionResult EditDepartment(int id) 
        {
            var dept = deptRepository.GetById(id);
            ViewBag.department = dept;
            //ViewBag.department.authors = dept.authors.ToList();
            return View("EditDepartment", dept);
        }

        [HttpPost]
        public IActionResult SaveDepartment(Department newDept, int id) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    deptRepository.Update(newDept, id);
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex) 
            { 
                ModelState.AddModelError("", ex.Message);
            }
            var dept = context.departments.FirstOrDefault(d => d.Id == id);
            return RedirectToAction("UpdateDepartment", dept);
            
        }

        [HttpPost]
        public IActionResult AddDepartment(Department dept)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    deptRepository.add(dept);
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);    
            }
            return RedirectToAction("GetAll");
        }

        public IActionResult DeleteDepartment(int id)
        { 
            deptRepository.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult UpdateDepartment(int id)
        {
            //deptRepository.Update(dept, id);
            var dept = context.departments.FirstOrDefault(d => d.Id == id);
            return View("UpdateDepartment", dept);
        }
    }
}
