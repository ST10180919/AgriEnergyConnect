using AgriEnergyConnect.Models;

namespace ModulePlanner.Services
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// Main service class used to communicate with the database and authenticate actions
    /// </summary>
    public class AccountService : IAccountService
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Stores the user that is currently logged into the application
        /// </summary>
        public User UserLoggedIn { get; private set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Data service used to communicate with the database
        /// </summary>
        private MyDataService _userDataService { get; }

        public AccountService(ILogger<AccountService> logger)
        {
            UserLoggedIn = new User();
            this._userDataService = new MyDataService(logger);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets the given user's data from the database and populates the UserLoggedIn
        /// field
        /// </summary>
        /// <param name="user"></param>
        public void LoginUser(User user)
        {
            this.UserLoggedIn = this._userDataService.GetUserWithUsername(user.Email);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determines whether the given login credentials are valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns> True if the credentials are valid, false if not </returns>
        public bool LoginCredentialsAreValid(string username, string password)
        {
            return this._userDataService.UserLoginIsValid(username, password);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Registers the given user by adding a new user. Setts the UserLoggedIn.
        /// </summary>
        /// <param name="user"></param>
        public void RegisterUser(User user)
        {
            this._userDataService.AddUser(user);
            this.UserLoggedIn = user;
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determines if a user with the given user's username exists.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UserExists(User user)
        {
            return this._userDataService.UserExists(user);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Saves the current data in the UserLoggedIn to the database
        /// </summary>
        public void SaveUserData()
        {
            this._userDataService.UpdateUser(this.UserLoggedIn);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determines if the user currently logged in has any semester data
        /// </summary>
        /// <returns></returns>
        public bool UserHasData()
        {
            return this.UserLoggedIn.Products.Any();
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets all categories from the database
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            return this._userDataService.GetCategories();
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Removes a product from the database
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product)
        {
            this._userDataService.RemoveProduct(product);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets all roles in the database
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            return this._userDataService.GetRoles();
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Add a user without logging them in
        /// </summary>
        /// <param name="user"></param>
        public void AddUserWithoutLoggingIn(User user)
        {
            this._userDataService.AddUser(user);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets all products in the database
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            return this._userDataService.GetAllProducts();
        }
    }
}
//---------------------------------------EOF-------------------------------------------