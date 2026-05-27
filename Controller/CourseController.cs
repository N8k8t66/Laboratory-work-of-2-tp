using Laboratorywork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Laboratorywork.Controllers
{
    public class CourseController : Controller
    {
        private static List<Course> _courses = new List<Course>();
        private static int _nextId = 1;

        public MvcHtmlString InternalCourseList(List<Course> courses, string title = "Мои курсы")
        {
            var html = new StringBuilder();

            html.Append($"<div class='internal-course-list' style='border: 2px solid green; padding: 15px; margin: 10px 0; border-radius: 8px;'>");
            html.Append($"<h3 style='color: green;'>{title}</h3>");
            html.Append("<ul>");

             while (i < courses.Count)
            {
                html.Append(
                    $"<li><strong>{courses[i].CourseName}</strong> — " +
                    $"{courses[i].Teacher} | " +
                    $"{courses[i].Hours} ч. | " +
                );
        
                i++;
            }

            html.Append("</ul>");
            html.Append("<p><em>Вызван ВНУТРЕННИЙ вспомогательный метод</em></p>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }

        public ActionResult Index()
        {
            TempData["UseInternalMethod"] = true;

            return View(_courses);
        }

        public ActionResult Select(int id)
        {
            var course = _courses.FirstOrDefault(b => b.Id == id);
            if (course != null)
            {
                Session["CurrentCourseId"] = id;
                TempData["Success"] = $"Выбран курс: {course.Title}";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                int? currentId = Session["CurrentCourseId"] as int?;
                if (currentId.HasValue && _courses.Any(b => b.Id == currentId.Value))
                {
                    return RedirectToAction("Details", new { id = currentId.Value });
                }

                if (_courses.Any())
                {
                    return RedirectToAction("Details", new { id = _courses.First().Id });
                }
                return RedirectToAction("Index");
            }

            var course = _courses.FirstOrDefault(b => b.Id == id);
            if (course == null)
            {
                return HttpNotFound();
            }

            Session["CurrentCourseId"] = id;
            return View(course);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                course.Id = _nextId++;
                _courses.Add(course);
                TempData["Success"] = $"Книга \"{course.Title}\" успешно добавлена!";
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                int? currentId = Session["CurrentCourseId"] as int?;
                if (currentId.HasValue)
                {
                    return RedirectToAction("Edit", new { id = currentId.Value });
                }
                return RedirectToAction("Index");
            }

            var course = _courses.FirstOrDefault(b => b.Id == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                var existing = _courses.FirstOrDefault(b => b.Id == course.Id);
                if (existing != null)
                {
                    existing.CourseName = course.CourseName;
                    existing.Teacher = course.Teacher;
                    existing.Hours = course.Hours;
                    existing.StartDate = course.StartDate;
                    existing.Price = course.Price;
                    existing.Phone = course.Phone;
                    TempData["Success"] = $"Курс \"{course.Title}\" успешно обновлена!";
                }
                return RedirectToAction("Index");
            }
            return View(course);
        }
    }
}
