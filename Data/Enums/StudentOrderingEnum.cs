namespace Data.Enums
{
    /// <summary>
    /// Defines the available sorting options for student records
    /// </summary>
    public enum StudentOrderingEnum
    {
        /// <summary>
        /// Sort students by their unique identifier
        /// </summary>
        StudID = 0,
        /// <summary>
        /// Sort students alphabetically by their name
        /// </summary>
        Name = 1,
        /// <summary>
        /// Sort students by their address
        /// </summary>
        Address = 2,
        /// <summary>
        /// Sort students by their department name
        /// </summary>
        DepartmentName = 3
    }
}
