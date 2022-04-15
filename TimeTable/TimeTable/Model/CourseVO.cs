using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseVO
    {
        private string number;         //순번
        private string department;     //학과
        private string classNumber;    //학수번호
        private string distribution;  //분반
        private string courseTitle;    //교과목명
        private string language;       //강의언어
        private string completionType; //이수구분
        private string credit;         //학점
        private string grade;          //학년
        private string instructor;     //교수명
        private string classDay;       //요일
        private string lectureRoom;    //강의실

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string ClassNumber
        {
            get { return classNumber; }
            set { classNumber = value; }
        }
        public string Distribution
        {
            get { return distribution; }
            set { distribution = value; }
        }
        public string CourseTitle
        {
            get { return courseTitle; }
            set { courseTitle = value; }
        }
        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        public string CompletionType
        {
            get { return completionType; }
            set { completionType = value; }
        }
        public string Credit
        {
            get { return credit; }
            set { credit = value; }
        }
        public string Grade
        {
            get { return grade; }
            set { grade = value; }
        }
        public string Instructor
        {
            get { return instructor; }
            set { instructor = value; }
        }
        public string ClassDay
        {
            get { return classDay; }
            set { classDay = value; }
        }
        public string LectureRoom
        {
            get { return lectureRoom; }
            set { lectureRoom = value; }
        }
    }
}
