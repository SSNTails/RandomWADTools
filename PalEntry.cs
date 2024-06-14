using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaypalEdit
{
    public class Palette
    {
        public PalEntry[] Entries;

        public Palette(PalEntry[] entries)
        {
            this.Entries = entries;
        }

        public static Palette LoadJascPalette(string jascPal)
        {
            List<PalEntry> entries = new List<PalEntry>();
            using (TextReader tr = new StreamReader(jascPal))
            {
                while (true)
                {
                    string line = tr.ReadLine();

                    if (line == null)
                        break;

                    if (line == "JASC-PAL")
                    {
                        tr.ReadLine(); // Version #
                        tr.ReadLine(); // # of lines (always 256 in this case)
                        continue;
                    }

                    var tokens = line.Split(' ');
                    PalEntry p = new PalEntry();
                    p.R = byte.Parse(tokens[0]);
                    p.G = byte.Parse(tokens[1]);
                    p.B = byte.Parse(tokens[2]);
                    entries.Add(p);
                }
            }

            return new Palette(entries.ToArray());
        }

        public static Palette[] LoadPCPlaypal(string pcPlaypal)
        {
            List<Palette> palettes = new List<Palette>();

            using (StreamReader sr = new StreamReader(pcPlaypal))
            using (BinaryReader br = new BinaryReader(sr.BaseStream))
            {
                for (int i = 0; i < 14; i++)
                {
                    List<PalEntry> entries = new List<PalEntry>();

                    for (int j = 0; j < 256; j++)
                    {
                        PalEntry p = new PalEntry();
                        p.R = br.ReadByte();
                        p.G = br.ReadByte();
                        p.B = br.ReadByte();
                        entries.Add(p);
                    }

                    palettes.Add(new Palette(entries.ToArray()));
                }
            }

            return palettes.ToArray();
        }

        public static Palette[] LoadJagPlaypal(string jagPlaypal)
        {
            List<Palette> palettes = new List<Palette>();

            using (StreamReader sr = new StreamReader(jagPlaypal))
            using (BinaryReader br = new BinaryReader(sr.BaseStream))
            {
                for (int i = 0; i < 15; i++)
                {
                    List<PalEntry> entries = new List<PalEntry>();

                    for (int j = 0; j < 256; j++)
                    {
                        PalEntry p = new PalEntry();
                        p.R = br.ReadByte();
                        p.G = br.ReadByte();
                        p.B = br.ReadByte();
                        entries.Add(p);
                    }

                    palettes.Add(new Palette(entries.ToArray()));
                }
            }

            return palettes.ToArray();
        }

        public void WriteJasc(string jascPal)
        {
            using (TextWriter tw = new StreamWriter(jascPal))
            {
                tw.WriteLine("JASC-PAL");
                tw.WriteLine("0100");
                tw.WriteLine(this.Entries.Length.ToString());

                for (int i = 0; i < this.Entries.Length; i++)
                {
                    tw.WriteLine(this.Entries[i].R.ToString() + " " + this.Entries[i].G.ToString() + " " + this.Entries[i].B.ToString());
                }
            }
        }

        public static void WriteJagPlaypal(Palette[] pal, string jagPlaypal)
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream(jagPlaypal, FileMode.Create)))
            {
                for (int i = 0; i < pal.Length; i++)
                {
                    for (int j = 0; j < pal[i].Entries.Length; j++)
                    {
                        bw.Write(pal[i].Entries[j].R);
                        bw.Write(pal[i].Entries[j].G);
                        bw.Write(pal[i].Entries[j].B);
                    }
                }
            }
        }
    }

    public class PalEntry
    {
        public byte R, G, B;

        public PalEntry(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public PalEntry()
        {
        }
    }
}
