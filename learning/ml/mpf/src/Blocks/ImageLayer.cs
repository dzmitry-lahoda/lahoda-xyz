﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using AForge.Imaging.Textures;
using MathNet.Numerics.LinearAlgebra.Generic;
using Graphics=System.Drawing.Graphics;
using Rectangle=System.Drawing.Rectangle;


namespace dnHTM.ImageLayer
{
    public static class ImageLayer
    {

        public static double[,] LoadBlackAndWhiteQuadraticImageMatrix(string fileName)
        {
            var  imageMatrix = new double[1,1];
 
            var bitmap = new Bitmap(fileName);

            var bitmapasd = ChangePixelFormat(bitmap, PixelFormat.Format24bppRgb);
            var bitmap8pp = ColorToGrayscale(bitmapasd);
            
            var floats = TextureTools.FromBitmap(bitmap8pp);
            imageMatrix = FromFloats(floats);



            return imageMatrix;

        }

        private static double[,] FromFloats(float[,] floats)
        {
            //TODO: Consider usage of Buffer,Marshal or unsafe fixed context,
            var size = floats.GetLength(0);
            var converted = new double[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (floats[i, j] < 0.999)
                    {
                        converted[i, j] = 0;
                    }
                    else
                    {
                        converted[i, j] = floats[i, j];
                    }
                }
            }
            return converted;
        }

        public static List<Matrix<double>> FrameImageMatrix(double[,] imageMatrix, int frameSize)
        {
            var frames = new List<Matrix<double>>();

            return frames;
        }

        private static Bitmap ChangePixelFormat(Bitmap inputImage, PixelFormat newFormat)
        {
            var bmp = new Bitmap(inputImage.Width, inputImage.Height, newFormat);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawImage(inputImage, 0, 0);
            }
            return bmp;
        }

                private static Bitmap ChangePixelFormat2(Bitmap inputImage, PixelFormat newFormat)
        {
                    var bm2 = new Bitmap(inputImage.Width, inputImage.Height, PixelFormat.Format24bppRgb);
                    var g2 = Graphics.FromImage(bm2);
                    g2.DrawImage(inputImage, new Rectangle(0, 0, 1, 1), 0, 0, inputImage.Width, inputImage.Height,
                                 GraphicsUnit.Pixel);

            return bm2;
        }


        /// <summary>
        /// Converts a RGB bitmap into an 8-bit grayscale bitmap
        /// </summary>
        public static Bitmap ColorToGrayscale(Bitmap bmp)
        {
            int w = bmp.Width,
                h = bmp.Height,
                r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            PixelFormat pfIn = bmp.PixelFormat;
            ColorPalette palette;
            Bitmap output;
            BitmapData bmpData, outputData;

            //Create the new bitmap
            output = new Bitmap(w, h, PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            palette = output.Palette;
            for (int i = 0; i < 256; i++)
            {
                Color tmp = Color.FromArgb(255, i, i, i);
                palette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            output.Palette = palette;

            //No need to convert formats if already in 8 bit
            if (pfIn == PixelFormat.Format8bppIndexed)
            {
                output = (Bitmap)bmp.Clone();

                //Make sure the palette is a grayscale palette and not some other
                //8-bit indexed palette
                output.Palette = palette;

                return output;
            }

            //Get the number of bytes per pixel
            switch (pfIn)
            {
                case PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly,
                                   pfIn);
            outputData = output.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly,
                                         PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = .299*R + .587*G + .114*B
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                (0.299f * bmpPtr[r * bmpStride + ic] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 2]);
                }
                else //bytesPerPixel == 4
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = alpha * (.299*R + .587*G + .114*B)
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                ((bmpPtr[r * bmpStride + ic] / 255.0f) *
                                (0.299f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 2] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 3]));
                }
            }

            //Unlock the images
            bmp.UnlockBits(bmpData);
            output.UnlockBits(outputData);

            return output;
        }
    }
}

