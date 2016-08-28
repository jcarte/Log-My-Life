using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LogMyLife.Domain.Data
{
    public class Cell: IStorable
    {
        /// <summary>
        /// The record this cell is contained in (the row)
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int CellID { get; set; }


        /// <summary>
        /// The record this cell is contained in (the row)
        /// </summary>
        public int RecordID { get; set; }

        /// <summary>
        /// The column this cell is contained within
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// The actual data being stored
        /// </summary>
        public string Data { get; set; }

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
    }
}
