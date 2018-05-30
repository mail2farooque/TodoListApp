using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace TodoListApp.Helper
{
    public class CustomAdapter : BaseAdapter
    {
        private MainActivity mainActivity;
        private List<string> taskList;
        private DbHelper dbHelper;

        public CustomAdapter(MainActivity mainActivity, List<string> taskList, DbHelper dbHelper)
        {
            this.mainActivity = mainActivity;
            this.taskList = taskList;
            this.dbHelper = dbHelper;
        }

        public override Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater) mainActivity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.row, null);
            TextView txtTask = view.FindViewById<TextView>(Android.Resource.Id.task_title);
            Button btnDelete = view.FindViewById<Button>(Android.Resource.Id.btnDelete);
            txtTask.Text = taskList[position];
            btnDelete.Click += delegate
            {
                string task = taskList[position];
                dbHelper.DeleteTask(task);
                mainActivity.LoadtaskList(); //reload data
            };
            return view;
        }

        public override int Count => taskList.Count;
    }
}