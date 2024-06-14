using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaypalEdit
{
    public class Demo
    {
        public uint Skill { get; set; }
        public uint Map { get; set; }
        public List<uint> Buttons { get; set; } = new List<uint>();

        uint swap32u(uint i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            return BitConverter.ToUInt32(bytes.Reverse().ToArray());
        }

        public void Read(string filename)
        {
            Buttons = new List<uint>();
            using (BinaryReader br = new BinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
            {
                Skill = swap32u(br.ReadUInt32());
                Map = swap32u(br.ReadUInt32());

                try
                {
                    while (true)
                        Buttons.Add(swap32u(br.ReadUInt32()));
                }
                catch (Exception)
                {
                }
            }
        }

        public void Write(string filename)
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream(filename, FileMode.Create, FileAccess.Write)))
            {
                bw.Write(swap32u(Skill));
                bw.Write(swap32u(Map));

                foreach (uint tic in Buttons)
                    bw.Write(swap32u(tic));
            }
        }
    }
}
