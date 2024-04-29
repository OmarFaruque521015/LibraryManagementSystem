using LibraryManagement.BLL.IRepositories;
using LibraryManagement.DLL;
using LibraryManagement.Models.Models;
using LibraryManagement.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Repositories
{
    public class AuthorRepository: IAuthorRepository
    {
        private IConfiguration _config;
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Roles> _rolesRepository;
        private readonly LibraryDBContext _libraryDbContext;
        public AuthorRepository(IConfiguration config,IRepository<Roles> rolesRepository, IRepository<Users> usersRepository, LibraryDBContext libraryDbContext)
        {
            _config = config;
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _libraryDbContext = libraryDbContext;

        }
 
        public async Task<AuthorReturnVM> AuthenticateUserAsync(UserVM userVM)
        {

            AuthorReturnVM result = new AuthorReturnVM();
            var data = _libraryDbContext.Users.FirstOrDefault(c =>
                                    (c.UserName == userVM.UserName)
                                  && c.Password == userVM.Password);

            if (data == null) { result.ErrorMessage = "Invalid Username/Password!"; return result; }


            var userData = await _usersRepository.GetByIdAsync(data.UserID);
            var roleIdList = _libraryDbContext.RoleAssigns.Where(x => x.UserID == data.UserID).Select(x => x.RoleID).ToList();
            var roles = _libraryDbContext.Roles.Where(m => roleIdList.Contains(m.RoleID)).ToList();

            // generating jwt token
            var tokenString = GenerateJSONWebToken(data.UserName, (int)data.UserID, roles);
            result.Token = tokenString;
            result.UserName = data.UserName;
            result.UserID = data.UserID;

            return result;
        }


        private string GenerateJSONWebToken(string userName, int userId, List<Roles> roles)
        {
            var data = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
         new Claim("UserName", userName),
         new Claim("UserId", userId.ToString()),
         new Claim("UserRoles",JsonConvert.SerializeObject(roles).ToString()),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
     };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
