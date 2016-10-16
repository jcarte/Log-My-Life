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
    public class EditFieldAdapter : BaseAdapter<KeyValuePair<string,string>>
    {
        
        private readonly IList<KeyValuePair<string,string>> _items;
        private readonly Context _context;

        private bool _isEditable;

        public event Action<KeyValuePair<string, string>> ItemUpdated;



        public EditFieldAdapter(Context context, Dictionary<string,string> items, bool isEditable)
        {
            _items = items.ToList();
            _context = context;
            _isEditable = isEditable;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var view = convertView;

            if (view == null)//first time to create
            {
                var inflater = LayoutInflater.FromContext(_context);
                if (_isEditable)
                {
                    view = inflater.Inflate(Resource.Layout.editrow, parent, false);
                    var value = view.FindViewById<EditText>(Resource.Id.right_ER);
                    //value.EditorAction += OnEditorAction;//TODO swap for focus change
                    value.FocusChange += OnFocusChange;
                }
                else
                    view = inflater.Inflate(Resource.Layout.row, parent, false);
            }


            if (_isEditable)
            {
                var key = view.FindViewById<TextView>(Resource.Id.left_ER);
                var value = view.FindViewById<EditText>(Resource.Id.right_ER);

                key.Text = item.Key;
                value.Text = item.Value;
                value.FocusableInTouchMode = true;
            }
            else
            {
                var key = view.FindViewById<TextView>(Resource.Id.left);
                var value = view.FindViewById<TextView>(Resource.Id.right);

                key.Text = item.Key;
                value.Text = item.Value;
            }
            
            return view;
            
        }

        private void OnFocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if(!e.HasFocus)
            {
                EditText val = sender as EditText;
                TextView key = ((View)(val.Parent)).FindViewById<TextView>(Resource.Id.left_ER);
                //KeyValuePair<string, string> kvp = _items.FirstOrDefault(k => k.Key == key.Text);
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key.Text, val.Text);

                ItemUpdated?.Invoke(kvp);
            }
        }

        //KeyValuePair<string, string> lastKVP;//TODO find better way, so hacky

        //private void OnEditorAction(object sender, TextView.EditorActionEventArgs e)
        //{
        //    if (e.ActionId == ImeAction.Done)
        //    {
        //        EditText val = sender as EditText;
        //        TextView key = ((View)(val.Parent)).FindViewById<TextView>(Resource.Id.left_ER);
        //        //KeyValuePair<string, string> kvp = _items.FirstOrDefault(k => k.Key == key.Text);
        //        KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key.Text, val.Text);

        //        ItemUpdated?.Invoke(kvp);

        //        //if (!lastKVP.Equals(kvp))
        //        //{
        //        //    lastKVP = kvp;
        //        //    ItemUpdated?.Invoke(kvp);
        //        //}
                    
        //    }
        //}

        public override int Count
        {
            get { return _items.Count; }
        }


        public override KeyValuePair<string,string> this[int position]
        {
            get{return _items[position];}
        }

        
    }
}