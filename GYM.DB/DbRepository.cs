﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GYM.Core.Util;
using GYM.Model;

namespace GYM.DB
{
    public class DbRepository : DbContext
    {

        public DbRepository()
            : base("name=DbRepository")
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="entry">entry对象</param>
        private void InitObject(DbEntityEntry entry)
        {
            if (entry.Entity is BaseEntity)
            {
                var entity = entry.Entity as BaseEntity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        //初始化这些值，如果这些值为null时，自动赋值
                        if (entity.CreatedTime == new DateTime())
                            entity.CreatedTime = DateTime.Now;
                        if (entity.UpdatedTime == new DateTime())
                            entity.UpdatedTime = DateTime.Now;
                        if (string.IsNullOrEmpty(entity.ID))
                            entity.ID = Guid.NewGuid().ToString("N");
                        break;
                    case EntityState.Modified:
                        //初始化这些值，如果这些值为null时，自动赋值
                        if (entity.UpdatedTime == new DateTime())
                            entity.UpdatedTime = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        //初始化这些值，如果这些值为null时，自动赋值
                        if (entity.UpdatedTime == new DateTime())
                            entity.UpdatedTime = DateTime.Now;
                        break;
                }
            }
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AdminInvite> AdminInvite { get; set; }

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Operate> Operate { get; set; }
        
        public virtual DbSet<UserRelation> UserRelation { get; set; }
        public virtual DbSet<Relation> Relation { get; set; }
        public virtual DbSet<UserCoach> UserCoach { get; set; }
        public virtual DbSet<UserCourse> UserCourse { get; set; }
        public virtual DbSet<UserStore> UserStore { get; set; }


        public virtual DbSet<DataDictionary> DataDictionary { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<CourseCategory> CourseCategory { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Syllabus> Syllabus { get; set; }
        


        public virtual DbSet<Recharge> Recharge { get; set; }
        public virtual DbSet<RechargePlan> RechargePlan { get; set; }
        

        public override int SaveChanges()
        {
            try
            {
                var entries = from e in this.ChangeTracker.Entries()
                              where e.State != EntityState.Unchanged
                              select e;   //过滤所有修改了的实体，包括：增加 / 修改 / 删除

                foreach (var entry in entries)
                {
                    InitObject(entry);
                }

                if (entries.Count() == 0)
                    return 1;
                else
                    return base.SaveChanges();

            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                //并发冲突数据
                if (ex.GetType() == typeof(DbUpdateConcurrencyException))
                {
                    return -1;
                }
                return 0;
            }

        }
        
    }

}
