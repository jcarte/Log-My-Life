using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using d= LogMyLife.Domain.Data;

namespace LogMyLife.Domain.Model
{
    public class Category
    {
        /// <summary>
        /// Unique Identifier for the category
        /// </summary>
        public int CategoryID { get; internal set; }

        /// <summary>
        /// Name of the list
        /// </summary>
        public string Name { get; set; }
        
        public CategoryType Type { get; set; }

        public enum CategoryType //TODO finalise
        {
            UserCreated = 0,
            Music = 1,
            Film = 2,
            Book = 3,
            Wine = 4
        }

        //can create new - how to handle PK?
        internal Category()
        {

        }


        //public Category(d.Category c)
        //{

        //    this.CategoryID = c.CategoryID;
        //    this.Name = c.Name;

        //    try
        //    {
        //        this.Type = (CategoryType)c.Type;
        //    }
        //    catch (Exception)
        //    {
        //        this.Type = CategoryType.UserCreated;
        //    }


        //}

        public override string ToString()
        {
            return $"ID = {CategoryID}, Name = {Name}, Type = {Type.ToString()}";
        }

    }
}
