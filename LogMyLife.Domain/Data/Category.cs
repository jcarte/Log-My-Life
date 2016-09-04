using System;
using SQLite;


namespace LogMyLife.Domain.Data
{
    /// <summary>
    /// A category represents a single list of items with it's own set of
    /// columns and records.
    /// </summary>
    public class Category: IStorable
    {
        /// <summary>
        /// The unique identifier for the category
        /// </summary>
        [PrimaryKey,AutoIncrement]   
        public int CategoryID { get; set; }

        /// <summary>
        /// Description of the category
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Numerical representation of the type
        /// </summary>
        public int Type { get; set; }

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
