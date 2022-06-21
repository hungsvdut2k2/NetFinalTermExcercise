﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class CourseClassroom_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static CourseClassroom_BLL _instance;

        public static CourseClassroom_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseClassroom_BLL();
                }

                return _instance;
            }
            private set { }
        }

        public List<ReturnedCourseClassroom> GetAllClassroomOfCourse(string courseId)
        {
            List<ReturnedCourseClassroom> resList = new List<ReturnedCourseClassroom>();
            List<CourseClassroom> courseClassrooms = _context.CourseClassroom.Where(courseClass => courseClass.courseId == courseId && courseClass.IsComplete == false).ToList();
            foreach (var courseClass in courseClassrooms)
            {
                var item = new ReturnedCourseClassroom();
                List<Schedule> schedules = _context.Schedule.Where(w => w.CourseClassId == courseClass.CourseClassId).ToList();
                foreach (var schedule in schedules)
                {
                    item.Schedule +=
                        $"{schedule.ConvertNumberToDate()} : {schedule.StartPeriod} - {schedule.EndPeriod}  " ;
                }

                item.CourseClassId = courseClass.CourseClassId;
                item.TeacherName = courseClass.TeacherName;
                resList.Add(item);
            }
            return resList;
        }

        public void AddCourseClass(CourseClassroom newCourseClass)
        {
            try
            {
                _context.CourseClassroom.Add(newCourseClass);
                _context.SaveChanges();
            }
            catch (DbException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteCourseClass(string courseClassId)
        {
            CourseClassroom courseClassroom = _context.CourseClassroom.Find(courseClassId);
            _context.CourseClassroom.Remove(courseClassroom);
            _context.SaveChanges();
        }

        public string GetCourseIdByCouresClassId(string courseClassId)
        {
            CourseClassroom courseClassroom = _context.CourseClassroom.Where(courseClass => courseClass.CourseClassId == courseClassId).FirstOrDefault();
            return courseClassroom.courseId;
        }

        public List<ReturnedCourseClassroom> GetAllTeacherCourseClassrooms(string teacherName)
        {
            List<CourseClassroom> courseClassroomList = _context.CourseClassroom
                .Where(courseClass => courseClass.TeacherName == teacherName && courseClass.IsComplete == false).ToList();
            List<ReturnedCourseClassroom> resList = new List<ReturnedCourseClassroom>();
            foreach (var courseClass in courseClassroomList)
            {
                var item = new ReturnedCourseClassroom();
                List<Schedule> schedules = _context.Schedule.Where(w => w.CourseClassId == courseClass.CourseClassId).ToList();
                foreach (var schedule in schedules)
                {
                    item.Schedule +=
                        $"{schedule.ConvertNumberToDate()} : {schedule.StartPeriod} - {schedule.EndPeriod}  ";
                }

                item.CourseClassId = courseClass.CourseClassId;
                item.TeacherName = courseClass.TeacherName;
                resList.Add(item);
            }
            return resList;
        }

        public List<ComboboxItem> GetComboboxItems(string teacherName)
        {
            List<CourseClassroom> courseClassroomList = _context.CourseClassroom
                .Where(courseClass => courseClass.TeacherName == teacherName && courseClass.IsComplete == false).ToList();
            List<ComboboxItem> resList = new List<ComboboxItem>();
            foreach (var courseClass in courseClassroomList)
            {
                var item = new ComboboxItem
                {
                    Text = courseClass.CourseClassId,
                    Value = courseClass.CourseClassId
                };
                resList.Add(item);
            }

            return resList;
        }

        public bool CheckConflictCourseClassroom(string userId, string courseClassId)
        {
            List<UserCourseClassroom> userCourseClassrooms =
                _context.UserCourseClassroom.Where(w => w.UserId == userId).ToList();
            CourseClassroom findinCourseClassroom = _context.CourseClassroom.Find(courseClassId);
            List<CourseClassroom> courseClassrooms = new List<CourseClassroom>();
            foreach (var item in userCourseClassrooms)
            {
                CourseClassroom temp = _context.CourseClassroom.Find(item.CourseClassroomId);
                courseClassrooms.Add(temp);
            }

            foreach (var courseClassroom in courseClassrooms)
            {
                if (findinCourseClassroom.courseId == courseClassroom.courseId)
                {
                    return false;
                }
            }
            return true;
        }
        public void AddStudentToCourseClassroom(UserCourseClassroom userCourseClassroom)
        {
            _context.UserCourseClassroom.Add(userCourseClassroom);
            _context.SaveChanges();
        }
    }
}
