using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cupcake.Web.WapTemplate.edmx;
using System.Data.SqlClient;
using Cupcake.Web.WapTemplate.Models;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceComments_Praise
    {
        //点赞新增一条用户记录/删除
        public int PraiseAdd(Comments_Praise model) {
            using (CupcakeEntities db=new CupcakeEntities())
            {
                bool  iswhere = db.Comments_Praise.Where(m => m.UserId == model.UserId && m.ChildId == model.ChildId).Count()>0;
                int ischoose = 0;
                if (iswhere == false)
                {
                    //新增
                    Comments_Praise comp = new Comments_Praise();
                    comp.Id = Guid.NewGuid();
                    comp.UserId = model.UserId;
                    comp.ChildId = model.ChildId;
                    comp.IsDelete = false;
                    comp.CreateDate = DateTime.Now;
                    comp.UpdateDate = DateTime.Now;
                    db.Comments_Praise.Add(comp);
                    ischoose = 1;
                }
                else {
                    //删除
                    var info = db.Comments_Praise.Where(m => m.UserId == model.UserId && m.ChildId == model.ChildId).SingleOrDefault();
                    db.Comments_Praise.Remove(info);
                    ischoose = 2;
               }
                int result = 0;
                db.Configuration.ValidateOnSaveEnabled = false;
                if (db.SaveChanges() > 0) 
                {
                    //判断是删除还是新增过程
                    // 1 新增   
                    // 2 删除
                    if (ischoose == 1)
                    {
                        result = 1;
                    }
                    else {
                        result = 2;
                    }
                }
                return result;
            }
        
        }
    }
}