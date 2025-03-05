using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaypalEdit
{

    class ColormapEntry
    {
        public byte[] values = new byte[3];

        public override string ToString()
        {
            return values[0].ToString() + ", " + values[1].ToString() + ", " + values[2].ToString();
        }
    }
    public class Colormap
    {
        static byte BestColor(int r, int g, int b, Palette palsrc, int rangel, int rangeh)
        {
            int i;
            int dr, dg, db;
            int bestdistortion, distortion;
            int bestcolor;

            // let any color go to 0 as a last resort
            bestdistortion = (r * r + g * g + b * b) * 2;
            bestcolor = GetBlack(palsrc);

            int palStart = 0;
            for (i = rangel; i <= rangeh; i++)
            {
                if (rangel + palStart == 0xfc)
                    continue; // Megadrive 'thru' color
                if (rangel + palStart == 0)
                    continue; // Transparent color

                PalEntry pal = palsrc.Entries[rangel + palStart];
                dr = r - (int)pal.R;
                dg = g - (int)pal.G;
                db = b - (int)pal.B;
                palStart++;
                distortion = dr*dr + dg*dg + db*db;
                if (distortion < bestdistortion)
                {
                    if (distortion == 0)
                        return (byte)i; // perfect match

                    bestdistortion = distortion;
                    bestcolor = i;
                }
            }

            return (byte)bestcolor;
        }

        public static byte GetBlack(Palette palsrc)
        {
            for (int i = 0; i < 256; i++)
            {
                if (palsrc.Entries[i].R == 0 && palsrc.Entries[i].G == 0 && palsrc.Entries[i].B == 0)
                    return (byte)i;
            }

            return 0;
        }

        public static byte[,] BuildLights(Palette palsrc, int numlights)
        {
            int red, green, blue;//, ri, gi, bi;
            //short color12, color15;
            byte[,] lightpalette = new byte[numlights, 256];

            for (int l = 0; l < numlights; l++)
            {
                for (int c = 0; c < palsrc.Entries.Length; c++)
                {
                    PalEntry entry = palsrc.Entries[c];
                    red = entry.R;
                    green = entry.G;
                    blue = entry.B;

                    red = (red * (numlights - l) + numlights / 2) / numlights;
                    green = (green * (numlights - l) + numlights / 2) / numlights;
                    blue = (blue * (numlights - l) + numlights / 2) / numlights;
                    /*
                    ri = (red + 8) >> 4;
                    ri = ri > 15 ? 15 : ri;
                    gi = (green + 8) >> 4;
                    gi = gi > 15 ? 15 : gi;
                    bi = (blue + 8) >> 4;
                    bi = bi > 15 ? 15 : bi;

                    color12 = (short)((ri << 12) + (gi << 8) + (bi << 4) + 15);

                    ri = (red + 4) >> 3;
                    ri = ri > 31 ? 31 : ri;
                    gi = (green + 4) >> 3;
                    gi = gi > 31 ? 31 : gi;
                    bi = (blue + 4) >> 3;
                    bi = bi > 31 ? 31 : bi;

                    color15 = (short)((ri << 10) + (gi << 5) + bi);
                    */
                    lightpalette[l, c] = BestColor(red, green, blue, palsrc, 1, 255);

//                    color12s[l][c] = color12;
//                    color15s[l][c] = color15;
                }
            }

            return lightpalette;
        }
    }
}
