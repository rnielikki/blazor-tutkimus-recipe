using Xunit;
using Egil.RazorComponents.Testing;
using RecipeApplication.Client.Pages;
using RichardSzalay.MockHttp;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using KeywordSearchBox;
using RecipeApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApplicationTest
{
    public class RecipePageTest : ComponentTestFixture
    {
        [Fact]
        public async Task EmptyResultTest()
        {
            var mockHttp = Services.AddMockHttp();
            mockHttp.When($"http://example.com/api/Recipes/1").Respond("application/json", "null");
            Services.Add(new ServiceDescriptor(typeof(ISearchModel<RecipeDto>), MakeMock()));

            var component = RenderComponent<Recipe>(
                (nameof(Recipe.Id),1)
                );
            //placeholder until the feature is done.
            await Task.Delay(100);
            Assert.Equal("Not found.", component.Find("p").TextContent);
        }
        private ISearchModel<RecipeDto> MakeMock()
        {
            var mock = new Mock<ISearchModel<RecipeDto>>();
            mock.SetupGet(model => model.Results).Returns(new List<RecipeDto>());
            return mock.Object;
        }
    }
}
