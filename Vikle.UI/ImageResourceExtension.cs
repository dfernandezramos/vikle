using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI
{
    /// <summary>
    /// This class contains the markup extension for setting embedded image resources as source 
    /// </summary>
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        /// <summary>
        /// Gets or sets the source of the embedded image resource.
        /// </summary>
        public string Source { get; set; }
        
        /// <summary>
        /// Provides the value of the embedded image resource.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Source == null ? null: ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
    }
}