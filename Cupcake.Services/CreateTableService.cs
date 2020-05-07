using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Services
{
    public class CreateTableService //: BaseService<CreateTable>
    {
        public ReturnValue Create(CreateTable model)
        {
            ReturnValue revalue = new ReturnValue();
            try
            {
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.AppendFormat("if exists (select * from sysobjects where name='{0}') drop table {0}", model.TableName);
                sqlstr.AppendFormat(" create table {0}( Id uniqueidentifier primary key,", model.TableName);
                for (int i = 0; i < model.ColumnModels.Count; i++)
                {
                    if (i > 0)
                    {
                        sqlstr.Append(",");
                    }
                    sqlstr.Append(GetCompleteType(model.ColumnModels[i]));
                }
                sqlstr.AppendFormat(")");

                SQLHelper.ExecuteCommand(sqlstr.ToString());
                revalue.IsSuccess = true;
            }
            catch(Exception ex)
            {
                revalue.IsSuccess = false;
                revalue.Message = ex.Message;
            }

            return revalue;
        }

        public string GetCompleteType(ColumnModel model)
        {
            string restr = null;
            restr = "[" + model.ColumnName + "]" + " ";
            restr += model.ColumnType;//EnumHelper.GetEnumDescription(model.ColumnType);
            if (model.ColumnType == ColumnType.NVarchar)
            {
                if (model.ColumnLength > 0)
                {
                    restr += string.Format(" ({0}) ", model.ColumnLength);
                }
                else
                {
                    restr += string.Format(" ({0}) ", 50);
                }
            }
            if (model.ColumnType == ColumnType.NChar)
            {
                if (model.ColumnLength > 0)
                {
                    restr += string.Format(" ({0}) ", model.ColumnLength);
                }
                else
                {
                    restr += string.Format(" ({0}) ", 10);
                }
            }
            
            return restr;
        }
        
    }

}
