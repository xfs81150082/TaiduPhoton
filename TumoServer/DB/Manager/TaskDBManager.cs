﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TumoCommon.Model;

namespace TumoPhoton.DB.Manager
{
    public class TaskDBManager
    {
        public List<TaskDB> GeTaskDbList(Role role)
        {
            using (var session=NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var res = session.QueryOver<TaskDB>().Where(x => x.Role == role);
                    transaction.Commit();
                    return (List<TaskDB>) res.List();
                }
            }
        }

        public void AddTaskDB(TaskDB taskDB)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tansation = session.BeginTransaction())
                {
                    taskDB.LastUpdateTime = new DateTime();
                    session.Save(taskDB);
                    tansation.Commit();
                }
            }
        }

        public void UpdateTaskDB(TaskDB taskDB)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tansation = session.BeginTransaction())
                {
                    taskDB.LastUpdateTime = new DateTime();
                    session.Update(taskDB);
                    tansation.Commit();
                }
            }
        }



    }
}
