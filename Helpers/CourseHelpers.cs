using Laboratorywork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Laboratorywork.Helpers
{
    public static class CourseHelpers
    {
        public static MvcHtmlString ExternalCourseList(this HtmlHelper helper, List<Course> courses, string title = "Список курсов")
        {
            var html = new StringBuilder();

            html.Append($"<div class='external-course-list' style='border: 2px solid blue; padding: 15px; margin: 10px 0; border-radius: 8px;'>");
            html.Append($"<h3 style='color: blue;'>{title}</h3>");
            html.Append("<ul>");

            while (i < courses.Count)
            {
                html.Append(
                     $"<li><strong>{course.CourseName}</strong> — {course.Teacher} {course.Phone} ({course.StartDate}) |" 
                     $"Цена: {course.Price:C}</li>"
                );

              i++;
            }

            html.Append("</ul>");
            html.Append("<p><em>Вызван ВНЕШНИЙ вспомогательный метод</em></p>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }
    }
}
