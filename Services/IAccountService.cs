using AgriEnergyConnect.Models;

namespace ModulePlanner.Services
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// Interface used to structure the AccountService (needs to be here to add to the
    /// application's services)
    /// </summary>
    public interface IAccountService
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// User logged into the application
        /// </summary>
        User UserLoggedIn { get; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Log a user into the applicaiton
        /// </summary>
        /// <param name="user"></param>
        void LoginUser(User user);

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determine if the given credentials are valid
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool LoginCredentialsAreValid(string email, string password);

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="user"></param>
        void RegisterUser(User user);

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determine if a user with the same username exists
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UserExists(User user);

        List<Category> GetCategories();

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determines if the logged in user has any semester settings
        /// </summary>
        /// <returns></returns>
        bool UserHasData();

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Saves the logged in user's data to the database
        /// </summary>
        void SaveUserData();

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Removes a given product
        /// </summary>
        /// <param name="product"></param>
        void RemoveProduct(Product product);

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets the roles in the DB
        /// </summary>
        /// <returns></returns>
        List<Role> GetRoles();

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Adds a user without logging them in
        /// </summary>
        /// <param name="user"></param>
        void AddUserWithoutLoggingIn(User user);

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets all products regardless of Seller
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProducts();
    }
}
//---------------------------------------EOF-------------------------------------------