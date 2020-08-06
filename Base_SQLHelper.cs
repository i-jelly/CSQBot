using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using System.Data;

namespace cn.orua.qngel.Code
{
    /**public class Base_SQLHelper
    {
        
        public class SQLHelperData
        {
            public bool IsBusy;
            public SQLiteConnection Connection;
            public SQLiteDataReader reader;
            public SQLiteCommand command;
        }

        /// <summary>
        /// 所有人的好感-1
        /// </summary>
        /// <param name="b"></param>
        public void UpdateUserFavor(SQLHelperData b)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET Favor = Favor - 1 WHERE Favor > 0";
            b.command.ExecuteNonQuery();
            b.Connection.Close();
        }

        /// <summary>
        /// 所有人的涩图限制+1
        /// </summary>
        /// <param name="b"></param>
        public void UpdateUserEroImgLimit(SQLHelperData b)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET EroImgLimit = EroImgLimit + 1 WHERE EroImgLimit < 12";
            b.command.ExecuteNonQuery();
            b.Connection.Close();
        }

        /// <summary>
        /// 增加指定AccountID的好感度(每句话
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        public void AddFavorEveryChat(SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET Favor = Favor + 1 WHERE QQ = @AccountID";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.command.ExecuteNonQuery();
            b.Connection.Close();
        }

        /// <summary>
        /// 初始化QQ群的数据库表
        /// </summary>
        /// <param name="b"></param>
        public void NewGroupTable( SQLHelperData b)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"CREATE TABLE IF NOT EXISTS User (
                                    id          INTEGER PRIMARY KEY     AUTOINCREMENT,
                                    QQ          TEXT    NOT NULL,
                                    Cash        TEXT    DEFAULT '5000',
                                    JoinDate    INT     NOT NULL,
                                    LastSign    INT     DEFAULT 0,
                                    LastS       INT     DEFAULT 50,
                                    LastSS      INT     DEFAULT 100,
                                    AuthLevel   INT     DEFAULT 1,
                                    EroImgLimit INT     DEFAULT 0,
                                    Favor       INT     DEFAULT 0,
                                    Call        TEXT    DEFAULT 'ご主人',
                                    Called      TEXT    DEFAULT '丛雨'
                            )";
            b.command.ExecuteNonQuery();
            b.command.CommandText = @"CREATE TABLE IF NOT EXISTS Ava(
                                    id INTEGER PRIMARY  KEY     AUTOINCREMENT,
                                    QQ                  TEXT    NOT NULL,
                                    Ava                 TEXT    NOT NULL,
                                    Type                TEXT    NOT NULL
                            )";
            b.command.ExecuteNonQuery();
            b.command.CommandText = @"CREATE TABLE LotHis (
                                    id INTEGER PRIMARY  KEY     AUTOINCREMENT,
                                    QQ                  TEXT    NOT NULL,
                                    Ava                 TEXT    NOT NULL,
                                    Type                TEXT    NOT NULL
                            )";
            b.command.ExecuteNonQuery();
            b.Connection.Close();
        }

        /// <summary>
        /// 数据库中是否存在对应的群的数据表
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool HasTable( SQLHelperData b)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name = 'User'";
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            if (b.reader.Read())
            {
                bool _reply = b.reader.GetInt32(0) > 0;
                b.reader.Close();
                return _reply;
            }
            b.reader.Close();
            return false;
        }

        /// <summary>
        /// 返回机器人对用户的称呼
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns>称呼</returns>
        public String GetCall( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT Call FROM User WHERE QQ = '@AccountID'";
            b.command.Parameters.Add("AccountID",DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            if (b.reader.Read())
            {
                String _reply = b.reader.GetString(0);
                b.reader.Close();
                return _reply;
            }
            b.reader.Close();
            return "None";
        }

        /// <summary>
        /// 设置用户对机器人的称呼
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <param name="Call">称呼</param>
        /// <returns>成功与否</returns>
        public bool SetCall( SQLHelperData b,long AccountID,String Call)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET Call = @Call WHERE QQ = @AccountID";
            b.command.Parameters.Add("Call", DbType.String).Value = Call;
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            bool _reply = b.command.ExecuteNonQuery() > 0;
            b.Connection.Close();
            return _reply;
        }

        /// <summary>
        /// 获取用户对机器人的称呼
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns>用户怎么叫机器人的</returns>
        public String GetCalled( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT Called FROM User WHERE QQ = '@AccountID'";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            if (b.reader.Read())
            {
                String _reply = b.reader.GetString(0);
                b.reader.Close();
                return _reply;
            }
            b.reader.Close();
            return "None";
        }

        /// <summary>
        /// 设置用户对机器人的称呼
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <param name="Called">称呼</param>
        /// <returns></returns>
        public bool SetCalled( SQLHelperData b,long AccountID, String Called)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET Called = @Call WHERE QQ = @AccountID";
            b.command.Parameters.Add("Call", DbType.String).Value = Called;
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            bool _reply = b.command.ExecuteNonQuery() > 0;
            b.Connection.Close();
            return _reply;
        }

        /// <summary>
        /// 增加用户记录，注册时用到
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns></returns>
        public bool AddUser( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            b.command.Connection = b.Connection;
            b.command.CommandText = @"INSERT INTO User (QQ,JoinDate) VALUES (@AccountID,@JoinDate)";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.command.Parameters.Add("JoinDate", DbType.Int64).Value = (long)(DateTime.Now - time).TotalSeconds;
            bool _reply = b.command.ExecuteNonQuery() > 0;
            b.Connection.Close();
            return _reply;
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns></returns>
        public bool UserExists( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT COUNT(*) FROM User WHERE QQ = @AccountID";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            if (b.reader.Read())
            {
                bool _reply = b.reader.GetInt32(0) > 0;
                b.reader.Close();
                return _reply;
            }
            b.reader.Close();
            return false;
        }

        /// <summary>
        /// 获取用户的货币
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns>字符串形式的数量</returns>
        public String GetCash( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT Cash FROM User WHERE QQ = @AccountID";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            b.reader.Read();
            String _reply = b.reader.GetString(0);
            b.reader.Close();
            return _reply;
        }

        /// <summary>
        /// 设置用户的货币数量
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <param name="Cash"></param>
        /// <returns>执行的成功与否</returns>
        public bool SetCash( SQLHelperData b,long AccountID, String Cash)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET Cash = @Cash WHERE QQ = @AccountID";
            b.command.Parameters.Add("Cash", DbType.String).Value = Cash;
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            bool _reply = b.command.ExecuteNonQuery() > 0;
            b.Connection.Close();
            return _reply;
        }

        /// <summary>
        /// 获取用户的认证等级（暂时无用
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns>INT形式的等级</returns>
        public int GetAuthLevel( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT AuthLevel FROM User WHERE QQ = @AccountID";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            b.reader.Read();
            int _reply = b.reader.GetInt32(0);
            b.reader.Close();
            return _reply;
        }

        /// <summary>
        /// 设置用户的认证等级（暂时无用
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <param name="AuthLevel">认证等级</param>
        /// <returns></returns>
        public bool SetAuthLevel( SQLHelperData b, long AccountID, int AuthLevel)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET AuthLevel = @AuthLevel WHERE QQ = @AccountID";
            b.command.Parameters.Add("AuthLevel", DbType.Int32).Value = AuthLevel;
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            bool _reply = b.command.ExecuteNonQuery() > 0;
            b.Connection.Close();
            return _reply;
        }

        /// <summary>
        /// 获取用户的好感度
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns>INT形式的好感度</returns>
        public int GetFavor( SQLHelperData b,long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT  Favor FROM User WHERE QQ = @AccountID";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader(CommandBehavior.CloseConnection);
            b.reader.Read();
            int _reply = b.reader.GetInt32(0);
            b.reader.Close();
            return _reply;
        }

        /// <summary>
        /// 设置用户的好感度
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <param name="Favor">好感度</param>
        /// <returns></returns>
        public bool SetFavor( SQLHelperData b,long AccountID, int Favor)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"UPDATE User SET Favor = @Favor WHERE QQ = @AccountID";
            b.command.Parameters.Add("Favor", DbType.Int32).Value = Favor;
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            bool _reply = b.command.ExecuteNonQuery() > 0;
            b.Connection.Close();
            return _reply;
        }

        /// <summary>
        /// 刷新签到
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns></returns>
        public bool RenewSign(SQLHelperData b, long AccountID)
        {
            b.Connection.Open();
            b.command.Connection = b.Connection;
            b.command.CommandText = @"SELECT LastSign FROM User WHERE QQ = @AccountID";
            b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
            b.reader = b.command.ExecuteReader();
            b.reader.Read();
            TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            if ((b.reader.GetInt64(0) + 28800) / 86400 < ((long)t.TotalSeconds + 28800) / 86500)
            {
                b.reader.Close();
                b.command.CommandText = @"UPDATE User SET LastSign = @TimeSpan WHERE QQ = @AccountID";
                b.command.Parameters.Add("TimeSpan",DbType.Int64).Value = (long)t.TotalSeconds;
                b.command.Parameters.Add("AccountID", DbType.String).Value = AccountID.ToString();
                if(b.command.ExecuteNonQuery(CommandBehavior.CloseConnection) > 0)
                {
                    b.Connection.Close();
                    return true;
                }
            }
            b.Connection.Close();
            return false;
        }

        /// <summary>
        /// 每日签到获取货币
        /// </summary>
        /// <param name="b"></param>
        /// <param name="AccountID">QQ号</param>
        /// <returns></returns>
        public int DailySignWithCashUpdate(SQLHelperData b, long AccountID)
        {
            int CashAdd = new Random().Next(100, 1000);
            SetCash(b, AccountID, (CashAdd + int.Parse(GetCash(b, AccountID))).ToString());
            return CashAdd;
        }
    }**/
}
