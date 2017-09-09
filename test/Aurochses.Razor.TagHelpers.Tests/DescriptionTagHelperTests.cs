using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Aurochses.Testing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace Aurochses.Razor.TagHelpers.Tests
{
    public class DescriptionTagHelperTests
    {
        private const string DescriptionForAttributeName = "asp-description-for";

        private readonly DescriptionTagHelper _descriptionTagHelper;

        public DescriptionTagHelperTests()
        {
            _descriptionTagHelper = new DescriptionTagHelper();
        }

        [Theory]
        [InlineData(typeof(HtmlTargetElementAttribute), nameof(HtmlTargetElementAttribute.Tag), "div", nameof(HtmlTargetElementAttribute.Attributes), DescriptionForAttributeName)]
        public void Attribute_Defined(Type attributeType, string firstAttributePropertyName, object firstAttributePropertyValue, string secondAttributePropertyName, object secondAttributePropertyValue)
        {
            // Arrange & Act & Assert
            var typeInfo = TypeAssert.HasAttribute<DescriptionTagHelper>(attributeType);

            AttributeAssert.ValidateProperty(typeInfo, attributeType, firstAttributePropertyName, firstAttributePropertyValue);
            AttributeAssert.ValidateProperty(typeInfo, attributeType, secondAttributePropertyName, secondAttributePropertyValue);
        }

        [Fact]
        public void Inherit_TagHelper()
        {
            // Arrange & Act & Assert
            Assert.IsAssignableFrom<TagHelper>(_descriptionTagHelper);
        }

        [Theory]
        [InlineData(typeof(HtmlAttributeNameAttribute), nameof(HtmlAttributeNameAttribute.Name), DescriptionForAttributeName)]
        public void DescriptionFor_Attribute_Defined(Type attributeType, string attributePropertyName, object attributePropertyValue)
        {
            // Arrange & Act & Assert
            var propertyInfo = TypeAssert.PropertyHasAttribute<DescriptionTagHelper>("DescriptionFor", attributeType);

            AttributeAssert.ValidateProperty(propertyInfo, attributeType, attributePropertyName, attributePropertyValue);
        }

        [Fact]
        public void Process_NullTagHelperContext_ThrowsArgumentNullException()
        {
            // Arrange
            var mockTagHelperOutput = new Mock<TagHelperOutput>(
                MockBehavior.Strict,
                "",
                new TagHelperAttributeList(),
                new Func<bool, HtmlEncoder, Task<TagHelperContent>>(
                    (b, encoder) =>
                    {
                        return new Task<TagHelperContent>(() => new DefaultTagHelperContent());
                    }
                )
            );

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _descriptionTagHelper.Process(null, mockTagHelperOutput.Object));
            Assert.Equal("context", exception.ParamName);
        }

        [Fact]
        public void Process_NullTagHelperOutput_ThrowsArgumentNullException()
        {
            // Arrange
            var mockTagHelperContext = new Mock<TagHelperContext>(MockBehavior.Strict, new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _descriptionTagHelper.Process(mockTagHelperContext.Object, null));
            Assert.Equal("output", exception.ParamName);
        }

        // todo: solve tests!
        //[Fact]
        //public void Process_NullModelMetadata_ThrowsInvalidOperationException()
        //{
        //    InvalidOperationException exception = null;

        //    var tagHelperContext = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));
        //    var tagHelperOutput = new TagHelperOutput(
        //        "div",
        //        new TagHelperAttributeList(),
        //        (b, htmlEncoder) =>
        //        {
        //            return new Task<TagHelperContent>(() => new DefaultTagHelperContent());
        //        }
        //    );
        //    //var mockTagHelperContext = new Mock<TagHelperContext>(MockBehavior.Strict, new TagHelperAttributeList(), new Dictionary<object, object>(), "");
        //    //var mockTagHelperOutput = new Mock<TagHelperOutput>(
        //    //    MockBehavior.Strict,
        //    //    "",
        //    //    new TagHelperAttributeList(),
        //    //    new Func<bool, HtmlEncoder, Task<TagHelperContent>>(
        //    //        (b, encoder) =>
        //    //        {
        //    //            return new Task<TagHelperContent>(() => new DefaultTagHelperContent());
        //    //        }
        //    //    )
        //    //);

        //    Expression<Func<TestModel, string>> i = model => model.ToString();
        //    var c = ExpressionMetadataProvider.FromLambdaExpression(i, new ViewDataDictionary<TestModel>(new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>())), new ModelStateDictionary()), new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>())));

        //    _descriptionTagHelper.DescriptionFor = new ModelExpression("test", c);

        //    try
        //    {
        //        //_descriptionTagHelper.DescriptionFor = new ModelExpression("", new ModelExplorer(new EmptyModelMetadataProvider(), new DefaultModelMetadata(new EmptyModelMetadataProvider(), new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>()), new DefaultMetadataDetails(ModelMetadataIdentity.ForProperty(), )), null));

        //        _descriptionTagHelper.Process(tagHelperContext, tagHelperOutput);
        //    }
        //    catch (InvalidOperationException e)
        //    {
        //        exception = e;
        //    }

        //    Assert.NotNull(exception);
        //    Assert.Equal($"No provided metadata ({DescriptionForAttributeName})", exception.Message);
        //}
    }
}