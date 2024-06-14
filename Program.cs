// See https://aka.ms/new-console-template for more information
using PlaypalEdit;
using System.Drawing;
using System.Text;

Console.WriteLine("Hello, World!");

Map map = new Map();
map.PopulateFromDirectory_Jag(@"D:\32xrb2\Levels\MAP30");
map.WritePC(@"D:\32xrb2\Levels\MAP30\Jag");

map = new Map();
map.PopulateFromDirectory_Jag(@"D:\32xrb2\Levels\MAP01");
map.WritePC(@"D:\32xrb2\Levels\MAP01\Jag");

map = new Map();
map.PopulateFromDirectory_Jag(@"D:\32xrb2\Levels\E1M1");
map.WritePC(@"D:\32xrb2\Levels\E1M1\PC");
map = new Map();
map.PopulateFromDirectory_PC(@"D:\32xrb2\Levels\E1M1\PC");
map.WriteJaguar(@"D:\32xrb2\Levels\E1M1\JagTest");


return;

Demo d = new Demo();
d.Map = 30;
d.Skill = 2;

d.Buttons.Clear();
for (int i = 0; i < 15 * 22; i++)
{
    d.Buttons.Add(0);
}

// End marker
d.Buttons.Add(0x200000);
d.Buttons.Add(0x200000);
d.Buttons.Add(0x000020);
d.Buttons.Add(0x000020);

d.Write(@"D:\32xrb2\DEMO1.lmp");
return;

/*
Palette dumpPal = Palette.LoadJascPalette(@"D:\32xrb2\srbPal0.pal");

StringBuilder sb = new StringBuilder();
foreach (var pal in dumpPal.Entries)
{
    sb.AppendLine("{ " + pal.R + ", " + pal.G + ", " + pal.B + " },");
}

System.IO.File.WriteAllText(@"D:\32xrb2\22pal.txt", sb.ToString());



Map map = new Map();
map.PopulateFromDirectory_PC(@"D:\32xrb2\Levels\MAP01");
map.WriteJaguar(@"D:\32xrb2\Levels\MAP01\Jag");

Texture1 buildTex = new Texture1();
buildTex.PopulateFromDirectory(@"D:\32xrb2\Patches\BuildTex");

using (FileStream fs = new FileStream(@"D:\32xrb2\Patches\TEXTURE1.lmp", FileMode.Create, FileAccess.Write))
    buildTex.Write(fs);

return;

Map m = new Map();
m.PopulateFromDirectory_Jag("D:\\32xrb2\\comptest\\MAP03");
m.WritePC("D:\\32xrb2\\comptest\\MAP03\\export");

using (FileStream fs = new FileStream(@"D:\32xrb2\comptest\texture1.lmp", FileMode.Open, FileAccess.Read))
{
    Texture1 t = new Texture1();
    t.Read(fs);

    Console.WriteLine(t);
}
*/
Palette[] srb2pal = Palette.LoadPCPlaypal(@"D:\32xrb2\playpal.pal");

List<Palette> outputPals = new List<Palette>();
int palIndex = 0;
outputPals.Add(srb2pal[palIndex++]);

// Now compose the other palettes
/*
SRB2 should use:

0: Base
1-5: white flash (armageddon shield) (normal to white ramp)
6-10: black fade (normal to black ramp)
11: blue tint (water)
12: pause
*/

// white fade
PalEntry[] entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.8);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.6);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.4);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.2);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

// Completely white
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(255, 255, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.0);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

// Black fade
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.8);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.6);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.4);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.2);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

// Completely black
entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(0, 0, 0);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.0);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(128, 128, 255);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.5);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

entries = new PalEntry[256];
for (int i = 0; i < 256; i++)
{
    Color baseColor = Color.FromArgb(srb2pal[0].Entries[i].R, srb2pal[0].Entries[i].G, srb2pal[0].Entries[i].B);
    Color mixColor = Color.FromArgb(128, 128, 128);

    Color finalColor = Util.Blend(baseColor, mixColor, 0.5);

    entries[i] = new PalEntry(finalColor.R, finalColor.G, finalColor.B);
}
srb2pal[palIndex++] = new Palette(entries);

Palette[] output = new Palette[palIndex];
for (int i = 0; i < palIndex; i++)
    output[i] = srb2pal[i];

Palette.WriteJagPlaypal(output, @"D:\32xrb2\SRB2_PLAYPALS.lmp");
return;
byte[,] colormapLights = Colormap.BuildLights(srb2pal[0], 32);

using (BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\32xrb2\SRB2_COLORMAP.lmp", FileMode.Create)))
{
    for (int i = 0; i < colormapLights.GetLength(0); i++)
    {
        for (int j = 0; j < colormapLights.GetLength(1); j++)
        {
            bw.Write(colormapLights[i, j]);
        }
    }

    byte black = 0;
    for (int i = 0; i < 256; i++)
        bw.Write(black);
}


