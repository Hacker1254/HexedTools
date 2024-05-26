// SPDX-License-Identifier: MIT
// Copyright (C) 2018-present iced project and contributors

#if GAS
using System;
using Iced.Intel.FormatterInternal;
using Iced.Intel.Internal;

namespace Iced.Intel.GasFormatterInternal {
	static partial class InstrInfos {
		public static readonly InstrInfo[] AllInfos = ReadInfos();

		static string AddSuffix(string s, char[] ca) =>
			ca[0] == '\0' ? s : string.Intern(s + new string(ca));

		static string AddPrefix(string s, char[] ca) =>
			string.Intern(new string(ca) + s);

		static InstrInfo[] ReadInfos() {
			var reader = new DataReader(GetSerializedInstrInfos());
			var infos = new InstrInfo[IcedConstants.CodeEnumCount];
			var strings = FormatterStringsTable.GetStringsTable();

			var ca = new char[1];
			string s, s2, s3, s4, s5, s6;
			uint v, v2, v3;
			int prevIndex = -1;
			for (int i = 0; i < infos.Length; i++) {
				byte f = reader.ReadByte();
				var ctorKind = (CtorKind)(f & 0x7F);
				int currentIndex;
				if (ctorKind == CtorKind.Previous) {
					currentIndex = reader.Index;
					reader.Index = prevIndex;
					ctorKind = (CtorKind)(reader.ReadByte() & 0x7F);
				}
				else {
					currentIndex = -1;
					prevIndex = reader.Index - 1;
				}
				s = strings[reader.ReadCompressedUInt32()];
				if ((f & 0x80) != 0) {
					ca[0] = 'v';
					s = AddPrefix(s, ca);
				}
				InstrInfo instrInfo;
				switch (ctorKind) {
				case CtorKind.Normal_1:
					instrInfo = new SimpleInstrInfo(s);
					break;

				case CtorKind.Normal_2a:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					instrInfo = new SimpleInstrInfo(s, s2);
					break;

				case CtorKind.Normal_2b:
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo(s, (InstrOpInfoFlags)v);
					break;

				case CtorKind.Normal_2c:
					ca[0] = (char)reader.ReadByte();
					s = AddSuffix(s, ca);
					instrInfo = new SimpleInstrInfo(s);
					break;

				case CtorKind.Normal_3:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo(s, s2, (InstrOpInfoFlags)v);
					break;

				case CtorKind.AamAad:
					instrInfo = new SimpleInstrInfo_AamAad(s);
					break;

				case CtorKind.asz:
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_as((int)v, s);
					break;

				case CtorKind.bnd:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_bnd(s, s2, (InstrOpInfoFlags)v);
					break;

				case CtorKind.DeclareData:
					instrInfo = new SimpleInstrInfo_DeclareData((Code)i, s);
					break;

				case CtorKind.er_2:
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_er((int)v, s);
					break;

				case CtorKind.er_4:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_er((int)v, s, s2, (InstrOpInfoFlags)v2);
					break;

				case CtorKind.far:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_far((int)v, s, s2);
					break;

				case CtorKind.imul:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					instrInfo = new SimpleInstrInfo_imul(s, s2);
					break;

				case CtorKind.maskmovq:
					instrInfo = new SimpleInstrInfo_maskmovq(s);
					break;

				case CtorKind.movabs:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					s3 = strings[reader.ReadCompressedUInt32()];
					s4 = AddSuffix(s3, ca);
					instrInfo = new SimpleInstrInfo_movabs(s, s2, s3, s4);
					break;

				case CtorKind.nop:
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadByte();
					instrInfo = new SimpleInstrInfo_nop((int)v, s, (Register)v2);
					break;

				case CtorKind.OpSize:
					v = reader.ReadByte();
					s2 = string.Intern(s + "w");
					s3 = string.Intern(s + "l");
					s4 = string.Intern(s + "q");
					instrInfo = new SimpleInstrInfo_OpSize((CodeSize)v, s, s2, s3, s4);
					break;

				case CtorKind.OpSize2_bnd:
					s2 = strings[reader.ReadCompressedUInt32()];
					s3 = strings[reader.ReadCompressedUInt32()];
					s4 = strings[reader.ReadCompressedUInt32()];
					instrInfo = new SimpleInstrInfo_OpSize2_bnd(s, s2, s3, s4);
					break;

				case CtorKind.OpSize3:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_OpSize3((int)v, s, s2);
					break;

				case CtorKind.os:
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadByte();
					if (v2 > 1)
						throw new InvalidOperationException();
					v3 = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os((int)v, s, v2 != 0, (InstrOpInfoFlags)v3);
					break;

				case CtorKind.CC_1:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_cc((int)v, new[] { s }, new[] { s2 });
					break;

				case CtorKind.CC_2:
					s2 = strings[reader.ReadCompressedUInt32()];
					ca[0] = (char)reader.ReadByte();
					v = reader.ReadCompressedUInt32();
					s3 = AddSuffix(s, ca);
					s4 = AddSuffix(s2, ca);
					instrInfo = new SimpleInstrInfo_cc((int)v, new[] { s, s2 }, new[] { s3, s4 });
					break;

				case CtorKind.CC_3:
					s2 = strings[reader.ReadCompressedUInt32()];
					s3 = strings[reader.ReadCompressedUInt32()];
					ca[0] = (char)reader.ReadByte();
					v = reader.ReadCompressedUInt32();
					s4 = AddSuffix(s, ca);
					s5 = AddSuffix(s2, ca);
					s6 = AddSuffix(s3, ca);
					instrInfo = new SimpleInstrInfo_cc((int)v, new[] { s, s2, s3 }, new[] { s4, s5, s6 });
					break;

				case CtorKind.os_jcc_1:
					v2 = reader.ReadCompressedUInt32();
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_jcc((int)v, (int)v2, new[] { s });
					break;

				case CtorKind.os_jcc_2:
					s2 = strings[reader.ReadCompressedUInt32()];
					v2 = reader.ReadCompressedUInt32();
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_jcc((int)v, (int)v2, new[] { s, s2 });
					break;

				case CtorKind.os_jcc_3:
					s2 = strings[reader.ReadCompressedUInt32()];
					s3 = strings[reader.ReadCompressedUInt32()];
					v2 = reader.ReadCompressedUInt32();
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_jcc((int)v, (int)v2, new[] { s, s2, s3 });
					break;

				case CtorKind.os_loopcc:
					s2 = strings[reader.ReadCompressedUInt32()];
					ca[0] = (char)reader.ReadByte();
					s3 = AddSuffix(s, ca);
					s4 = AddSuffix(s2, ca);
					v3 = reader.ReadCompressedUInt32();
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_loop((int)v, (int)v2, (int)v3, new[] { s, s2 }, new[] { s3, s4 });
					break;

				case CtorKind.os_loop:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_loop((int)v, (int)v2, -1, new[] { s }, new[] { s2 });
					break;

				case CtorKind.os_mem:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_mem((int)v, s, s2);
					break;

				case CtorKind.Reg16:
					ca[0] = 'w';
					s2 = AddSuffix(s, ca);
					instrInfo = new SimpleInstrInfo_Reg16(s, s2);
					break;

				case CtorKind.os_mem2:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os_mem2((int)v, s, s2);
					break;

				case CtorKind.os2_3:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadByte();
					if (v2 > 1)
						throw new InvalidOperationException();
					instrInfo = new SimpleInstrInfo_os2((int)v, s, s2, v2 != 0, InstrOpInfoFlags.None);
					break;

				case CtorKind.os2_4:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					v = reader.ReadCompressedUInt32();
					v2 = reader.ReadByte();
					if (v2 > 1)
						throw new InvalidOperationException();
					v3 = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_os2((int)v, s, s2, v2 != 0, (InstrOpInfoFlags)v3);
					break;

				case CtorKind.pblendvb:
					instrInfo = new SimpleInstrInfo_pblendvb(s);
					break;

				case CtorKind.pclmulqdq:
					v = reader.ReadByte();
					instrInfo = new SimpleInstrInfo_pclmulqdq(s, FormatterConstants.GetPseudoOps((PseudoOpsKind)v));
					break;

				case CtorKind.pops:
					v = reader.ReadByte();
					v2 = reader.ReadByte();
					if (v2 > 1)
						throw new InvalidOperationException();
					instrInfo = new SimpleInstrInfo_pops(s, FormatterConstants.GetPseudoOps((PseudoOpsKind)v), v2 != 0);
					break;

				case CtorKind.mem16:
					ca[0] = (char)reader.ReadByte();
					s2 = AddSuffix(s, ca);
					ca[0] = 'w';
					s3 = AddSuffix(s, ca);
					instrInfo = new SimpleInstrInfo_mem16(s, s2, s3);
					break;

				case CtorKind.Reg32:
					instrInfo = new SimpleInstrInfo_Reg32(s);
					break;

				case CtorKind.sae:
					v = reader.ReadCompressedUInt32();
					instrInfo = new SimpleInstrInfo_sae((int)v, s);
					break;

				case CtorKind.ST_STi:
					instrInfo = new SimpleInstrInfo_ST_STi(s);
					break;

				case CtorKind.STi_ST:
					v = reader.ReadByte();
					if (v > 1)
						throw new InvalidOperationException();
					instrInfo = new SimpleInstrInfo_STi_ST(s, v != 0);
					break;

				case CtorKind.STIG1:
					v = reader.ReadByte();
					if (v > 1)
						throw new InvalidOperationException();
					instrInfo = new SimpleInstrInfo_STIG1(s, v != 0);
					break;

				default:
					throw new InvalidOperationException();
				}
				infos[i] = instrInfo;
				if (currentIndex >= 0)
					reader.Index = currentIndex;
			}
			if (reader.CanRead)
				throw new InvalidOperationException();

			return infos;
		}
	}
}
#endif
