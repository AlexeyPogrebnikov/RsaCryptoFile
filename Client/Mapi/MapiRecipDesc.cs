using System;
using System.Runtime.InteropServices;

namespace CryptoFile.Client.Mapi {
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	class MapiRecipDesc {
		internal MapiRecipDesc(string name, RecipClass recipClass) {
			this.name = name;
			this.recipClass = (int)recipClass;

			unmanagedAddress = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MapiRecipDesc)));
			Marshal.StructureToPtr(this, unmanagedAddress, false);
		}

		internal IntPtr UnmanagedAddress {
			get { return unmanagedAddress; }
		}

		internal int reserved;
		internal int recipClass;
		internal string name;
		internal string address;
		internal int eIDSize;
		internal IntPtr entryID;

		private readonly IntPtr unmanagedAddress;

		~MapiRecipDesc() {
			Marshal.FreeHGlobal(unmanagedAddress);
		}
	}
}