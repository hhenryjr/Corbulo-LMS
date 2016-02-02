using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UsersData;
using System.Configuration;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sabio.Web.Exceptions;

namespace Sabio.Web.Services
{
    public class UserDataService : BaseService, IUserDataService
    {
        private IFileService _fileService;

        public UserDataService(IFileService FileService)
        {
            _fileService = FileService;
        }

        public int Get()
        {
            return 0;
        }

        public UserInfo Get(int id)
        {
            UserInfo s = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Users_SelectById",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                },
                map: delegate (IDataReader reader, short set)
                 {
                     s = MapUser(reader);
                 }

                );
            return s;
        }

        private UserInfo MapUser(IDataReader reader)
        {
            UserInfo s = new UserInfo();
            UserTracks t = new UserTracks();
            Campus c = new Campus();
            int startingIndex = 0;

            s.Id = reader.GetSafeInt32(startingIndex++);
            s.FirstName = reader.GetSafeString(startingIndex++);
            s.LastName = reader.GetSafeString(startingIndex++);
            s.Email = reader.GetSafeString(startingIndex++);
            s.Gender = reader.GetSafeInt32(startingIndex++);
            s.Phone = reader.GetSafeString(startingIndex++);
            s.UserName = reader.GetSafeString(startingIndex++);
            s.UserId = reader.GetSafeString(startingIndex++);            
            s.Bio = reader.GetSafeString(startingIndex++);
            s.DesiredTrack = reader.GetSafeInt32(startingIndex++);
            s.DesiredCampusLocation = reader.GetSafeInt32(startingIndex++);
            s.CoverPhotoPath = reader.GetSafeString(startingIndex++);
            s.AvatarPhotoPath = reader.GetSafeString(startingIndex++);
            s.CoverPhotoUrl = _fileService.GetFullUrl(s.CoverPhotoPath);
            s.AvatarPhotoUrl = _fileService.GetFullUrl(s.AvatarPhotoPath);
            t.Name = reader.GetSafeString(startingIndex++);
            c.Id = reader.GetSafeInt32(startingIndex++);
            c.Name = reader.GetSafeString(startingIndex++);
            //s.AddressId = reader.GetSafeInt32(startingIndex++);
            //s.Facebook = reader.GetSafeString(startingIndex++);
            //s.LinkedIn = reader.GetSafeString(startingIndex++);
            //s.Twitter = reader.GetSafeString(startingIndex++);
            //s.Webpage = reader.GetSafeString(startingIndex++);
            s.OnboardStatus = reader.GetSafeInt32(startingIndex++);

            s.Campuses = c;
            s.UserTracks = t;

            return s;

        }

