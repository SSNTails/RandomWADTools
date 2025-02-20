using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaypalEdit
{
    public static class BossSpriteMaker
    {
        const string machinePath = @"D:\32xrb2\Sprites\EGGB2_MACH\1";
        const string eggmanPath = @"D:\32xrb2\Sprites\EGGB2_BODY\1";

        public static void Make()
        {
            var machineFiles = System.IO.Directory.GetFiles(machinePath, "*.png");
            var eggmanFiles = System.IO.Directory.GetFiles(eggmanPath, "*.png");

            foreach (var mf in machineFiles)
            {
                Bitmap bmpM = new Bitmap(mf);

                foreach (var ef in eggmanFiles)
                {
                    using (Bitmap bmpE = new Bitmap(ef))
                    {
                        if (bmpE.Width != bmpM.Width)// || bmpE.Height != bmpM.Height)
                            continue;

                        for (int x = 0; x < bmpM.Width; x++)
                        {
                            /*
                            // Bottom to top
                            for (int y = bmpM.Height-1, yE = bmpE.Height-1; y >= 0 && yE >= 0; y--, yE--)
                            {
                                Color cM = bmpM.GetPixel(x, y);
                                Color cE = bmpE.GetPixel(x, yE);

                                if (cE == cM || cE.A < 255)
                                    bmpE.SetPixel(x, yE, Color.FromArgb(0, 255, 255));
                            }*/

                            // Top to bottom
                            for (int y = 0, yE = 0; y < bmpM.Height; y++, yE++)
                            {
                                Color cM = bmpM.GetPixel(x, y);
                                Color cE = bmpE.GetPixel(x, yE);

                                if (cE == cM || cE.A < 255)
                                    bmpE.SetPixel(x, yE, Color.FromArgb(0, 255, 255));
                            }

                            for (int yE = 0; yE < bmpE.Height; yE++)
                            {
                                Color cE = bmpE.GetPixel(x, yE);

                                if (cE.A < 255)
                                    bmpE.SetPixel(x, yE, Color.FromArgb(0, 255, 255));
                            }
                        }

                        string outputFile = System.IO.Path.GetFileName(ef);
                        outputFile = Path.Combine(eggmanPath + @"\out", outputFile);
                        bmpE.Save(outputFile, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }
    }
}
