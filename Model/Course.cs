using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorywork.Model
{
    public class Course : Controller
    {
        [DisplayName("Идентификатор курса")]
        public int Id { get; set; }

        [DisplayName("Название курса")]
        public string CourseName { get; set; }

        [DisplayName("Преподаватель")]
        public string Teacher { get; set; }

        [DisplayName("Количество часов")]
        public int Hours { get; set; }

        [DisplayName("Дата начала")]
        public DateTime StartDate { get; set; }

        [DisplayName("Стоимость обучения")]
        public decimal Price { get; set; }

        [DisplayName("Контактный телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
