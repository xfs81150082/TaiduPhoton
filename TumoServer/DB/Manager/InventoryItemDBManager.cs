using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TumoCommon.Model;

namespace TumoPhoton.DB.Manager
{
    public class InventoryItemDBManager
    {
        public List<InventoryItemDB> GetInventoryItemDB(Role role)
        {
            using (var session=NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var res = session.QueryOver<InventoryItemDB>().Where(x => x.Role == role);
                    transaction.Commit();
                    return (List<InventoryItemDB>) res.List<InventoryItemDB>();
                }
            }
        }

        public void AddInventoryItemDB(InventoryItemDB itemDb)
        {
            using (var session = NHibernateHelper.OpenSession()) 
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(itemDb);
                    transaction.Commit();
                }
            }
        }

        public void UpdateInventoryItemDB(InventoryItemDB itemDb2)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(itemDb2);
                    transaction.Commit();
                }
            }
        }

        public void UpdateInventoryItemDBList(List<InventoryItemDB> list)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var itemDb in list)
                    {
                        session.Update(itemDb);
                    }
                    transaction.Commit();
                }
            }
        }

        public void UpgradeEquip(InventoryItemDB itemDb4, Role role)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(itemDb4);
                    session.Update(role);
                    transaction.Commit();
                }
            }
        }
    }
}
