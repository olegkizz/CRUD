using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace IdentityNLayer.BLL.Services
{
    public class StudentToGroupActionService : IStudentToGroupActionService
    {
        public IUnitOfWork Db { get; set; }

        public StudentToGroupActionService(IUnitOfWork db)
        {
            Db = db;
        }
        public IEnumerable<StudentToGroupAction> GetAll()
        {
            return Db.StudentToGroupActions.GetAll();
        }
       /* public StudentToGroupAction GetById(int id)
        {
            return Db.StudentToGroupActions.Get(id);
        }

        public void Create(StudentToGroupAction studentaction)
        {
            Db.StudentToGroupActions.Create(studentaction);
            Db.Save();
        }

        public void Update(StudentToGroupAction entity)
        {
            Db.StudentToGroupActions.Update(entity);
            Db.Save();
        }

        public void Delete(int id)
        {
            Db.Students.Delete(id);
            Db.Save();
        }*/
        public void Apply(int studentId, int GroupId)
        {
            Db.StudentToGroupActions.Create(new StudentToGroupAction()
            {
                StudentId = studentId,
                GroupId = GroupId,
                Action = UserGroupStates.Applied,
                Date = DateTime.Now
            });
        }
        public void Decline(int studentId, int GroupId)
        {
            /*  ActionsStudentGroup currentState = GetCurrentState(studentId, GroupId);
              if (currentState == ActionsStudentGroup.Requested)
                  currentState = ActionsStudentGroup.Declined;
              else if (currentState != ActionsStudentGroup.Declined && currentState != ActionsStudentGroup.Aborted)
              {
                  currentState = ActionsStudentGroup.Aborted;

                  Db.StudentToGroupActions.Create(new StudentToGroupAction()
                  {
                      StudentId = studentId,
                      GroupId = GroupId,
                      Action = currentState,
                      Date = DateTime.Now
                  });
              }*/
            throw new NotImplementedException();
        }
        public void Request(int studentId, int GroupId)
        {
            Db.StudentToGroupActions.Create(new StudentToGroupAction()
            {
                StudentId = studentId,
                GroupId = GroupId,
                Action = UserGroupStates.Requested,
                Date = DateTime.Now
            });
        }
        public void Grade(int studentId, int GroupId, Grade grade)
        {

        }
   /*     public ActionsStudentGroup GetCurrentState(int studentId, int GroupId)
        {
            return ((List<StudentToGroupAction>)Db.StudentToGroupActions.Find(
                    ac => ac.StudentId == studentId && ac.GroupId == GroupId)).FindLast(
                ac => ac.StudentId == studentId && ac.GroupId == GroupId).Action;
        }*/
    }
}
