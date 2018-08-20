using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	[Serializable]
	class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public bool Male { get; set; }

		public Person() { } // Empty constructor for creating empty Person object

		public Person(byte[] data)
		{
			this.Male = BitConverter.ToBoolean(data, 0);
			this.Age = BitConverter.ToInt32(data, 1);
			this.Name = Encoding.ASCII.GetString(data, 5, data.Length - 5);
		}

		public Person(string name, int age, bool male)
		{
			this.Name = name;
			this.Age = age;
			this.Male = male;
		}

		public byte[] ObjectToByteArray(Object obj)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using (MemoryStream m = new MemoryStream())
			{
				binaryFormatter.Serialize(m, obj);
				return m.ToArray();
			}
		}

		public static Object ByteArrayToObject(byte[] data)
		{
			using (MemoryStream m = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				m.Write(data, 0, data.Length);
				m.Seek(0, SeekOrigin.Begin);
				Object obj = binaryFormatter.Deserialize(m);
				return obj;
			}
		}

		public byte[] Serialize()
		{
			using (MemoryStream m = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(m))
				{
					writer.Write(Male);
					writer.Write(Age);
					writer.Write(Name);
				}
				return m.ToArray();
			}
		}
		//Absolutely important to retain the same order in which they were serialized
		public static Person DeSerialize(byte[] data)
		{
			Person result = new Person();
			using (MemoryStream m = new MemoryStream(data))
			{
				using (BinaryReader reader = new BinaryReader(m))
				{
					result.Male = reader.ReadBoolean();
					result.Age = reader.ReadInt32();
					result.Name = reader.ReadString();
				}
				return result;
			}
		}

		public byte[] toByteArray()
		{
			List<byte> byteList = new List<byte>();

			byteList.AddRange(BitConverter.GetBytes(Male));
			byteList.AddRange(BitConverter.GetBytes(Age));
			byteList.AddRange(Encoding.ASCII.GetBytes(Name));

			return byteList.ToArray();
		}
	}
}
