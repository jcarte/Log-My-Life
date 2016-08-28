using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
//using SQLite.Extensions;
//using SQLite.Net;


//using SQLiteNetExtensions;
//using SQLiteNetExtensions.Attributes;
//using SQLiteNetExtensions.Exceptions;
//using SQLiteNetExtensions.Extensions;


namespace LogMyLife.Domain.Data
{
    /// <summary>
    /// A category represents a single list of items with it's own set of
    /// columns and records.
    /// </summary>
    public class Category: IStorable
    {
        [PrimaryKey,AutoIncrement]   
        public int CategoryID { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public int Type { get; set; }

        //TODO adding in objects doesn't work, when tested FK, make these work too, check documentation
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Column> Columns { get; set; }


        /// <summary>
        /// Reserved DB audit field - is this object flagged as deleted? Allows items to not be removed
        /// from DB, preserving information
        /// </summary>
        public bool IsRecordDeleted { get; set; }

        /// <summary>
        /// Time that the DB record (this object) was last edited, is a reserved DB audit field
        /// </summary>
        public DateTime RecordModifed { get; set; }

        /// <summary>
        /// Time that the DB record (this object) was created, is a reserved DB audit field
        /// </summary>
        public DateTime RecordCreated { get; set; }



        public override string ToString()
        {
            return string.Format($"ID: {CategoryID}, NAME: {Name}, TYPE: {Type}");
        }
    }








}
