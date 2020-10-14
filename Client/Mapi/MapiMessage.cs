using System;
using System.Runtime.InteropServices;

namespace CryptoFile.Client.Mapi {
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	class MapiMessage {
		internal MapiMessage(string subject, string noteText, string recipient) {
			this.subject = subject;
			this.noteText = noteText;
			recipCount = 1;
			recipDesc = new MapiRecipDesc(recipient, RecipClass.MAPI_TO);
			recips = recipDesc.UnmanagedAddress;
		}

		internal void OpenInMailClient() {
			var result = MAPISendMail(IntPtr.Zero, IntPtr.Zero, this, MAPI_LOGON_UI | MAPI_DIALOG, 0);
			if (result != (int)MapiResult.SUCCESS_SUCCESS && result != (int)MapiResult.MAPI_E_USER_ABORT)
				throw new MapiException(result);
		}

		internal int reserved;
		internal string subject;
		internal string noteText;
		internal string messageType;
		internal string dateReceived;
		internal string conversationID;
		internal int flags;
		internal IntPtr originator;
		internal int recipCount;
		internal IntPtr recips;
		internal int fileCount;
		internal IntPtr files;

		[DllImport("MAPI32.DLL")]
		private static extern int MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, int flg, int rsv);

		private readonly MapiRecipDesc recipDesc;
		private const int MAPI_LOGON_UI = 1;
		private const int MAPI_DIALOG = 8;
	}
}