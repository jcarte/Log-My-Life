using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMyLife.Domain.Data
{
    internal interface IStorable
    {

        //TODO needs ID?

        /// <summary>
        /// Reserved DB audit field - is this object flagged as deleted? Allows items to not be removed
        /// from DB, preserving information
        /// </summary>
        bool IsRecordDeleted { get; set; }

        /// <summary>
        /// Time that the DB record (this object) was last edited, is a reserved DB audit field
        /// </summary>
        DateTime RecordModifed { get; set; }

        /// <summary>
        /// Time that the DB record (this object) was created, is a reserved DB audit field
        /// </summary>
        DateTime RecordCreated { get; set; }
    }
}
