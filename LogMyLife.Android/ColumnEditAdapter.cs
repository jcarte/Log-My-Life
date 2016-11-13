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
using LogMyLife.Domain.Model;
using Android.Views.InputMethods;

namespace LogMyLife.Android
{
    public class ColumnEditAdapter : BaseAdapter<Column>
    {


        private List<Column> _items;
        private readonly Context _context;


        public List<Column> Items { get { return _items; }  }

        public ColumnEditAdapter(Context context, List<Column> items)
        {
            _items = items.ToList();
            _context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Column item = _items[position];
            View view = convertView;
            EditText value = null;
            ImageButton btnDelete = null;

            if (view == null)//first time to create
            {
                var inflater = LayoutInflater.FromContext(_context);

                view = inflater.Inflate(Resource.Layout.editrowsingle, parent, false);
                value = view.FindViewById<EditText>(Resource.Id.rowEdit);
                value.EditorAction += OnEditorAction;
                value.TextChanged += OnTextChange;

                btnDelete = view.FindViewById<ImageButton>(Resource.Id.btnDelete);
                btnDelete.Click += BtnDelete_Click;

            }

            if(value == null)
                value = view.FindViewById<EditText>(Resource.Id.rowEdit);

            value.Tag = new ColumnHolder(item);
            value.Text = item.Name;
            
            //value.SetTag(1, position);
            value.FocusableInTouchMode = true;
            
            return view;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //Remove a column from the category
            ImageButton dlt = sender as ImageButton;
            EditText et = ((View)dlt.Parent).FindViewById<EditText>(Resource.Id.rowEdit);
            //Column col = _items[(int)et.GetTag(1)];
            et.Text = string.Empty;
        }

        private void OnTextChange(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            //ImageButton dlt = sender as ImageButton;
            //EditText et = ((View)dlt.Parent).FindViewById<EditText>(Resource.Id.rowEdit);
            EditText et = sender as EditText;
            Column col = _items.FirstOrDefault(co=>co.ColumnID == ((ColumnHolder)et.Tag).Column.ColumnID);
            col.Name = et.Text;
        }

        

        private void OnEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done)
            {
                InputMethodManager inputManager = (InputMethodManager)_context.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(((Activity)_context).CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

            }
        }

        public override int Count
        {
            get { return _items.Count; }
        }


        public override Column this[int position]
        {
            get{return _items[position];}
        }

        
    }


    public class ColumnHolder : Java.Lang.Object
    {
        public Column Column { get; set; }

        public ColumnHolder (Column c)
        {
            this.Column = c;
        }
    }

}