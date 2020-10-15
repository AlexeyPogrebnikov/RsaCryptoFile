using System;
using System.Runtime.InteropServices;

namespace CryptoFile.Client.Mapi
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal class MapiRecipDesc
	{
		internal MapiRecipDesc(string name, RecipClass recipClass)
		{
			this.name = name;
			this.recipClass = (int) recipClass;

			UnmanagedAddress = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MapiRecipDesc)));
			Marshal.StructureToPtr(this, UnmanagedAddress, false);
		}

		internal IntPtr UnmanagedAddress { get; }

		internal int reserved;
		internal int recipClass;
		internal string name;
		internal string address;
		internal int eIDSize;
		internal IntPtr entryID;

		~MapiRecipDesc()
		{
			Marshal.FreeHGlobal(UnmanagedAddress);
		}
	}
}