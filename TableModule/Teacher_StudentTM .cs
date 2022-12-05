using Project_VIS.Database;
using Project_VIS.Domain.DTO;
using Project_VIS.Domain.Exceptions;
//using Project_VIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_VIS.Domain.TableModule
{// TABLEMODULE prevadi DataTable na DataTableObject
    public class Teacher_StudentTM
    {
        private readonly Table_Teacher_Student _teacher_studentTDG;
        public Teacher_StudentTM()
        {
            _teacher_studentTDG = new Table_Teacher_Student();
        }

        public int Create(int student_id, int teacher_id)
        {
            return _teacher_studentTDG.Create(student_id,teacher_id);
        }

        public bool Update(int student_id,int teacher_id, bool active)
        {;
            return _teacher_studentTDG.Update(student_id,teacher_id, active);
        }
        public bool? CheckExist(int student_id, int teacher_id)
        {
            return _teacher_studentTDG.CheckExist(student_id, teacher_id);
        }
    }
}