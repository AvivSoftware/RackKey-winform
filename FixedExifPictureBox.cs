﻿using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
public class FixedPictureBox : PictureBox
{
    public static Image CorrectExifOrientation(Image image)
    {
        if (image == null) return null;
        int orientationId = 0x0112;
        if (image.PropertyIdList.Contains(orientationId))
        {
            var orientation = (int)image.GetPropertyItem(orientationId).Value[0];
            var rotateFlip = RotateFlipType.RotateNoneFlipNone;
            switch (orientation)
            {
                case 1: rotateFlip = RotateFlipType.RotateNoneFlipNone; break;
                case 2: rotateFlip = RotateFlipType.RotateNoneFlipX; break;
                case 3: rotateFlip = RotateFlipType.Rotate180FlipNone; break;
                case 4: rotateFlip = RotateFlipType.Rotate180FlipX; break;
                case 5: rotateFlip = RotateFlipType.Rotate90FlipX; break;
                case 6: rotateFlip = RotateFlipType.Rotate90FlipNone; break;
                case 7: rotateFlip = RotateFlipType.Rotate270FlipX; break;
                case 8: rotateFlip = RotateFlipType.Rotate270FlipNone; break;
                default: rotateFlip = RotateFlipType.RotateNoneFlipNone; break;
            }
            if (rotateFlip != RotateFlipType.RotateNoneFlipNone)
            {
                image.RotateFlip(rotateFlip);
                image.RemovePropertyItem(orientationId);
            }
        }
        return image;
    }
    [Localizable(true)]
    [Bindable(true)]
    public new Image Image
    {
        get { return base.Image; }
        set { base.Image = value; CorrectExifOrientation(value); }
    }

}