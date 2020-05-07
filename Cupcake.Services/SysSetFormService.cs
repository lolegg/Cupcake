using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using Cupcake.Core.Domain;
using Cupcake.Data;
using Cupcake.Core;

namespace Cupcake.Services
{
    public class SysSetFormService : BaseService<SysSetForm>
    {
        public IList<SysSetForm> GetListByCondition(SysSetFormCondition condition, ref Paging paging)
        {
            using (var repository = new Repository<SysSetForm>())
            {
                Expression<Func<SysSetForm, bool>> where = PredicateExtensions.True<SysSetForm>();
                if (!string.IsNullOrEmpty(condition.FuntionName))
                {
                    where = where.And(o => o.FuntionName.Contains(condition.FuntionName));
                }

                if (condition.Status.HasValue)
                {
                    where = where.And(o => o.Status == condition.Status);
                }

                return repository.GetPaged(ref paging, where, m => m.CreateDate).ToList();
            }
        }

        public ReturnValue InsertData(string tablename,string[] arr)
        {
            ReturnValue revalue = new ReturnValue();

            try
            {
                StringBuilder strColumnsNames = new StringBuilder();
                StringBuilder strValues = new StringBuilder();

                if (arr.Length > 0)
                {
                    strColumnsNames.Append("(Id,");
                    strValues.AppendFormat("(newid(),");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string[] cv = arr[i].Split(':');

                        if (cv.Length > 0)
                        {
                            strColumnsNames.Append(cv[0] + ",");
                            strValues.Append("'" + cv[1] + "',");
                        }
                    }
                    strColumnsNames.Remove(strColumnsNames.Length-1, 1);
                    strValues.Remove(strValues.Length-1, 1);

                    strColumnsNames.Append(")");
                    strValues.Append(")");

                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.AppendFormat("insert into {0}", tablename);
                    sqlstr.Append(strColumnsNames);
                    sqlstr.Append(" values ");
                    sqlstr.Append(strValues);

                    SQLHelper.ExecuteCommand(sqlstr.ToString());
                    revalue.IsSuccess = true;
                }
                else
                {
                    revalue.IsSuccess = false;
                    revalue.Message = "参数有误";
                }
                
            }
            catch (Exception ex)
            {
                revalue.IsSuccess = false;
                revalue.Message = ex.Message;
            }

            return revalue;
        }

        public ReturnValue UpdateData(string tablename,string id, string[] arr)
        {
            ReturnValue revalue = new ReturnValue();

            try
            {
                StringBuilder strArgs = new StringBuilder();

                if (arr.Length > 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string[] cv = arr[i].Split(':');

                        if (cv.Length > 0)
                        {
                            strArgs.Append(cv[0] + "='" + cv[1] + "',");
                        }
                    }
                    strArgs.Remove(strArgs.Length - 1, 1);

                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.AppendFormat("update {0} set ", tablename);
                    sqlstr.Append(strArgs);
                    sqlstr.AppendFormat(" where id = '{0}' ",id);
                    
                    SQLHelper.ExecuteCommand(sqlstr.ToString());
                    revalue.IsSuccess = true;
                }
                else
                {
                    revalue.IsSuccess = false;
                    revalue.Message = "参数有误";
                }

            }
            catch (Exception ex)
            {
                revalue.IsSuccess = false;
                revalue.Message = ex.Message;
            }

            return revalue;
        }

        public string SelectData(string tablename, string columns)
        {
            try
            {
                string strSql = "select " + columns + " from " + tablename;
                DataTable dt = SQLHelper.GetDataSet(strSql);

                StringBuilder sb = new StringBuilder();

                if(dt != null && dt.Rows.Count > 0)
                {
                    //sb.Append("[");
                    string[] arr = columns.Split(',');

                    for (int i = 0; i < dt.Rows.Count; i ++ )
                    {
                        DataRow dr = dt.Rows[i];

                        //sb.Append("{");

                        for (int j = 0; j < arr.Length; j++)
                        {
                            sb.Append(dr[j] + ",");
                            //sb.Append("\""+arr[j] + "\":\"" + dr[j] + "\",");
                            //sb.Append( "\"data\":\"" + dr[j] + "\",");
                        }

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("&");
                        //sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    //sb.Append("]");
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        public string SelectData(string tablename, string columns,string id)
        {
            try
            {
                string strSql = "select " + columns + " from " + tablename + " where Id = '" + id + "'";
                DataTable dt = SQLHelper.GetDataSet(strSql);

                StringBuilder sb = new StringBuilder();

                if (dt != null && dt.Rows.Count > 0)
                {
                    //sb.Append("[");
                    string[] arr = columns.Split(',');

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        //sb.Append("{");

                        for (int j = 0; j < arr.Length; j++)
                        {
                            sb.Append(dr[j] + ",");
                            //sb.Append("\""+arr[j] + "\":\"" + dr[j] + "\",");
                            //sb.Append( "\"data\":\"" + dr[j] + "\",");
                        }

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("&");
                        //sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    //sb.Append("]");
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }


        public string SelectData(string tablename, string columns, string[] args)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("select " + columns + " from " + tablename + " where 1=1 ");

                if(args.Length > 0 && !string.IsNullOrEmpty(args[0]))
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        string[] cv = args[i].Split(':');

                        if (cv.Length > 0)
                        {
                            sbSql.Append( " and " + cv[0] + "='" + cv[1] + "'");
                        }
                    }
                }

                DataTable dt = SQLHelper.GetDataSet(sbSql.ToString());

                StringBuilder sb = new StringBuilder();

                if (dt != null && dt.Rows.Count > 0)
                {
                    //sb.Append("[");
                    string[] arr = columns.Split(',');

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        //sb.Append("{");

                        for (int j = 0; j < arr.Length; j++)
                        {
                            sb.Append(dr[j] + ",");
                            //sb.Append("\""+arr[j] + "\":\"" + dr[j] + "\",");
                            //sb.Append( "\"data\":\"" + dr[j] + "\",");
                        }

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("&");
                        //sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    //sb.Append("]");
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        public ReturnValue Delete(string table,string id)
        {
            ReturnValue rv = new ReturnValue();

            try
            {
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.AppendFormat("delete from {0} where Id = '{1}'", table, id);

                SQLHelper.ExecuteCommand(sqlstr.ToString());
                rv.IsSuccess = true;
            }
            catch(Exception ex)
            {
                rv.IsSuccess = false;
                rv.Message = ex.Message;
            }

            return rv;
        }
    }
}
