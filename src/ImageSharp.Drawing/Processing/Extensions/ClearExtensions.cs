// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.Processing;

namespace SixLabors.ImageSharp.Drawing.Processing
{
    /// <summary>
    /// Adds extensions that allow the flood filling of images without blending.
    /// </summary>
    public static class ClearExtensions
    {
        /// <summary>
        /// Flood fills the image with the specified color without any blending.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
        public static IImageProcessingContext Clear(this IImageProcessingContext source, Color color)
            => source.Clear(new SolidBrush(color));

        /// <summary>
        /// Flood fills the image with the specified color without any blending.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="options">The drawing options.</param>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
        public static IImageProcessingContext Clear(this IImageProcessingContext source, GraphicsOptions options, Color color)
            => source.Clear(options, new SolidBrush(color));

        /// <summary>
        /// Flood fills the image with the specified brush without any blending.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The details how to fill the region of interest.</param>
        /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
        public static IImageProcessingContext Clear(this IImageProcessingContext source, IBrush brush) =>
            source.Clear(source.GetGraphicsOptions(), brush);

        /// <summary>
        /// Flood fills the image with the specified brush without any blending.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="options">The drawing options.</param>
        /// <param name="brush">The details how to fill the region of interest.</param>
        /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
        public static IImageProcessingContext Clear(
            this IImageProcessingContext source,
            DrawingOptions options,
            IBrush brush)
            => source.Fill(options.CloneForClearOperation(), brush);

        /// <summary>
        /// Clones the graphic options and applies changes required to force clearing.
        /// </summary>
        /// <param name="options">The options to clone</param>
        /// <returns>A clone of option with ColorBlendingMode, AlphaCompositionMode, and BlendPercentage set</returns>
        internal static GraphicsOptions CloneForClearOperation(this GraphicsOptions options)
        {
            options = options.DeepClone();
            options.ColorBlendingMode = PixelFormats.PixelColorBlendingMode.Normal;
            options.AlphaCompositionMode = PixelFormats.PixelAlphaCompositionMode.Src;
            options.BlendPercentage = 1;
            return options;
        }

        /// <summary>
        /// Clones the path graphic options and applies changes required to force clearing.
        /// </summary>
        /// <param name="drawingOptions">The drawing options to clone</param>
        /// <returns>A clone of shapeOptions with ColorBlendingMode, AlphaCompositionMode, and BlendPercentage set</returns>
        internal static DrawingOptions CloneForClearOperation(this DrawingOptions drawingOptions)
        {
            GraphicsOptions options = drawingOptions.GraphicsOptions.DeepClone();
            options.ColorBlendingMode = PixelFormats.PixelColorBlendingMode.Normal;
            options.AlphaCompositionMode = PixelFormats.PixelAlphaCompositionMode.Src;
            options.BlendPercentage = 1F;

            return new DrawingOptions(options, drawingOptions.ShapeOptions, drawingOptions.TextOptions, drawingOptions.Transform);
        }
    }
}
