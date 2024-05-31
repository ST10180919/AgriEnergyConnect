using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Models;
using System.Collections.Generic;

namespace ModulePlanner.Services
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// Class used to interact with the database
    /// </summary>
    public class MyDataService
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Entity used to access the entity data model
        /// </summary>
        private ApplicationDBContext DbContext;

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Logger used to log errors
        /// </summary>
        private readonly ILogger<AccountService> _logger;

        public MyDataService(ILogger<AccountService> logger)
        {
            _logger = logger;
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user"> user to be added </param>
        public void AddUser(User user)
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    this.DbContext.Roles.Update(user.Role);
                    this.DbContext.Users.Add(user);
                    this.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets a user with the matching username. Returns a new User if no matching 
        /// username exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserWithUsername(string email)
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    var user = this.DbContext.Users.Where(user => user.Email == email).Include(user => user.Products).
                        ThenInclude(product => product.Category).Include(user => user.Role).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
            return new User();
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determines if a user with the same username exists in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns> true if the user exists, false if not </returns>
        public bool UserExists(User user)
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    var userExists = this.DbContext.Users.Any(u => u.Email.Equals(user.Email));
                    return userExists;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return false;
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Updates all the data for a user
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    DbContext.Users.Update(user);
                    this.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Determines if the given login credentials are valid for a user in the database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserLoginIsValid(string email, string password)
        {
            // hasher used to hash the given password
            var hasher = new PasswordHasher<User>();
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    var user = this.DbContext.Users.FirstOrDefault(u => u.Email.Equals(email));

                    if (user != null)
                    {
                        // comparing hashed passwords
                        return hasher.VerifyHashedPassword(user, user.Password, password).Equals(PasswordVerificationResult.Success);
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return false;
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Get Categories from db
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    var categories = this.DbContext.Categories.ToList();
                    return categories;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Remove product from db
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product)
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    this.DbContext.Products.Remove(product);
                    this.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets roles from db
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    return this.DbContext.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Gets all products from db
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            try
            {
                using (this.DbContext = new ApplicationDBContext())
                {
                    return this.DbContext.Products.Include(p => p.Seller).Include(p => p.Category).ToList();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
//---------------------------------------EOF-------------------------------------------