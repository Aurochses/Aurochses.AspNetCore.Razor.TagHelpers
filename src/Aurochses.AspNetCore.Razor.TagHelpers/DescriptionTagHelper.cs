using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aurochses.AspNetCore.Razor.TagHelpers
{
    /// <summary>
    /// Description TagHelper.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    [HtmlTargetElement("div", Attributes = DescriptionForAttributeName)]
    public class DescriptionTagHelper : TagHelper
    {
        private const string DescriptionForAttributeName = "asp-description-for";

        /// <summary>
        /// Gets or sets the description for.
        /// </summary>
        /// <value>The description for.</value>
        [HtmlAttributeName(DescriptionForAttributeName)]
        public ModelExpression DescriptionFor { get; set; }

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var metadata = DescriptionFor.Metadata;
            if (metadata == null)
            {
                throw new InvalidOperationException($"No provided metadata ({DescriptionForAttributeName})");
            }

            if (!string.IsNullOrWhiteSpace(metadata.Description))
            {
                // ReSharper disable once MustUseReturnValue
                output.Content.SetContent(metadata.Description);
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }
    }
}