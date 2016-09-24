namespace LogMyLife.Domain.Model
{
    /// <summary>
    /// A list of items all with the same theme
    /// </summary>
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
        
        /// <summary>
        /// The type of information held inside the category
        /// </summary>
        public CategoryType Type { get; set; }

        /// <summary>
        /// Type of information held in a category
        /// </summary>
        public enum CategoryType //TODO finalise
        {
            UserCreated = 0,
            Music = 1,
            Film = 2,
            Book = 3,
            Wine = 4
        }

        /// <summary>
        /// Blank contructor, only this project can create
        /// </summary>
        internal Category()//TODO params to make sure cat is created properly?
        {}
        
        public override string ToString()
        {
            return $"ID = {CategoryID}, Name = {Name}, Type = {Type.ToString()}";
        }

    }
}
