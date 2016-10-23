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
        
        private IList<KeyValuePair<string,string>> _items;
        private readonly Context _context;

        private bool _isEditable;

        public IList<KeyValuePair<string, string>> Items { get { return _items; }  }

        //public event Action<KeyValuePair<string, string>> ItemUpdated;



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
                    value.EditorAction += OnEditorAction;
                    //value.FocusChange += OnFocusChange;
                    value.TextChanged += OnTextChange;
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

        private void OnTextChange(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            EditText val = sender as EditText;
            TextView key = ((View)(val.Parent)).FindViewById<TextView>(Resource.Id.left_ER);

            var kvp = _items.First(k => k.Key == key.Text);
            _items[_items.IndexOf(kvp)] = new KeyValuePair<string, string>(key.Text, val.Text);

            //KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key.Text, val.Text);

            //if (_items.Any(k => k.Key == kvp.Key) && _items.First(k => k.Key == kvp.Key).Value != kvp.Value)
            //    ItemUpdated?.Invoke(kvp);
        }

        //private void OnFocusChange(object sender, View.FocusChangeEventArgs e)
        //{
        //    if(!e.HasFocus)
        //    {
        //        EditText val = sender as EditText;
        //        TextView key = ((View)(val.Parent)).FindViewById<TextView>(Resource.Id.left_ER);
        //        KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key.Text, val.Text);

        //        if (_items.Any(k => k.Key == kvp.Key) && _items.First(k => k.Key == kvp.Key).Value != kvp.Value)
        //            ItemUpdated?.Invoke(kvp);
        //    }
        //}

        private void OnEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done)
            {
                InputMethodManager inputManager = (InputMethodManager)_context.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(((Activity)_context).CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                //EditText val = sender as EditText;
                //TextView key = ((View)(val.Parent)).FindViewById<TextView>(Resource.Id.left_ER);
                //KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key.Text, val.Text);

                //if (_items.Any(k => k.Key == kvp.Key) && _items.First(k => k.Key == kvp.Key).Value != kvp.Value)
                //    ItemUpdated?.Invoke(kvp);
            }
        }

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