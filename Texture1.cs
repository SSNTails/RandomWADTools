using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaypalEdit
{
    public class MapPatch
    {
        public short OriginX { get; set; }
        public short OriginY { get; set; }
        public short PatchIndex { get; set; }
        public short StepDir { get; set; }
        public short Colormap { get; set; }
    }

    public class MapTexture
    {
        public string Name { get; set; } // 8 bytes
        public bool Masked { get; set; } // 4 bytes
        public short Width { get; set; }
        public short Height { get; set; }
        public int ColumnDirectory { get; set; } // Ignored (4 bytes)
        public short PatchCount { get; set; } // Always 1 in Jaguar

        public MapPatch PatchInfo { get; set; } // Jag only has 1

        public int GetBinarySize()
        {
            int size = 22;
            size += 10; // Only 1 PatchInfo

            return size;
        }
    }

    public class Texture1
    {
        public int numTextures;
        public List<int> offset = new List<int>();

        public List<MapTexture> MTexture { get; set; } = new List<MapTexture>();

        static bool ContainsTransparency(System.Drawing.Bitmap b)
        {
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    System.Drawing.Color color = b.GetPixel(x, y);

                    if (color.R == 0 && color.G == 255 && color.B == 255)
                        return true;
                }
            }

            return false;
        }

        public void PopulateFromDirectory(string directory)
        {
            numTextures = 0;
            offset.Clear();
            MTexture.Clear();

            System.IO.DirectoryInfo info = new DirectoryInfo(directory);

            var fileInfos = info.GetFiles("*.png");

            short count = 0;
            foreach (var file in fileInfos)
            {
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(file.FullName);

                MapTexture tex = new MapTexture();
                MTexture.Add(tex);
                tex.Name = System.IO.Path.GetFileNameWithoutExtension(file.FullName);

                tex.Masked = ContainsTransparency(b);
                tex.Width = (short)b.Width;
                tex.Height = (short)b.Height;
                tex.ColumnDirectory = 0;
                tex.PatchCount = 1;

                tex.PatchInfo = new MapPatch();
                tex.PatchInfo.OriginX = 0;
                tex.PatchInfo.OriginY = 0;
                tex.PatchInfo.PatchIndex = count;
                tex.PatchInfo.StepDir = 1;
                tex.PatchInfo.Colormap = 0;
            }

            // Now we can set the offsets
            int offsetCursor = 4 + (4 * MTexture.Count);
            foreach (var tex in MTexture)
            {
                offset.Add(offsetCursor);
                offsetCursor += tex.GetBinarySize();
            }

            numTextures = MTexture.Count;
        }

        public void Read(Stream s)
        {
            offset.Clear();
            MTexture.Clear();

            using (BinaryReader br = new BinaryReader(s))
            {
                numTextures = br.ReadInt32();
                for (int i = 0; i < numTextures; i++)
                    offset.Add(br.ReadInt32());

                for (int i = 0; i < numTextures; i++)
                {
                    MapTexture t = new MapTexture();
                    MTexture.Add(t);
                    t.Name = new string(br.ReadChars(8));
                    t.Name = t.Name.Replace("\0", "");
                    t.Name = t.Name.Trim();
                    t.Masked = br.ReadInt32() > 0;
                    t.Width = br.ReadInt16();
                    t.Height = br.ReadInt16();
                    t.ColumnDirectory = br.ReadInt32();
                    t.PatchCount = br.ReadInt16();

                    t.PatchInfo = new MapPatch();
                    t.PatchInfo.OriginX = br.ReadInt16();
                    t.PatchInfo.OriginY = br.ReadInt16();
                    t.PatchInfo.PatchIndex = br.ReadInt16();
                    t.PatchInfo.StepDir = br.ReadInt16();
                    t.PatchInfo.Colormap = br.ReadInt16();
                }
            }
        }

        public void Write(Stream s)
        {
            using (BinaryWriter bw = new BinaryWriter(s))
            {
                bw.Write(numTextures);
                foreach (int of in offset)
                    bw.Write(of);

                foreach (MapTexture mt in MTexture)
                {
                    char[] name = new char[8];
                    for (int i = 0; i < 8; i++)
                        name[0] = '\0';
                    mt.Name.ToLower().ToCharArray().CopyTo(name, 0);

                    bw.Write(name);
                    bw.Write(mt.Masked ? (Int32)1 : (Int32)0);
                    bw.Write(mt.Width);
                    bw.Write(mt.Height);
                    bw.Write(mt.ColumnDirectory);
                    bw.Write(mt.PatchCount);

                    bw.Write(mt.PatchInfo.OriginX);
                    bw.Write(mt.PatchInfo.OriginY);
                    bw.Write(mt.PatchInfo.PatchIndex);
                    bw.Write(mt.PatchInfo.StepDir);
                    bw.Write(mt.PatchInfo.Colormap);
                }
            }
        }
    }
}
