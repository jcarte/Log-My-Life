namespace LogMyLife.Domain.Model
{
    /// <summary>
    /// A list of items all with the same theme
    /// </summary>
    public class Column
    {

        /// <summary>
        /// Unique Identifier for the category
        /// </summary>
        public int ColumnID { get; internal set; }

        /// <summary>
        /// Unique Identifier for the category
        /// </summary>
        public int CategoryID { get; internal set; }

        /// <summary>
        /// Name of the Column
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Order that the column appears inside the category
        /// </summary>
        public int Order { get; set; }
        

        public ColumnType Type { get; set; }


        public bool IsHidden { get; set; }




        public enum ColumnType
        {
            Title,Review,Normal
        }



        /// <summary>
        /// Blank contructor, only this project can create
        /// </summary>
        internal Column()
        { }

        public override string ToString()
        {
            return $"ID = {ColumnID}, Name = {Name}, Order = {Order}, Type = {Type.ToString()}, IsHidden = {IsHidden}";
        }

    }
}
