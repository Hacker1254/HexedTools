// SPDX-License-Identifier: MIT
// Copyright (C) 2018-present iced project and contributors

// ⚠️This file was generated by GENERATOR!🦹‍♂️

#nullable enable

#if GAS || INTEL || MASM || NASM || FAST_FMT
namespace Iced.Intel {
	/// <summary>Formatter text kind</summary>
	public enum FormatterTextKind {
		/// <summary>Normal text</summary>
		Text = 0,
		/// <summary>Assembler directive</summary>
		Directive = 1,
		/// <summary>Any prefix</summary>
		Prefix = 2,
		/// <summary>Any mnemonic</summary>
		Mnemonic = 3,
		/// <summary>Any keyword</summary>
		Keyword = 4,
		/// <summary>Any operator</summary>
		Operator = 5,
		/// <summary>Any punctuation</summary>
		Punctuation = 6,
		/// <summary>Number</summary>
		Number = 7,
		/// <summary>Any register</summary>
		Register = 8,
		/// <summary>A decorator, eg. <c>sae</c> in <c>{sae}</c></summary>
		Decorator = 9,
		/// <summary>Selector value (eg. far <c>JMP</c>/<c>CALL</c>)</summary>
		SelectorValue = 10,
		/// <summary>Label address (eg. <c>JE XXXXXX</c>)</summary>
		LabelAddress = 11,
		/// <summary>Function address (eg. <c>CALL XXXXXX</c>)</summary>
		FunctionAddress = 12,
		/// <summary>Data symbol</summary>
		Data = 13,
		/// <summary>Label symbol</summary>
		Label = 14,
		/// <summary>Function symbol</summary>
		Function = 15,
	}
}
#endif