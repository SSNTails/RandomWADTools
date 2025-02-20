// See https://aka.ms/new-console-template for more information
using PlaypalEdit;
using System.Drawing;
using System.Text;

Console.WriteLine("Hello, World!");

Palette srb2pal = Palette.LoadJascPalette(@"D:\32xrb2\srbPal0.pal");

List<Palette> outputPals = new List<Palette>();
int palIndex = 0;
outputPals.Add(srb2pal);

// Now compose the other palettes

//SRB2 should use:

//0: Base
//1-5: white flash (armageddon shield) (normal to white ramp)
//6-10: black fade (normal to black ramp)
//11: blue tint (water)
//12: pause
//13: purple tint (thz water)


// white fade
PalEntry[] entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.8);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.6);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.4);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.2);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

// Completely white
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.0);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

// Black fade
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.8);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.6);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.4);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.2);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

// Completely black
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.0);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

// Blue tint (water)
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(64, 160, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.5);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

// Pause menu (Grey)
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(128, 128, 128);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.5);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

// Purple tint (THZ water)
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal.Entries[i].R, srb2pal.Entries[i].G, srb2pal.Entries[i].B);
    Color mixColor = Color.FromArgb(183, 0, 183);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.5);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
outputPals.Add(new Palette(entries));

Palette.WriteJagPlaypal(outputPals.ToArray(), @"D:\32xrb2\SRB2_PLAYPALS.lmp");

byte[,] colormapLights = Colormap.BuildLights(srb2pal, 32);

byte[,] colormapLights16 = Colormap.BuildLights(srb2pal, 64);

using (BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\32xrb2\SRB2_COLORMAP.lmp", FileMode.Create)))
{
    for (int i = 0; i < colormapLights.GetLength(0); i++)
    {
        for (int j = 128; j < colormapLights.GetLength(1); j++)
            bw.Write(colormapLights[i, j]);
        for (int j = 0; j < 128; j++)
            bw.Write(colormapLights[i, j]);
    }

    byte black = 0;
    for (int i = 0; i < 256; i++)
        bw.Write(black);
}

using (BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\32xrb2\SRB2_COLORMAP_16.lmp", FileMode.Create)))
{
    for (int i = 0; i < 63; i += 2)
    {
        for (int j = 128; j < 256; j++)
        {
            bw.Write(colormapLights16[i+1, j]);
            bw.Write(colormapLights16[i, j]);
        }
        for (int j = 0; j < 128; j++)
        {
            bw.Write(colormapLights16[i + 1, j]);
            bw.Write(colormapLights16[i, j]);
        }
    }

    byte black = 0;
    for (int i = 0; i < 256; i++)
    {
        bw.Write(black);
        bw.Write(black);
    }

    // Boss flashing colormap
    for (int i = 128; i < 256; i++)
    {
        black = (byte)i;
        bw.Write(black);
        bw.Write(black);
    }
    for (int i = 0; i < 128; i++)
    {
        black = (byte)i;

        if (i == 31)
            black = 255;
        else if (i == 30)
            black = 1;
        else if (i == 29)
            black = 2;
        else if (i == 28)
            black = 3;
        else if (i == 27)
            black = 4;
        else if (i == 26)
            black = 5;
        else if (i == 25)
            black = 6;
        else if (i == 24)
            black = 7;
        else if (i == 23)
            black = 8;
        else if (i == 22)
            black = 9;
        else if (i == 21)
            black = 10;
        else if (i == 20)
            black = 11;
        else if (i == 19)
            black = 12;
        else if (i == 18)
            black = 13;
        else if (i == 17)
            black = 14;
        else if (i == 16)
            black = 15;

        bw.Write(black);
        bw.Write(black);
    }

    // Yellow text colormap
    for (int i = 128; i < 256; i++)
    {
        black = (byte)i;
        bw.Write(black);
        bw.Write(black);
    }
    for (int i = 0; i < 128; i++)
    {
        if (i == 3)
            black = 73;
        else if (i == 73)
            black = 37;
        else
            black = (byte)i;

        bw.Write(black);
        bw.Write(black);
    }
}

using (BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\32xrb2\SRB2_TRANSTABLE_16.lmp", FileMode.Create)))
{
    for (int i = 0; i < 63; i += 2)
    {
        for (int j = 128; j < 256; j++)
        {
            bw.Write(colormapLights16[i + 1, j]);
            bw.Write(colormapLights16[i, j]);
        }
        for (int j = 0; j < 128; j++)
        {
            bw.Write(colormapLights16[i + 1, j]);
            bw.Write(colormapLights16[i, j]);
        }
    }
}