        public UserInfo GetByUserId(string UserId)
        {
            UserInfo s = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Users_SelectByUserIdIncludingImagePaths",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@UserId", UserId);
                },
                map: delegate (IDataReader reader, short set)
                {
                    s = MapUser(reader);
                }

                );
            return s;

        }

        public int Insert(UserInfoAddRequest model, string userId)
        {
            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Insert",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@FirstName", model.FirstName);
                    parameterCollection.AddWithValue("@LastName", model.LastName);
                    parameterCollection.AddWithValue("@UserName", model.UserName);
                    parameterCollection.AddWithValue("@Email", model.Email);
                    parameterCollection.AddWithValue("@Phone", model.Phone);
                    parameterCollection.AddWithValue("@Gender", model.Gender);
                    parameterCollection.AddWithValue("@Bio", model.Bio);
                    parameterCollection.AddWithValue("@UserId", userId);

                    SqlParameter s = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterCollection.Add(s);
                },

                returnParameters: delegate (SqlParameterCollection para)
                {
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }

                );
            return id;
        }

        public void Update(UserInfoUpdate model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Update",
           inputParamMapper: delegate (SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@FirstName", model.FirstName);
               u.AddWithValue("@LastName", model.LastName);
               u.AddWithValue("@UserName", model.UserName);
               u.AddWithValue("@Email", model.Email);
               u.AddWithValue("@Phone", model.Phone);
               u.AddWithValue("@Gender", model.Gender);
               u.AddWithValue("@Bio", model.Bio);
               u.AddWithValue("@CoverPhotoId", model.CoverPhotoId);
               u.AddWithValue("@AvatarPhotoId", model.AvatarPhotoId);

           });
        }

        public void UpdateUserStatus(UserStatusUpdate model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_UpdateV2",
           inputParamMapper: delegate(SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@UserId", model.UserId);
               u.AddWithValue("@EnrollmentStatusId", model.EnrollmentStatusId);
           });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserSections_Insert",
               inputParamMapper: delegate(SqlParameterCollection param)
               {
                   param.AddWithValue("@UserId", model.UserId);
                   param.AddWithValue("@SectionId", model.SectionId);
                   param.AddWithValue("@EnrollmentStatusId", model.EnrollmentStatusId);

               });

        }

        public List<UserInfo> GetsList()
        {
            List<UserInfo> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Users_Select"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   UserInfo s = new UserInfo();
                   int startingIndex = 0;

                   s.Id = reader.GetSafeInt32(startingIndex++);
                   s.FirstName = reader.GetSafeString(startingIndex++);
                   s.LastName = reader.GetSafeString(startingIndex++);
                   s.Email = reader.GetSafeString(startingIndex++);
                   s.Gender = reader.GetSafeInt32(startingIndex++);
                   s.Phone = reader.GetSafeString(startingIndex++);
                   s.UserName = reader.GetSafeString(startingIndex++);
                   s.Bio = reader.GetSafeString(startingIndex++);
                   s.DesiredTrack = reader.GetSafeInt32(startingIndex++);
                   s.DesiredCampusLocation = reader.GetSafeInt32(startingIndex++);
                   s.OnboardStatus = reader.GetSafeInt32(startingIndex++);

                   if (list == null)
                   {
                       list = new List<UserInfo>();
                   }

                   list.Add(s);
               }
               );


            return list;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_DeleteByID",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                });

        }

        public List<UserInfo> GetList()
        {
            List<UserInfo> s = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Users_SelectUsersByEnrollmentStatusID"
                , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   UserInfo item = MapUser(reader);

                   if (s == null)
                   {
                       s = new List<UserInfo>();
                   }

                   s.Add(item);
               });


            return s;
        }

        public List<UserBase> GetByRole()
        {
            List<UserBase> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.User_SelectByRoleName"
                , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   UserBase s = new UserBase();
                   int startingIndex = 0;

                  
                   s.FirstName = reader.GetSafeString(startingIndex++);
                   s.LastName = reader.GetSafeString(startingIndex++);
                   s.Email = reader.GetSafeString(startingIndex++);
                   s.UserId = reader.GetSafeString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<UserBase>();
                   }

                   list.Add(s);
               }
               );


            return list;
        }

        public bool ConfirmEmail(Guid tokenId)
        {
            bool result = false;
            Token token = UserTokensService.Get(tokenId);

            ApplicationUserManager userManager = UserService.GetUserManager();
            ApplicationUser user = userManager.FindByName(token.UserName);
            if (user != null)
            {
                // Set the EmailConfirmed flag in AspNetUsers table
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_UpdateEmailConfirmed"
                   , inputParamMapper: delegate (SqlParameterCollection UpdateEmailConfirmed)
                   {
                       UpdateEmailConfirmed.AddWithValue("@EmailConfirmed", true);
                       UpdateEmailConfirmed.AddWithValue("@UserName", token.UserName);
                   });

                // Insert new record in Users table
                ApplicationUser au = UserService.GetUserbyUserName(token.UserName);
                UserInfoAddRequest req = new UserInfoAddRequest();
                req.UserName = token.UserName;
                req.Phone = au.PhoneNumber;
                req.Email = au.Email;

                Insert(req, au.Id);

                // Delete the token from UserTokens table
                UserTokensService.Delete(tokenId);
                result = true;
            }
            return result;
        }

        public List<UserInfo> GetOnboardedStudents()
        {
            List<UserInfo> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Users_SelectOnboarderStudents"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   UserInfo s = new UserInfo();
                   int startingIndex = 0;

                   s.Id = reader.GetSafeInt32(startingIndex++);
                   s.UserId = reader.GetSafeString(startingIndex++);
                   s.FirstName = reader.GetSafeString(startingIndex++);
                   s.LastName = reader.GetSafeString(startingIndex++);
                   s.DesiredSabioStartDate = reader.GetDateTime(startingIndex++);
                   Campus campus = new Campus();
                   campus.Name = reader.GetSafeString(startingIndex++);
                   s.Campuses = campus;
                   Track track = new Track();
                   track.Name = reader.GetSafeString(startingIndex++);
                   s.Track = track;

                   if (list == null)
                   {
                       list = new List<UserInfo>();
                   }

                   list.Add(s);
               }
               );


            return list;
        }

    }
}