using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TodoListApp.Helper
{
    public class DbHelper : SQLiteOpenHelper
    {
        private static string DB_NAME = "TODODB";
        private static int DB_VER = 1;
        public static string DB_TABLE = "Task";
        public static string DB_COLUMN = "TaskName";
        public DbHelper(Context context):base (context, DB_NAME, null, DB_VER) { }
        public override void OnCreate(SQLiteDatabase db)
        {
            string query = $"CREATE TABLE {DbHelper.DB_TABLE} (ID INTEGER PRIMARY KEY AUTOINCREMENT, {DbHelper.DB_COLUMN} TEXT NOT NULL);";
            db.ExecSQL(query);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            string query = $"DELETE TABLE IF EXISTS {DB_TABLE}";
            db.ExecSQL(query);
            OnCreate(db);
        }

        public void InsertNewTask(string task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put(DB_COLUMN,task);
            db.InsertWithOnConflict(DB_TABLE, null, values, Android.Database.Sqlite.Conflict.Replace);
            db.Close();
        }

        public void DeleteTask(string task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            db.Delete(DB_TABLE, DB_COLUMN + "= ? ", new[] {task});
            db.Close();
        }

        public List<string> GetTaskList()
        {
            List<string> tasList = new List<string>();
            SQLiteDatabase db = this.WritableDatabase;
            ICursor cursor = db.Query(DB_TABLE, new string[] {DB_COLUMN}, null, null, null, null, null);
            while (cursor.MoveToNext())
            {
                int index = cursor.GetColumnIndex(DB_COLUMN);
                tasList.Add(cursor.GetString(index));
            }

            return tasList;
        }
    }
}