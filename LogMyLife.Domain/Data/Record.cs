using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace LogMyLife.Domain.Data
{
    /// <summary>
    /// A single record within a category
    /// </summary>
    public class Record : IStorable
    {
        [PrimaryKey,AutoIncrement]
        public int RecordID { get; set; }

        [Indexed]
        public int CategoryID { get; set; }

        /// <summary>
        /// Records are archived when they are completed,
        /// unarchived records are still current.
        /// </summary>
        public bool IsArchived { get; set; }

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
            return string.Format($"RecordId: {RecordID}, CatId: {CategoryID}, IsArchived: {IsArchived}");
        }
    }
}
