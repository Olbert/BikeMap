using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GisButtons.Models
{
    public partial class City
    {
        public long Id { get; set; }

        [Display(Name = "Город")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Население")]
        [DataType(DataType.Text)]
        public long People { get; set; }

        [Display(Name = "Координаты")]
        [DataType(DataType.Text)]
        public string Coord { get; set; }

        [Display(Name = "Фото")]
        [DataType(DataType.Url)]
        public string Photo { get; set; }

        [Display(Name = "Видео")]
        [DataType(DataType.Url)]
        public string Video { get; set; }
    }
}
