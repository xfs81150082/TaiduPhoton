using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TumoCommon.Model;

namespace TumoPhoton.DB.Manager
{
    class ServerPropertyManager
    {  
        //
        public static ServerPropertyManager _instance;
        public ServerPropertyManager()
        {
            _instance = this;
            
        }

        //得到服务器列表
        public List<ServerProperty> GetServerList()
        {
            using (var session = NHibernateHelper.OpenSession()) 
            {
                using (var transaction = session.BeginTransaction())
                {
                    var list = session.QueryOver<ServerProperty>();
                    transaction.Commit();
                    return (List<ServerProperty>) list.List<ServerProperty>();
                }
            }
            
        }

        //自已加的
        public void DeleteById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ServerProperty tu = new ServerProperty();
                    tu.Id = id;
                    session.Delete(tu);
                    transaction.Commit();
                }
            }
        }


        public IList<ServerProperty> GetSpByname(string spName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var spList = session.QueryOver<ServerProperty>().Where(sp => sp.Name == spName);
                    transaction.Commit();
                    return spList.List();
                }
            }

        }

        //私下自己加的
        public void SaveSp(ServerProperty sp)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(sp);
                    transaction.Commit();
                }
            }
        }

        public void DeleteSpById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ServerProperty tu = new ServerProperty();
                    tu.Id = id;
                    session.Delete(tu);
                    transaction.Commit();
                }
            }

        }

        public void UpdateSp(ServerProperty sp)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(sp);
                    transaction.Commit();
                }
            }

        }




    }
}
