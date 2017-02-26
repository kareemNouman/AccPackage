using Account.Core;
using Account.Core.Common;
using Account.Core.Framework.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Account.Data
{
    
    [DbConfigurationType(typeof(DbContextConfiguration))]
    public class AccountDbContext : DbContext
    {

        private readonly IWorkContext _workContext;

        private AuditTrailFactory auditFactory;
        private List<DbEntityEntry> auditList = new List<DbEntityEntry>();
        private List<DBAudit> list = new List<DBAudit>();


        public AccountDbContext(IWorkContext workContext)
            : base("AccountPackage")
        {
            this._workContext = workContext;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
               type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            //auditList.Clear();
            //list.Clear();
            //auditFactory = new AuditTrailFactory(this);
            //var entityList = ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
            //foreach (var entity in entityList)
            //{
            //    DBAudit audit = auditFactory.GetAudit(entity, _workContext.UserId, _workContext.Role);
            //    auditList.Add(entity);
            //    list.Add(audit);
            //}

            var retVal = base.SaveChanges();
            if (auditList.Count > 0)
            {
                int i = 0;
                foreach (var audit in list)
                {
                    audit.PrimaryKeyValue = auditFactory.GetKeyValue(auditList[i]).Value;
                    this.Set<DBAudit>().Add(audit);
                    i++;
                }
                base.SaveChanges();
            }

            return retVal;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }


    class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }


    public class AuditTrailFactory
    {
        private DbContext context;

        public AuditTrailFactory(DbContext context)
        {
            this.context = context;
        }
        public DBAudit GetAudit(DbEntityEntry entry, Int64 userid, AccountRole role)
        {
            DBAudit audit = new DBAudit();
            audit.UserId = userid;
            audit.Role = role;
            audit.TableName = GetTableName(entry);
            audit.ActionDate = DateTime.Now;

            //entry is Added 
            if (entry.State == EntityState.Added)
            {
                audit.NewData = SetAddedProperties(entry);
                audit.Actions = AuditActions.Insert;
            }
            //entry in deleted
            else if (entry.State == EntityState.Deleted)
            {
                audit.OldData = SetDeletedProperties(entry);
                audit.Actions = AuditActions.Delete;
            }
            //entry is modified
            else if (entry.State == EntityState.Modified)
            {
                var oldValues = "";
                var newValues = "";
                SetModifiedProperties(entry, ref oldValues, ref newValues);
                audit.OldData = oldValues;
                audit.NewData = newValues;
                audit.Actions = AuditActions.Update;
            }

            return audit;
        }

        private void SetAddedProperties(DbEntityEntry entry, StringBuilder newData)
        {
            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                var newVal = entry.CurrentValues[propertyName];
                if (newVal != null)
                {
                    newData.AppendFormat("{0}={1} || ", propertyName, newVal);
                }
            }
            if (newData.Length > 0)
                newData = newData.Remove(newData.Length - 3, 3);
        }

        private string SetAddedProperties(DbEntityEntry entry)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                var hasignorePorperty = Attribute.IsDefined(entry.Entity.GetType().GetProperty(propertyName), typeof(IgnoreAudit));
                if (hasignorePorperty)
                    continue;

                var newVal = entry.CurrentValues[propertyName];
                if (newVal != null)
                    values.Add(propertyName, newVal);
            }

            return Serialize(values);
        }

        private void SetDeletedProperties(DbEntityEntry entry, StringBuilder oldData)
        {
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in dbValues.PropertyNames)
            {
                var oldVal = dbValues[propertyName];
                if (oldVal != null)
                {
                    oldData.AppendFormat("{0}={1} || ", propertyName, oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData = oldData.Remove(oldData.Length - 3, 3);
        }

        private string SetDeletedProperties(DbEntityEntry entry)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in dbValues.PropertyNames)
            {
                var hasignorePorperty = Attribute.IsDefined(entry.Entity.GetType().GetProperty(propertyName), typeof(IgnoreAudit));
                if (hasignorePorperty)
                    continue;

                var oldVal = dbValues[propertyName];
                if (oldVal != null)
                    values.Add(propertyName, oldVal);
            }

            return Serialize(values);
        }

        private void SetModifiedProperties(DbEntityEntry entry, StringBuilder oldData, StringBuilder newData)
        {

            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in entry.OriginalValues.PropertyNames)
            {
                var oldVal = dbValues[propertyName];
                var newVal = entry.CurrentValues[propertyName];
                if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
                {
                    newData.AppendFormat("{0}={1} || ", propertyName, newVal);
                    oldData.AppendFormat("{0}={1} || ", propertyName, oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData = oldData.Remove(oldData.Length - 3, 3);
            if (newData.Length > 0)
                newData = newData.Remove(newData.Length - 3, 3);

        }

        private void SetModifiedProperties(DbEntityEntry entry, ref string olddata, ref string newdata)
        {
            Dictionary<string, object> newValues = new Dictionary<string, object>();
            Dictionary<string, object> oldValues = new Dictionary<string, object>();
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in entry.OriginalValues.PropertyNames)
            {
                var hasignorePorperty = Attribute.IsDefined(entry.Entity.GetType().GetProperty(propertyName), typeof(IgnoreAudit));
                if (hasignorePorperty)
                    continue;

                var oldVal = dbValues[propertyName];
                var newVal = entry.CurrentValues[propertyName];
                if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
                {
                    newValues.Add(propertyName, newVal);
                    oldValues.Add(propertyName, oldVal);
                }
            }

            newdata = Serialize(newValues);
            olddata = Serialize(oldValues);
        }


        private string Serialize(Dictionary<string, object> data)
        {

            XElement el = new XElement("root",
                data.Select(kv => new XElement(kv.Key, kv.Value)));

            return el.ToString();

            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Dictionary<string, object>));
            //string result = "";
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    serializer.WriteObject(ms, data);
            //    result = Encoding.Default.GetString(ms.ToArray());
            //}
            //return result;
        }

        public long? GetKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            long id = 0;
            if (objectStateEntry.EntityKey.EntityKeyValues != null)
                id = Convert.ToInt64(objectStateEntry.EntityKey.EntityKeyValues[0].Value);

            return id;
        }

        //private string GetTableName(DbEntityEntry dbEntry)
        //{
        //    TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
        //    string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().BaseType.Name;
        //    return tableName;
        //}

        private string GetTableName(DbEntityEntry ent)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            Type entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;

            string entityTypeName = entityType.Name;

            EntityContainer container =
                objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == entityTypeName
                                    select meta.Name).First();
            return entitySetName;
        }
    }

}
