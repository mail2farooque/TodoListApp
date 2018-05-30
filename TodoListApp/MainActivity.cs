//using AdMaiora.AppKit.UI.App;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.AppCompat;
using Android.Content;
using System;
using System.Collections.Generic;
using TodoListApp.Helper;

namespace TodoListApp
{
    [Activity(Label = "TodoListApp", MainLauncher = true, Theme = "@style/Theme.AppCompact.Lights")]
    public class MainActivity : AppCompatActivity
    {
        private EditText taskEditText;
        private DbHelper dbHelper;
        private CustomAdapter adapter;
        private ListView lstTask;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Android.Resource.Menu.menu_item, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.action_add:
                    taskEditText = new EditText(this);
                    Android.Support.V7.App.AlertDialog dialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Task")
                        .SetMessage("What do you want to do next?")
                        .SetView(taskEditText)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    dialog.Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = taskEditText.Text;
            dbHelper.InsertNewTask(task);
            LoadtaskList();
        }

        public void LoadtaskList()
        {
            List<string> taskList = dbHelper.GetTaskList();
            adapter = new CustomAdapter(this, taskList, dbHelper);
            lstTask.Adapter = adapter;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            dbHelper = new DbHelper(this);
            lstTask = FindViewById<ListView>(Resource.Id.lstTask);

            //Load Data
            LoadtaskList();
        }

       
    }
}

