﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class UserCourseClassroom
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public CourseClassroom courseClassroom { get; set; }
        public string CourseClassroomId { get; set; }
        public int Semester { get; set; }
    }
}
