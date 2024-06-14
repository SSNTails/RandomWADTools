using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlaypalEdit
{
    public class Map_Thing
    {
        public short X { get; set; }
        public short Y { get; set; }
        public short Angle { get; set; }
        public short Type { get; set; }
        public short Options { get; set; }
    }

    public class Map_Linedef
    {
        public short V1 { get; set; }
        public short V2 { get; set; }
        public short Flags { get; set; }
        public short Special { get; set; }
        public short Tag { get; set; }
        public short[] Sidenum { get; set; } = new short[2];
    }

    public class Map_Sidedef
    {
        public short Textureoffset { get; set; }
        public short Rowoffset { get; set; }
        public string Toptexture { get; set; }
        public string Bottomtexture { get; set; }
        public string Midtexture { get; set; }
        public short Sector { get; set; }
    }

    public class Map_Vertex
    {
        public short X { get; set; }
        public short Y { get; set; }
    }

    public class Map_Seg
    {
        public short V1 { get; set; }
        public short V2 { get; set; }
        public short Angle { get; set; }
        public short Linedef { get; set; }
        public short Side { get; set; }
        public short Offset { get; set; }
    }

    public class Map_SSector
    {
        public short Numsegs { get; set; }
        public short Firstseg { get; set; }
    }

    public class Map_Node
    {
        public short X { get; set; }
        public short Y { get; set; }
        public short DX { get; set; }
        public short DY { get; set; }
        public short[,] BBox { get; set; } = new short[2, 4];

        public ushort[] Children { get; set; } = new ushort[2];
    }

    public class Map_Node_Jag
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int DX { get; set; }
        public int DY { get; set; }
        public int[,] BBox { get; set; } = new int[2, 4];
        public int[] Children { get; set; } = new int[2];
    }

    public class Map_Sector
    {
        public short Floorheight { get; set; }
        public short Ceilingheight { get; set; }
        public string Floorpic { get; set; }
        public string Ceilingpic { get; set; }
        public short Lightlevel { get; set; }
        public short Special { get; set; }
        public short Tag { get; set; }
    }

    public class Map_Reject
    {
    }

    public class Map_Blockmap
    {
    }

    internal class Map
    {
        public string Name { get; set; }
        public List<Map_Thing> Things { get; set; } = new List<Map_Thing>();
        public List<Map_Linedef> Linedefs { get; set; } = new List<Map_Linedef>();
        public List<Map_Sidedef> Sidedefs { get; set; } = new List<Map_Sidedef>();
        public List<Map_Vertex> Vertexes { get; set; } = new List<Map_Vertex>();
        public List<Map_Seg> Segs { get; set; } = new List<Map_Seg>();
        public List<Map_SSector> SubSectors { get; set; } = new List<Map_SSector>();
        public List<Map_Node> Nodes { get; set; } = new List<Map_Node>();
        public List<Map_Node_Jag> Nodes_Jag { get; set; } = new List<Map_Node_Jag>();
        public List<Map_Sector> Sectors { get; set; } = new List<Map_Sector>();
        public byte[] Reject { get; set; }
        public short[] Blockmap { get; set; }

        ushort swap16u(ushort i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            return BitConverter.ToUInt16(bytes.Reverse().ToArray());
        }

        short swap16(short i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            return BitConverter.ToInt16(bytes.Reverse().ToArray());
        }

        int swap32(int i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            return BitConverter.ToInt32(bytes.Reverse().ToArray());
        }

        const int FRACBITS = 16;

        // Same format, just big endian'd
        public void WriteJaguar(string directory)
        {
            // THINGS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "THINGS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var thing in Things)
                {
                    bw.Write(thing.X);
                    bw.Write(thing.Y);
                    bw.Write(thing.Angle);
                    bw.Write(thing.Type);
                    bw.Write(thing.Options);
                }
            }

            // LINEDEFS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "LINEDEFS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var ld in Linedefs)
                {
                    bw.Write(ld.V1);
                    bw.Write(ld.V2);
                    bw.Write(ld.Flags);
                    bw.Write(ld.Special);
                    bw.Write(ld.Tag);
                    bw.Write(ld.Sidenum[0]);
                    bw.Write(ld.Sidenum[1]);
                }
            }

            // SIDEDEFS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SIDEDEFS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var s in Sidedefs)
                {
                    bw.Write(s.Textureoffset);
                    bw.Write(s.Rowoffset);
                    bw.Write(Create8FromString(s.Toptexture));
                    bw.Write(Create8FromString(s.Bottomtexture));
                    bw.Write(Create8FromString(s.Midtexture));
                    bw.Write(s.Sector);
                }
            }

            // VERTEXES
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "VERTEXES.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var v in Vertexes)
                {
                    bw.Write(swap32(v.X << FRACBITS));
                    bw.Write(swap32(v.Y << FRACBITS));
                }
            }

            // SEGS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SEGS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var s in Segs)
                {
                    bw.Write(s.V1);
                    bw.Write(s.V2);
                    bw.Write(s.Angle);
                    bw.Write(s.Linedef);
                    bw.Write(s.Side);
                    bw.Write(s.Offset);
                }
            }

            // SSECTORS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SSECTORS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var ss in SubSectors)
                {
                    bw.Write(ss.Numsegs);
                    bw.Write(ss.Firstseg);
                }
            }

            // NODES
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "NODES.lmp"), FileMode.Create, FileAccess.Write)))
            {
                if (Nodes.Count > 0) // PC
                {
                    foreach (var n in Nodes)
                    {
                        bw.Write(swap32(n.X << FRACBITS));
                        bw.Write(swap32(n.Y << FRACBITS));
                        bw.Write(swap32(n.DX << FRACBITS));
                        bw.Write(swap32(n.DY << FRACBITS));
                        bw.Write(swap32(n.BBox[0, 0] << FRACBITS));
                        bw.Write(swap32(n.BBox[0, 1] << FRACBITS));
                        bw.Write(swap32(n.BBox[0, 2] << FRACBITS));
                        bw.Write(swap32(n.BBox[0, 3] << FRACBITS));
                        bw.Write(swap32(n.BBox[1, 0] << FRACBITS));
                        bw.Write(swap32(n.BBox[1, 1] << FRACBITS));
                        bw.Write(swap32(n.BBox[1, 2] << FRACBITS));
                        bw.Write(swap32(n.BBox[1, 3] << FRACBITS));
                        bw.Write(swap32(n.Children[0]));
                        bw.Write(swap32(n.Children[1]));
                    }
                }
                else if (Nodes_Jag.Count > 0) // Jag
                {
                    foreach (var n in Nodes_Jag)
                    {
                        bw.Write(swap32(n.X));
                        bw.Write(swap32(n.Y));
                        bw.Write(swap32(n.DX));
                        bw.Write(swap32(n.DY));
                        bw.Write(swap32(n.BBox[0, 0]));
                        bw.Write(swap32(n.BBox[0, 1]));
                        bw.Write(swap32(n.BBox[0, 2]));
                        bw.Write(swap32(n.BBox[0, 3]));
                        bw.Write(swap32(n.BBox[1, 0]));
                        bw.Write(swap32(n.BBox[1, 1]));
                        bw.Write(swap32(n.BBox[1, 2]));
                        bw.Write(swap32(n.BBox[1, 3]));
                        bw.Write(swap32(n.Children[0]));
                        bw.Write(swap32(n.Children[1]));
                    }
                }
            }

            // SECTORS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SECTORS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var s in Sectors)
                {
                    bw.Write(s.Floorheight);
                    bw.Write(s.Ceilingheight);
                    bw.Write(Create8FromString(s.Floorpic));
                    bw.Write(Create8FromString(s.Ceilingpic));
                    bw.Write(s.Lightlevel);
                    bw.Write(s.Special);
                    bw.Write(s.Tag);
                }
            }

            // REJECT
            System.IO.File.WriteAllBytes(Path.Combine(directory, "REJECT.lmp"), Reject);

            // BLOCKMAP
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "BLOCKMAP.lmp"), FileMode.Create, FileAccess.Write)))
            {
                for (int i = 0; i < Blockmap.Length; i++)
                    bw.Write(swap16(Blockmap[i]));
            }
        }

        public void WritePC(string directory)
        {
            // THINGS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "THINGS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var thing in Things)
                {
                    bw.Write(thing.X);
                    bw.Write(thing.Y);
                    bw.Write(thing.Angle);
                    bw.Write(thing.Type);
                    bw.Write(thing.Options);
                }
            }

            // LINEDEFS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "LINEDEFS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var ld in Linedefs)
                {
                    bw.Write(ld.V1);
                    bw.Write(ld.V2);
                    bw.Write(ld.Flags);
                    bw.Write(ld.Special);
                    bw.Write(ld.Tag);
                    bw.Write(ld.Sidenum[0]);
                    bw.Write(ld.Sidenum[1]);
                }
            }

            // SIDEDEFS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SIDEDEFS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var s in Sidedefs)
                {
                    bw.Write(s.Textureoffset);
                    bw.Write(s.Rowoffset);
                    bw.Write(Create8FromString(s.Toptexture));
                    bw.Write(Create8FromString(s.Bottomtexture));
                    bw.Write(Create8FromString(s.Midtexture));
                    bw.Write(s.Sector);
                }
            }

            // VERTEXES
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "VERTEXES.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var v in Vertexes)
                {
                    bw.Write(v.X);
                    bw.Write(v.Y);
                }
            }

            // SEGS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SEGS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var s in Segs)
                {
                    bw.Write(s.V1);
                    bw.Write(s.V2);
                    bw.Write(s.Angle);
                    bw.Write(s.Linedef);
                    bw.Write(s.Side);
                    bw.Write(s.Offset);
                }
            }

            // SSECTORS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SSECTORS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var ss in SubSectors)
                {
                    bw.Write(ss.Numsegs);
                    bw.Write(ss.Firstseg);
                }
            }

            // NODES
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "NODES.lmp"), FileMode.Create, FileAccess.Write)))
            {
                if (Nodes.Count > 0) // PC
                {
                    foreach (var n in Nodes)
                    {
                        bw.Write(n.X);
                        bw.Write(n.Y);
                        bw.Write(n.DX);
                        bw.Write(n.DY);
                        bw.Write(n.BBox[0, 0]);
                        bw.Write(n.BBox[0, 1]);
                        bw.Write(n.BBox[0, 2]);
                        bw.Write(n.BBox[0, 3]);
                        bw.Write(n.BBox[1, 0]);
                        bw.Write(n.BBox[1, 1]);
                        bw.Write(n.BBox[1, 2]);
                        bw.Write(n.BBox[1, 3]);
                        bw.Write(n.Children[0]);
                        bw.Write(n.Children[1]);
                    }
                }
                else if (Nodes_Jag.Count > 0) // Jag
                {
                    foreach (var n in Nodes_Jag)
                    {
                        bw.Write((short)(n.X >> FRACBITS));
                        bw.Write((short)(n.Y >> FRACBITS));
                        bw.Write((short)(n.DX >> FRACBITS));
                        bw.Write((short)(n.DY >> FRACBITS));
                        bw.Write((short)(n.BBox[0, 0] >> FRACBITS));
                        bw.Write((short)(n.BBox[0, 1] >> FRACBITS));
                        bw.Write((short)(n.BBox[0, 2] >> FRACBITS));
                        bw.Write((short)(n.BBox[0, 3] >> FRACBITS));
                        bw.Write((short)(n.BBox[1, 0] >> FRACBITS));
                        bw.Write((short)(n.BBox[1, 1] >> FRACBITS));
                        bw.Write((short)(n.BBox[1, 2] >> FRACBITS));
                        bw.Write((short)(n.BBox[1, 3] >> FRACBITS));
                        bw.Write((ushort)(n.Children[0]));
                        bw.Write((ushort)(n.Children[1]));
                    }
                }
            }

            // SECTORS
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "SECTORS.lmp"), FileMode.Create, FileAccess.Write)))
            {
                foreach (var s in Sectors)
                {
                    bw.Write(s.Floorheight);
                    bw.Write(s.Ceilingheight);
                    bw.Write(Create8FromString(s.Floorpic));
                    bw.Write(Create8FromString(s.Ceilingpic));
                    bw.Write(s.Lightlevel);
                    bw.Write(s.Special);
                    bw.Write(s.Tag);
                }
            }

            // REJECT
            System.IO.File.WriteAllBytes(Path.Combine(directory, "REJECT.lmp"), Reject);

            // BLOCKMAP
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(directory, "BLOCKMAP.lmp"), FileMode.Create, FileAccess.Write)))
            {
                for (int i = 0; i < Blockmap.Length; i++)
                    bw.Write(Blockmap[i]);
            }
        }

        char[] Create8FromString(string value)
        {
            char[] writeBuffer = new char[8];
            value.ToCharArray().CopyTo(writeBuffer, 0);

            int i = value.Length;
            while (i < 8)
            {
                writeBuffer[i] = '\0';
                i++;
            }

            return writeBuffer;
        }

        public void PopulateFromDirectory_Jag(string directory)
        {
            Name = System.IO.Path.GetDirectoryName(directory);

            // Load THINGS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "THINGS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Things.Clear();
                    while (true)
                    {
                        Map_Thing thing = new Map_Thing();
                        thing.X = br.ReadInt16();
                        thing.Y = br.ReadInt16();
                        thing.Angle = br.ReadInt16();
                        thing.Type = br.ReadInt16();
                        thing.Options = br.ReadInt16();
                        Things.Add(thing);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load LINEDEFS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "LINEDEFS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Linedefs.Clear();
                    while (true)
                    {
                        Map_Linedef linedef = new Map_Linedef();
                        linedef.V1 = br.ReadInt16();
                        linedef.V2 = br.ReadInt16();
                        linedef.Flags = br.ReadInt16();
                        linedef.Special = br.ReadInt16();
                        linedef.Tag = br.ReadInt16();
                        linedef.Sidenum[0] = br.ReadInt16();
                        linedef.Sidenum[1] = br.ReadInt16();
                        Linedefs.Add(linedef);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SIDEDEFS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SIDEDEFS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Sidedefs.Clear();
                    while (true)
                    {
                        Map_Sidedef sidedef = new Map_Sidedef();
                        sidedef.Textureoffset = br.ReadInt16();
                        sidedef.Rowoffset = br.ReadInt16();
                        sidedef.Toptexture = new string(br.ReadChars(8));
                        sidedef.Bottomtexture = new string(br.ReadChars(8));
                        sidedef.Midtexture = new string(br.ReadChars(8));
                        sidedef.Sector = br.ReadInt16();
                        Sidedefs.Add(sidedef);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load VERTEXES
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "VERTEXES.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Vertexes.Clear();
                    while (true)
                    {
                        Map_Vertex vertex = new Map_Vertex();
                        vertex.X = (short)(swap32(br.ReadInt32()) >> FRACBITS);
                        vertex.Y = (short)(swap32(br.ReadInt32()) >> FRACBITS);
                        Vertexes.Add(vertex);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SEGS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SEGS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Segs.Clear();
                    while (true)
                    {
                        Map_Seg seg = new Map_Seg();
                        seg.V1 = br.ReadInt16();
                        seg.V2 = br.ReadInt16();
                        seg.Angle = br.ReadInt16();
                        seg.Linedef = br.ReadInt16();
                        seg.Side = br.ReadInt16();
                        seg.Offset = br.ReadInt16();
                        Segs.Add(seg);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SSECTORS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SSECTORS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    SubSectors.Clear();
                    while (true)
                    {
                        Map_SSector ssector = new Map_SSector();
                        ssector.Numsegs = br.ReadInt16();
                        ssector.Firstseg = br.ReadInt16();
                        SubSectors.Add(ssector);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load NODES
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "NODES.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Nodes.Clear();
                    Nodes_Jag.Clear();
                    while (true)
                    {
                        Map_Node_Jag node = new Map_Node_Jag();
                        node.X = swap32(br.ReadInt32());
                        node.Y = swap32(br.ReadInt32());
                        node.DX = swap32(br.ReadInt32());
                        node.DY = swap32(br.ReadInt32());
                        node.BBox[0, 0] = swap32(br.ReadInt32());
                        node.BBox[0, 1] = swap32(br.ReadInt32());
                        node.BBox[0, 2] = swap32(br.ReadInt32());
                        node.BBox[0, 3] = swap32(br.ReadInt32());
                        node.BBox[1, 0] = swap32(br.ReadInt32());
                        node.BBox[1, 1] = swap32(br.ReadInt32());
                        node.BBox[1, 2] = swap32(br.ReadInt32());
                        node.BBox[1, 3] = swap32(br.ReadInt32());
                        node.Children[0] = swap32(br.ReadInt32());
                        node.Children[1] = swap32(br.ReadInt32());
                        Nodes_Jag.Add(node);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SECTORS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SECTORS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Sectors.Clear();
                    while (true)
                    {
                        Map_Sector sector = new Map_Sector();
                        sector.Floorheight = br.ReadInt16();
                        sector.Ceilingheight = br.ReadInt16();
                        sector.Floorpic = new string(br.ReadChars(8));
                        sector.Ceilingpic = new string(br.ReadChars(8));
                        sector.Lightlevel = br.ReadInt16();
                        sector.Special = br.ReadInt16();
                        sector.Tag = br.ReadInt16();
                        Sectors.Add(sector);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load REJECT
            Reject = System.IO.File.ReadAllBytes(Path.Combine(directory, "REJECT.lmp"));

            // Load BLOCKMAP
            byte[] bmapdata = System.IO.File.ReadAllBytes(Path.Combine(directory, "BLOCKMAP.lmp"));

            Blockmap = new short[bmapdata.Length / 2];
            Buffer.BlockCopy(bmapdata, 0, Blockmap, 0, bmapdata.Length);

            for (int i = 0; i < Blockmap.Length; i++)
                Blockmap[i] = swap16(Blockmap[i]);
        }

        public void PopulateFromDirectory_PC(string directory)
        {
            Name = System.IO.Path.GetDirectoryName(directory);

            // Load THINGS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "THINGS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Things.Clear();
                    while (true)
                    {
                        Map_Thing thing = new Map_Thing();
                        thing.X = br.ReadInt16();
                        thing.Y = br.ReadInt16();
                        thing.Angle = br.ReadInt16();
                        thing.Type = br.ReadInt16();
                        thing.Options = br.ReadInt16();
                        Things.Add(thing);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load LINEDEFS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "LINEDEFS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Linedefs.Clear();
                    while (true)
                    {
                        Map_Linedef linedef = new Map_Linedef();
                        linedef.V1 = br.ReadInt16();
                        linedef.V2 = br.ReadInt16();
                        linedef.Flags = br.ReadInt16();
                        linedef.Special = br.ReadInt16();
                        linedef.Tag = br.ReadInt16();
                        linedef.Sidenum[0] = br.ReadInt16();
                        linedef.Sidenum[1] = br.ReadInt16();
                        Linedefs.Add(linedef);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SIDEDEFS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SIDEDEFS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Sidedefs.Clear();
                    while (true)
                    {
                        Map_Sidedef sidedef = new Map_Sidedef();
                        sidedef.Textureoffset = br.ReadInt16();
                        sidedef.Rowoffset = br.ReadInt16();
                        sidedef.Toptexture = new string(br.ReadChars(8));
                        sidedef.Bottomtexture = new string(br.ReadChars(8));
                        sidedef.Midtexture = new string(br.ReadChars(8));
                        sidedef.Sector = br.ReadInt16();
                        Sidedefs.Add(sidedef);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load VERTEXES
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "VERTEXES.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Vertexes.Clear();
                    while (true)
                    {
                        Map_Vertex vertex = new Map_Vertex();
                        vertex.X = br.ReadInt16();
                        vertex.Y = br.ReadInt16();
                        Vertexes.Add(vertex);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SEGS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SEGS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Segs.Clear();
                    while (true)
                    {
                        Map_Seg seg = new Map_Seg();
                        seg.V1 = br.ReadInt16();
                        seg.V2 = br.ReadInt16();
                        seg.Angle = br.ReadInt16();
                        seg.Linedef = br.ReadInt16();
                        seg.Side = br.ReadInt16();
                        seg.Offset = br.ReadInt16();
                        Segs.Add(seg);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SSECTORS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SSECTORS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    SubSectors.Clear();
                    while (true)
                    {
                        Map_SSector ssector = new Map_SSector();
                        ssector.Numsegs = br.ReadInt16();
                        ssector.Firstseg = br.ReadInt16();
                        SubSectors.Add(ssector);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load NODES
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "NODES.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Nodes.Clear();
                    while (true)
                    {
                        Map_Node node = new Map_Node();
                        node.X = br.ReadInt16();
                        node.Y = br.ReadInt16();
                        node.DX = br.ReadInt16();
                        node.DY = br.ReadInt16();
                        node.BBox[0, 0] = br.ReadInt16();
                        node.BBox[0, 1] = br.ReadInt16();
                        node.BBox[0, 2] = br.ReadInt16();
                        node.BBox[0, 3] = br.ReadInt16();
                        node.BBox[1, 0] = br.ReadInt16();
                        node.BBox[1, 1] = br.ReadInt16();
                        node.BBox[1, 2] = br.ReadInt16();
                        node.BBox[1, 3] = br.ReadInt16();
                        node.Children[0] = br.ReadUInt16();
                        node.Children[1] = br.ReadUInt16();
                        Nodes.Add(node);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load SECTORS
            using (BinaryReader br = new BinaryReader(new FileStream(Path.Combine(directory, "SECTORS.lmp"), FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    Sectors.Clear();
                    while (true)
                    {
                        Map_Sector sector = new Map_Sector();
                        sector.Floorheight = br.ReadInt16();
                        sector.Ceilingheight = br.ReadInt16();
                        sector.Floorpic = new string(br.ReadChars(8));
                        sector.Ceilingpic = new string(br.ReadChars(8));
                        sector.Lightlevel = br.ReadInt16();
                        sector.Special = br.ReadInt16();
                        sector.Tag = br.ReadInt16();
                        Sectors.Add(sector);
                    }
                }
                catch (Exception)
                {
                }
            }

            // Load REJECT
            Reject = System.IO.File.ReadAllBytes(Path.Combine(directory, "REJECT.lmp"));

            // Load BLOCKMAP
            byte[] bmapdata = System.IO.File.ReadAllBytes(Path.Combine(directory, "BLOCKMAP.lmp"));

            Blockmap = new short[bmapdata.Length / 2];
            Buffer.BlockCopy(bmapdata, 0, Blockmap, 0, bmapdata.Length);
        }
    }
}
