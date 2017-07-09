//------------------------------------------------------------------------------
// <copyright file="TextAdornment1.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows.Media.Imaging;

namespace CyberRebeatIDE
{
    /// <summary>
    /// TextAdornment1 places red boxes behind all the "a"s in the editor window
    /// </summary>
    internal sealed class TextAdornment1
    {
        /// <summary>
        /// The layer of the adornment.
        /// </summary>
        private readonly IAdornmentLayer layer;

        /// <summary>
        /// Text view where the adornment is created.
        /// </summary>
        private readonly IWpfTextView view;

        private Image canvas = new Image() { IsHitTestVisible = false };

        private BitmapImage bitmap = new BitmapImage(new Uri(@"Images\bg.png", UriKind.Relative));

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAdornment1"/> class.
        /// </summary>
        /// <param name="view">Text view to create the adornment for</param>
        public TextAdornment1(IWpfTextView view)
        {
            this.view = view;
            this.layer = this.view.GetAdornmentLayer("TextAdornment1");
            this.view.LayoutChanged += (s, e) => setImage();
        }

        /// <summary>
        /// 背景画像セット
        /// </summary>
        private void setImage()
        {
            this.layer.RemoveAllAdornments();

            canvas.Source = bitmap;
            canvas.Opacity = 0.3;
            canvas.Stretch = Stretch.None;

            Canvas.SetLeft(canvas, this.view.ViewportRight - bitmap.Width);
            Canvas.SetTop(canvas, this.view.ViewportTop);

            canvas.Width = bitmap.Width;
            canvas.Height = bitmap.Height;

            this.layer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, canvas, null);
        }
    }
}
