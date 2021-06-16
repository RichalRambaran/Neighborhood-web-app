using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Buurtapp.Areas.Identity.Data;
using Buurtapp.Controllers;
using Buurtapp.Data;
using Buurtapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Buurtapptest
{
    public class UnitTest1
    {
        //private string databaseName;

        //private UserContext GetContextWithData()
        //{
        //    UserContext context = GetNewInMemoryDatabase(true);
        //    context.Users.Add(new AppUser { Email = "test@test.nl", Id = "1" });
        //    context.Posts.Add(new Post { PostId = 1, UserId = "1", Title = "Title post 1", Content = "context post 1" });

        //    context.SaveChanges();

        //    return GetNewInMemoryDatabase(false);
        //}

        //private UserContext GetNewInMemoryDatabase(bool NewDb)
        //{
        //    if (NewDb)
        //    {
        //        this.databaseName = Guid.NewGuid().ToString();
        //    }
        //    var options = new DbContextOptionsBuilder<UserContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    return new UserContext(options);

        //}

        //Like test
        public interface IWijzigLikeResult {
            int postid { get; set; }
            int toegevoegd { get; set; }
        }

        [Fact]
        public void Toggle_Like_For_User_Test()
        {
            // arrange
            var identity = new GenericIdentity("tugberk");

            var mockUser = new Mock<UserManager<IdentityUser>>();
            var postid = 1;


            var response = new HttpResponseFeature();

            var features = new FeatureCollection();
            features.Set<IHttpResponseFeature>(response);

            var context = new DefaultHttpContext(features)

            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "test@test.nl"),
                }))
            };

            var controller = new LikesController(GetContextWithData());
            controller.ControllerContext.HttpContext = context;

            // act
            var result = controller.WijzigLike(postid) as JsonResult;

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            var jsonRsesult = result.Value;
            Assert.Equal(postid, jsonRsesult.GetType().GetProperty("postid").GetValue(jsonRsesult, null));
            Assert.Equal( 1, jsonRsesult.GetType().GetProperty("toegevoegd").GetValue(jsonRsesult, null));


            // act again
            result = controller.WijzigLike(postid) as JsonResult;

            // assert again
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            jsonRsesult = result.Value;
            Assert.Equal(0, jsonRsesult.GetType().GetProperty("toegevoegd").GetValue(jsonRsesult, null));

        }

        private UserContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new UserContext(options);

            context.Users.Add(new AppUser { Email = "test@test.nl", Id = "1" });

            context.Posts.Add(new Post { PostId = 1, UserId = "1", Title = "Title post 1", Content = "context post 1" });
            context.SaveChanges();

            return context;
        }

        // Views Test

        [Fact]
        public void ViewResultsTest()
        {
            Post newPost = new Post { PostId = 2, UserId = "1", Title = "Title post 2", Content = "context post 2" };
            // PostController pc = new PostController(GetContextWithData());
            // Post dbPost = GetContextWithData().Posts.Where(p => p.PostId == newPost.PostId).FirstOrDefault();
            // pc.Create(newPost, null);
            GetContextWithData().Posts.Add(newPost);
            GetContextWithData().SaveChanges();

            Assert.True(GetContextWithData().Posts.Count() == 1);
            // Assert.True(GetContextWithData().Posts.Count() == 2);
            var dbPost = GetContextWithData().Posts.FirstOrDefault(p => p.PostId == 2);
            Assert.Equal(newPost, dbPost);
            // var result = pc.Create();

        }
    }



}

