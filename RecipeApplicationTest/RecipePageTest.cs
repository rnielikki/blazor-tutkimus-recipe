using Xunit;
using Bunit;
using RecipeApplication.Client.Pages;
using RichardSzalay.MockHttp;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using KeywordSearchBox;
using RecipeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace RecipeApplicationTest
{
    public class RecipePageTest : ComponentTestFixture
    {
        public RecipePageTest()
        {
            Services.AddSingleton<MockHttpMessageHandler>();
            Services.AddSingleton<HttpMessageHandler>(srv => srv.GetRequiredService<MockHttpMessageHandler>());
            Services.AddSingleton<HttpClient>(srv => new HttpClient(srv.GetRequiredService<HttpMessageHandler>()));
            Services.AddSingleton(MakeMock());
            Services.GetService<HttpClient>().BaseAddress = new System.Uri("http://localhost");
        }
        [Fact]
        public async Task EmptyResultTest()
        {
            var mockHttp = Services.GetService<MockHttpMessageHandler>();
            mockHttp.When($"http://localhost/api/Recipes/*").Respond(System.Net.HttpStatusCode.NotFound);

            var component = RenderComponent<Recipe>(
                (nameof(Recipe.Id),1)
                );
            //placeholder until the feature is done.
            await Task.Delay(500);
            WaitForAssertion(()=>component.Find("div"));
            Assert.Equal("Not Found.", component.Find("p").TextContent);
        }
        private ISearchModel<RecipeDto> MakeMock()
        {
            var mock = new Mock<ISearchModel<RecipeDto>>();
            mock.SetupGet(model => model.Results).Returns(new List<RecipeDto>());
            return mock.Object;
        }
    }
}
