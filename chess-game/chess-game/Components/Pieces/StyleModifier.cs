using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace chess_game.Components.Pieces
{
    static class StyleModifier
    {
        #region Background

        private static readonly SolidColorBrush TransparentBackground = new(Colors.Transparent);

        /// <summary>
        /// Set element's background to transparent color.
        /// </summary>
        /// <param name="element">Element to set background</param>
        public static void MakeBackgroundTransparent(Control element)
        {
            element.Background = TransparentBackground;
        }

        /// <summary>
        /// Set element's background to an image with relative path for elements inherited from Control.
        /// </summary>
        /// <param name="element">Element to set image background</param>
        /// <param name="imagePath">Relative path to the image</param>
        public static void SetImageBackground(Control element, string imagePath)
        {
            string path = string.Format("ms-appx:///{0}", imagePath);
            Uri uriPath = new(path);
            BitmapImage backgroundImage = new() { UriSource = uriPath };
            element.Background = new ImageBrush() { ImageSource = backgroundImage };
        }

        /// <summary>
        /// Set element's background to an image with relative path for elements inherited from Panel.
        /// </summary>
        /// <param name="element">Element to set image background</param>
        /// <param name="imagePath">Relative path to the image</param>
        public static void SetImageBackground(Panel element, string imagePath)
        {
            string path = string.Format("ms-appx:///{0}", imagePath);
            Uri uriPath = new(path);
            BitmapImage backgroundImage = new() { UriSource = uriPath };
            element.Background = new ImageBrush() { ImageSource = backgroundImage };
        }

        #endregion

        #region Margins and Paddings

        private static readonly Thickness NoThickness = new(0.0);

        /// <summary>
        /// Make element have no margins.
        /// </summary>
        /// <param name="element">Element to remove margins</param>
        public static void NoMargin(Control element)
        {
            element.Margin = NoThickness;
        }

        /// <summary>
        /// Make element have no paddings.
        /// </summary>
        /// <param name="element">Element to remove paddings</param>
        public static void NoPadding(Control element)
        {
            element.Padding = NoThickness;
        }

        /// <summary>
        /// Make element have no margins and paddings.
        /// </summary>
        /// <param name="element">Element to remove margins and paddings</param>
        public static void NoMarginAndPadding(Control element)
        {
            NoMargin(element);
            NoPadding(element);
        }

        #endregion

        #region Alignments

        /// <summary>
        /// Set element's horizontal alignment.
        /// </summary>
        /// <param name="element">Element to set horizontal alignment</param>
        /// <param name="alignmentType">Horizontal alignment type</param>
        public static void SetAlignment(FrameworkElement element, HorizontalAlignment alignmentType)
        {
            element.HorizontalAlignment = alignmentType;
        }

        /// <summary>
        /// Set element's vertical alignment.
        /// </summary>
        /// <param name="element">Element to set vertical alignment</param>
        /// <param name="alignmentType">Vertical alignment type</param>
        public static void SetAlignment(FrameworkElement element, VerticalAlignment alignmentType)
        {
            element.VerticalAlignment = alignmentType;
        }

        /// <summary>
        /// Set element's horizontal and vertical alignment.
        /// </summary>
        /// <param name="element">Element to set horizontal and vertical alignment</param>
        /// <param name="horizontalAlignmentType">Horizontal and alignment type</param>
        /// <param name="verticalAlignmentType">Vertical and alignment type</param>
        public static void SetAlignment(FrameworkElement element,
                                        HorizontalAlignment horizontalAlignmentType ,
                                        VerticalAlignment verticalAlignmentType)
        {
            SetAlignment(element, horizontalAlignmentType);
            SetAlignment(element, verticalAlignmentType);
        }

        #endregion

        #region Corner radius

        private static readonly CornerRadius NoCornerRadius = new(0.0);

        /// <summary>
        /// Make element's corners square.
        /// </summary>
        /// <param name="element">Element to square corners.</param>
        public static void MakeCornersSquare(Control element)
        {
            element.CornerRadius = NoCornerRadius;
        }

        #endregion
    }
}
