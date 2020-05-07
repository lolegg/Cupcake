using Cupcake.Core.Domain;
using Cupcake.Core;
using System;
using System.Data.Entity;
using Cupcake.Data.Mappings;
using System.Configuration;

namespace Cupcake.Data
{
    /// <summary>
    /// 数据上下文类
    /// </summary>
    public sealed class DbContextManager : DbContext
    {
        //private static readonly DbContextManager instance = new DbContextManager();
        //public static DbContextManager Instance { get { return instance; } }
        public static DbContextManager Instance { get { return ContextHelper<DbContextManager>.GetCurrentContext(); } }
        public DbSet<User> Users
        {
            get
            {
                return this.Set<User>();
            }
        }
        public DbSet<Menu> Menus
        {
            get
            {
                return this.Set<Menu>();
            }
        }
        public DbSet<DataDictionary> DataDictionarys
        {
            get
            {
                return this.Set<DataDictionary>();
            }
        }


        //public DbSet<MediaInfo> MediaType
        //{
        //    get {
        //        return this.Set<MediaInfo>();
        //    }
        //}

        /// <summary>
        /// Database初始化
        /// </summary>
        public DbContextManager(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //string newdb = ConfigurationManager.AppSettings["CreateDatabaseIfNotExists"].ToLower();
            //if (newdb == "true")
            //{
            //    Database.SetInitializer<DbContextManager>(new InitializeDataForCreateDatabaseIfNotExists());
            //}
            //else
            //{
            //    Database.SetInitializer<DbContextManager>(null);
            //}
        }
        public DbContextManager()
        {
            string newdb = ConfigurationManager.AppSettings["CreateDatabaseIfNotExists"].ToLower();
            if (newdb == "true")
            {
                Database.SetInitializer<DbContextManager>(new InitializeDataForCreateDatabaseIfNotExists());
            }
            else
            {
                Database.SetInitializer<DbContextManager>(null);
            }
        }

        /// <summary>
        /// 当模型生成时
        /// </summary>
        /// <param name="modelBuilder">重载方法</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new CustomerMapping());
            var mappings = GetType().Assembly.GetInheritedTypes(typeof(FrameworkBaseEntityMapping<>));
            foreach (var mapping in mappings)
            {
                dynamic instance = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(instance);
            }

            base.OnModelCreating(modelBuilder);
        }


    }

    public class InitializeDataForCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<DbContextManager>
    {
        protected override void Seed(DbContextManager context)
        {
            #region Menu
            var root = new Menu()
            {
                Name = "Root",
                Sort = 0
            };
            context.Menus.Add(root);
            context.SaveChanges();

            var develop = new Menu()
            {
                Name = "开发",
                Sort = 0,
                ClassName = "fa-desktop",
                ParentId = root.Id
            };
            var setup = new Menu()
            {
                Name = "设置",
                Sort = 9999,
                ClassName = "fa-bar-chart-o",
                ParentId = root.Id
            };
            context.Menus.Add(develop);
            context.Menus.Add(setup);
            context.SaveChanges();

            context.Menus.Add(new Menu()
            {
                Name = "响应式演示",
                Sort = 5,
                Href = "~/Development/Responsive",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "数据选择",
                Sort = 10,
                Href = "/Development/Index",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "常用设置",
                Sort = 5,
                Href = "/Setting",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "菜单",
                Sort = 10,
                Href = "/Tree/DefaultView?c=Menu",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "组织",
                Sort = 15,
                Href = "/Tree/DefaultView?c=Organization",
                ParentId = setup.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "用户",
                Sort = 20,
                Href = "/User",
                ParentId = setup.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "角色",
                Sort = 25,
                Href = "/Role",
                ParentId = setup.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "权限",
                Sort = 30,
                Href = "/Tree/DefaultView?c=CustomPermissions",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "系统变量",
                Sort = 35,
                Href = "/Variables",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "数据字典",
                Sort = 40,
                Href = "/Tree/DefaultView?c=DataDictionary",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "日志",
                Sort = 45,
                Href = "/Log",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "回收站",
                Sort = 50,
                Href = "/Recycle",
                ParentId = setup.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "插件",
                Sort = 55,
                Href = "/Plugin",
                ParentId = develop.Id
            });
            context.Menus.Add(new Menu()
            {
                Name = "智能表单",
                Sort = 60,
                Href = "/SetForm",
                ParentId = develop.Id
            });
            context.SaveChanges();
            #endregion

            #region MediaType
            var rootDataDictionnary = new DataDictionary()
            {
                Name = "Root",
                Sort = 0
            };
            context.DataDictionarys.Add(rootDataDictionnary);
            context.SaveChanges();

            var mediaType = new DataDictionary()
            {
                Name = "媒体类型",
                ParentId = rootDataDictionnary.Id
            };
            context.DataDictionarys.Add(mediaType);
            context.SaveChanges();

            context.DataDictionarys.Add(new DataDictionary()
            {
                Name = "图片",
                Value = "*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.raw",
                ParentId = mediaType.Id
            });
            context.DataDictionarys.Add(new DataDictionary()
            {
                Name = "Office文件",
                Value="*.doc;*.docx;*.rtf;*.dotx;*.dot;*.xls;*.xlsx;*.xlt;*.xltx;*.ppt;*.pptx;*.pot;*.potx;*.infopathxml;*.xsn;*.vdw;*.vdx;*.vsd;*.vsdx;*.vst;*.vstx;*.mpp;*.mpt",
                ParentId = mediaType.Id
            });
            context.DataDictionarys.Add(new DataDictionary()
            {
                Name = "自定义",                
                ParentId = mediaType.Id
            });
            context.SaveChanges();
            #endregion

            context.Users.Add(new User()
            {
                UserName = "reloadsoft",
                Password = "111111",
                Name = "James Bond",
                UserType = UserType.Supper
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
