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

namespace LogMyLife.Android
{
    public class EditFieldAdapter : BaseAdapter<KeyValuePair<string,string>>
    {
        
        private readonly IList<KeyValuePair<string,string>> _items;
        private readonly Context _context;

        private bool _isEditable;

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

            if (view == null)
            {
                var inflater = LayoutInflater.FromContext(_context);
                if(_isEditable)
                    view = inflater.Inflate(Resource.Layout.editrow, parent, false);
                else
                    view = inflater.Inflate(Resource.Layout.row, parent, false);
            }

            if (_isEditable)
            {
                view.FindViewById<TextView>(Resource.Id.left_ER).Text = item.Key;
                view.FindViewById<EditText>(Resource.Id.right_ER).Text = item.Value;
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.left).Text = item.Key;
                view.FindViewById<TextView>(Resource.Id.right).Text = item.Value;
            }

            return view;

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