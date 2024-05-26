// SPDX-License-Identifier: MIT
// Copyright (C) 2018-present iced project and contributors

using System;
using System.Diagnostics.CodeAnalysis;

namespace Iced.Intel {
	static class ThrowHelper {
		// NOTE: NoInlining is not used because RyuJIT doesn't move the method call to the end of the caller's method

		internal static void ThrowArgumentException() => throw new ArgumentException();

		internal static void ThrowInvalidOperationException() => throw new InvalidOperationException();

		internal static void ThrowArgumentNullException_codeWriter() => throw new ArgumentNullException("codeWriter");
		internal static void ThrowArgumentNullException_data() => throw new ArgumentNullException("data");
		internal static void ThrowArgumentNullException_writer() => throw new ArgumentNullException("writer");
		internal static void ThrowArgumentNullException_options() => throw new ArgumentNullException("options");
		internal static void ThrowArgumentNullException_value() => throw new ArgumentNullException("value");
		internal static void ThrowArgumentNullException_list() => throw new ArgumentNullException("list");
		internal static void ThrowArgumentNullException_collection() => throw new ArgumentNullException("collection");
		internal static void ThrowArgumentNullException_array() => throw new ArgumentNullException("array");
		internal static void ThrowArgumentNullException_sb() => throw new ArgumentNullException("sb");
		internal static void ThrowArgumentNullException_output() => throw new ArgumentNullException("output");

		internal static void ThrowArgumentOutOfRangeException_value() => throw new ArgumentOutOfRangeException("value");
		internal static void ThrowArgumentOutOfRangeException_index() => throw new ArgumentOutOfRangeException("index");
		internal static void ThrowArgumentOutOfRangeException_count() => throw new ArgumentOutOfRangeException("count");
		internal static void ThrowArgumentOutOfRangeException_length() => throw new ArgumentOutOfRangeException("length");
		internal static void ThrowArgumentOutOfRangeException_operand() => throw new ArgumentOutOfRangeException("operand");
		internal static void ThrowArgumentOutOfRangeException_instructionOperand() => throw new ArgumentOutOfRangeException("instructionOperand");
		internal static void ThrowArgumentOutOfRangeException_capacity() => throw new ArgumentOutOfRangeException("capacity");
		internal static void ThrowArgumentOutOfRangeException_memorySize() => throw new ArgumentOutOfRangeException("memorySize");
		internal static void ThrowArgumentOutOfRangeException_size() => throw new ArgumentOutOfRangeException("size");
		internal static void ThrowArgumentOutOfRangeException_elementSize() => throw new ArgumentOutOfRangeException("elementSize");
		internal static void ThrowArgumentOutOfRangeException_register() => throw new ArgumentOutOfRangeException("register");
		internal static void ThrowArgumentOutOfRangeException_code() => throw new ArgumentOutOfRangeException("code");
		internal static void ThrowArgumentOutOfRangeException_data() => throw new ArgumentOutOfRangeException("data");
	}
}
