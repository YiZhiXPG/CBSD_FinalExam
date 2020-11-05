using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    public class UserController : Controller
    {
        ProblemDAL problemDAL = new ProblemDAL();

        [Authorize(Roles ="User")]
        public IActionResult Index()
        {
            List<ProblemInfo> proList = new List<ProblemInfo>();
            proList = problemDAL.GetAllProblem().ToList();
            return View(proList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] ProblemInfo objPro)
        {
            if(ModelState.IsValid)
            {
                problemDAL.AddProblem(objPro);
                return RedirectToAction("Index");
            }
            return View(objPro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id,[Bind] ProblemInfo objPro)
        {
            if(id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                problemDAL.AddProblem(objPro);
                return RedirectToAction("Index");
            }
            return View(problemDAL);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProblemInfo pro = problemDAL.GetProblemById(id);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }

        public IActionResult Delect(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProblemInfo pro = problemDAL.GetProblemById(id);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DelectPro(int? id)
        {
            problemDAL.DeleteProblem(id);
            return RedirectToAction("Index");
        }
    }
}
