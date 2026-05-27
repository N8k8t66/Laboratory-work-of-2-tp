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
        // ==================== ЧАСТЬ II ====================
        // Хранилище книг в памяти (List<T> - вариант 2)
        private static List<Course> _courses = new List<Course>();
        private static int _nextId = 1;

        // ==================== ЧАСТЬ III ====================
        // ВНУТРЕННИЙ вспомогательный метод (параметризованный)
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

        // ЧАСТЬ II: просмотр всех данных
        // ЧАСТЬ III: передача логического значения через TempData (вариант 2)
        public ActionResult Index()
        {
            // ЧАСТЬ III: TempData для выбора метода
            TempData["UseInternalMethod"] = true;  // true - внутренний, false - внешний

            return View(_courses);
        }

        // ЧАСТЬ II: сохранение текущего экземпляра через Session
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

        // ЧАСТЬ I: просмотр одной книги
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

        // ЧАСТЬ I: форма добавления
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create - сохранение новой книги
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

        // ЧАСТЬ I: форма редактирования
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

        // POST: Book/Edit - сохранение изменений
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
