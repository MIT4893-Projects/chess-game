using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    static class StyleModifier
    {
        private static readonly SolidColorBrush TransparentBackground = new(Colors.Transparent);
        private static readonly Thickness NoThickness = new(0.0);

        #region Background

        public static void MakeBackgroundTransparent(Control element)
        {
            element.Background = TransparentBackground;
        }

        public static void SetImageBackground(Control element, string imagePath)
        {
            string path = string.Format("ms-appx:///{0}", imagePath);
            Uri uriPath = new(path);
            BitmapImage backgroundImage = new() { UriSource = uriPath };
            element.Background = new ImageBrush() { ImageSource = backgroundImage };
        }

        public static void SetImageBackground(Panel element, string imagePath)
        {
            string path = string.Format("ms-appx:///{0}", imagePath);
            Uri uriPath = new(path);
            BitmapImage backgroundImage = new() { UriSource = uriPath };
            element.Background = new ImageBrush() { ImageSource = backgroundImage };
        }

        #endregion

        #region Margins and Paddings

        public static void NoMargin(Control element)
        {
            element.Margin = NoThickness;
        }

        public static void NoPadding(Control element)
        {
            element.Padding = NoThickness;
        }

        public static void NoMarginAndPadding(Control element)
        {
            NoMargin(element);
            NoPadding(element);
        }

        #endregion

        #region Alignments

        public static void SetAlignment(FrameworkElement element, HorizontalAlignment alignmentType)
        {
            element.HorizontalAlignment = alignmentType;
        }

        public static void SetAlignment(FrameworkElement element, VerticalAlignment alignmentType)
        {
            element.VerticalAlignment = alignmentType;
        }

        public static void SetAlignment(FrameworkElement element,
                                        HorizontalAlignment horizontalAlignmentType,
                                        VerticalAlignment verticalAlignmentType)
        {
            SetAlignment(element, horizontalAlignmentType);
            SetAlignment(element, verticalAlignmentType);
        }

        #endregion
    }
}
