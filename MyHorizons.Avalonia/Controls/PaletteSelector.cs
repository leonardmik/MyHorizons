﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MyHorizons.Data.TownData;
using System;

namespace MyHorizons.Avalonia.Controls
{
    public sealed class PaletteSelector : Canvas
    {
        private static readonly Pen gridPen = new Pen(new SolidColorBrush(0xFF777777), 2, null, PenLineCap.Flat, PenLineJoin.Bevel);
        private static readonly Bitmap background = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>().Open(new Uri("resm:MyHorizons.Avalonia.Resources.ItemGridBackground.png")));

        private DesignPattern? _design;

        public new double Height
        {
            get => base.Height;
            private set => base.Height = value;
        }

        public new double Width
        {
            get => base.Width;
            set => Resize(value);
        }

        public PaletteSelector(DesignPattern pattern, double width = 16.0d)
        {
            _design = pattern;
            Background = new ImageBrush(background)
            {
                Stretch = Stretch.Uniform,
                TileMode = TileMode.Tile,
                SourceRect = new RelativeRect(0, 0, background.Size.Width, background.Size.Height, RelativeUnit.Absolute),
                DestinationRect = new RelativeRect(0, 0, width, width, RelativeUnit.Absolute),
            };
            Resize(width);
        }

        public void SetDesign(DesignPattern? pattern)
        {
            _design = pattern;
            InvalidateVisual();
        }

        private void Resize(double w)
        {
            base.Width = w;
            base.Height = 16 * w;
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            if (_design != null)
            {
                for (var i = 0; i < 16; i++)
                {
                    var rect = new Rect(0, i * Width, Width, Width);
                    if (i < 15)
                        context.FillRectangle(new SolidColorBrush(_design.Palette[i].ToArgb()), rect);
                    context.DrawRectangle(gridPen, rect);
                }
            }
        }
    }
}
