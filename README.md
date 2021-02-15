# AndcultureCode.CSharp.Sitefinity.Testing [![Build Status](https://travis-ci.org/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing.svg?branch=main)](https://travis-ci.org/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing)

Base classes, utilities, and extensions to facilitate writing tests for Sitefinity.

## Sitefinity Version Support

- Compatibility: `13+`
- Tested with: `13.2 (build 7521)`

## Example

Below is an example of an integration test suite for the Blog Post Sitefinity service.

```csharp
using AndcultureCode.CSharp.Sitefinity.Testing;
using AndcultureCode.CSharp.Sitefinity.Testing.Extensions;
using OData.Services.Models.Blogs;
using OData.Services.Services.Blogs;
using Shouldly;
using System;
using Xunit;
using Xunit.Abstractions;

namespace OData.Services.Tests.Services.Blogs
{
    [Collection("OData Session Collection")]
    public class BlogPostODataServicesTests : ODataServiceTestsBase<BlogPostODataServices, BlogPostDto>, IDisposable
    {
        #region Members

        private BlogODataServices _blogService;
        private BlogDto _blog;

        #endregion Members

        #region Setup and Teardowns

        public BlogPostODataServicesTests(
            ODataSessionFixture fixture,
            ITestOutputHelper output) : base(fixture, output)
        {
            var session = fixture.Session;
            _blogService = new BlogODataServices(session.ODataConnectionSettings, session);
            _blog = new BlogDto();
            Model = new BlogPostDto();
            Sut = new BlogPostODataServices(session.ODataConnectionSettings, session);
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeOfExistingModel(_blogService, _blog);
        }

        #endregion Setup and Teardown

        #region Tests

        [Fact]
        public void CreateDraft_When_Data_Provided_Then_Returns_Created_Status_Code_With_Returned_Data_Object_With_Same_Data()
        {
            // Arrange & Act
            var responseModel = CreateDraft("CreateDraft");

            // Assert
            responseModel.ShouldNotBeNull();
        }

        [Fact]
        public void Modify_When_Updating_Draft_Then_Returns_NoContent_Status_Code_And_Updates_Data_Object_With_Same_Data()
        {
            // Arrange
            CreateDraft("Modify");

            Model.Title += "Updated";
            Model.Description += "Updated";
            Model.Content += "Updated";
            Model.AllowComments = !Model.AllowComments;
            Model.Summary += "Updated";

            // Act
            var response = Sut.Modify(Model);

            // Assert
            response.ShouldBeExpectedStatusCode();

            response = Sut.GetItem(Model.Id.Value);
            var responseModel = response.ResultObject;
            responseModel.Title.ShouldBe(Model.Title);
            responseModel.Description.ShouldBe(Model.Description);
            responseModel.Content.ShouldBe(Model.Content);
            responseModel.AllowComments.ShouldBe(Model.AllowComments);
            responseModel.Summary.ShouldBe(Model.Summary);
        }

        [Fact]
        public void Delete_When_Deleting_Draft_Then_Returns_NoContent_Status_Code()
        {
            // Arrange
            var responseModel = CreateDraft("Delete");

            // Act
            var response = Sut.Delete(responseModel.Id.Value);

            // Assert
            response.ShouldBeExpectedStatusCode();
        }

        [Fact]
        public void Publish_When_Publishing_Draft_Then_Returns_Ok_Status_Code_And_Published_Response()
        {
            // Arrange
            var responseModel = CreateDraft("Publish");

            // Act
            var response = Sut.Publish(responseModel.Id.Value);

            // Assert
            response.RestResponse.ShouldEqualPublishedContentResponse();
        }

        [Fact]
        public void GetItem_When_Retrieving_Item_Then_Returns_Ok_Status_Code_And_Item()
        {
            // Arrange
            var responseModel = CreateDraft("GetItem");

            // Act
            var response = Sut.GetItem(responseModel.Id.Value);

            // Assert
            response.ShouldBeExpectedStatusCode();
            response.ResultObject.Id.ShouldBe(responseModel.Id);
        }

        [Fact]
        public void GetCount_When_Retrieving_Count_Then_Returns_Ok_Status_Code_And_Count()
        {
            // Arrange
            CreateDraft("GetCount");

            // Act
            var response = Sut.GetCount();

            // Assert
            response.ShouldBeExpectedStatusCode();
            response.ResultObject.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void Get_When_Retrieving_Items_Then_Returns_Ok_Status_Code_And_Items()
        {
            // Arrange
            CreateDraft("Get");

            // Act
            var response = Sut.Get();

            // Assert
            response.ShouldBeExpectedStatusCode();
            response.ResultObject.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        #endregion Tests

        #region Overrides

        protected BlogPostDto CreateDraft(string methodBeingTested)
        {
            _blog.Title = GetRandomString("Title");
            var newBlogResponse = _blogService.CreateDraft(_blog);

            _blog = newBlogResponse.ResultObject;

            Model.Title = GetRandomString("Title");
            Model.Description = GetRandomString("Description");
            Model.Content = GetRandomString("Content");
            Model.AllowComments = false;
            Model.Summary = GetRandomString("Summary");
            Model.ParentId = _blog.Id;

            var response = Sut.CreateDraft(Model);
            response.ShouldBeExpectedStatusCode();

            var responseModel = response.ResultObject;
            Model.Id = responseModel.Id;

            responseModel.Title.ShouldBe(Model.Title);
            responseModel.Description.ShouldBe(Model.Description);
            responseModel.Content.ShouldBe(Model.Content);
            responseModel.AllowComments.ShouldBe(Model.AllowComments);
            responseModel.Summary.ShouldBe(Model.Summary);
            responseModel.ParentId.ShouldBe(Model.ParentId);

            return responseModel;
        }

        #endregion Overrides
    }
}
```

# Community

[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/0)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/0)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/1)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/1)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/2)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/2)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/3)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/3)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/4)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/4)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/5)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/5)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/6)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/6)[![](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/images/7)](https://sourcerer.io/fame/andCulture/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/links/7)
