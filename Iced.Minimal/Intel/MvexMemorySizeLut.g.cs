// SPDX-License-Identifier: MIT
// Copyright (C) 2018-present iced project and contributors

// ⚠️This file was generated by GENERATOR!🦹‍♂️

#nullable enable

#if MVEX
namespace Iced.Intel {
	static class MvexMemorySizeLut {
		public static readonly byte[] Data = new byte[] {
			// MvexTupleTypeLutKind.Int32
			(byte)MemorySize.Packed512_Int32,// 0
			(byte)MemorySize.Int32,// 1
			(byte)MemorySize.Packed128_Int32,// 2
			(byte)MemorySize.Packed256_Float16,// 3
			(byte)MemorySize.Packed128_UInt8,// 4
			(byte)MemorySize.Packed128_Int8,// 5
			(byte)MemorySize.Packed256_UInt16,// 6
			(byte)MemorySize.Packed256_Int16,// 7
			// MvexTupleTypeLutKind.Int32_Half
			(byte)MemorySize.Packed256_Int32,// 0
			(byte)MemorySize.Int32,// 1
			(byte)MemorySize.Packed128_Int32,// 2
			(byte)MemorySize.Packed128_Float16,// 3
			(byte)MemorySize.Packed64_UInt8,// 4
			(byte)MemorySize.Packed64_Int8,// 5
			(byte)MemorySize.Packed128_UInt16,// 6
			(byte)MemorySize.Packed128_Int16,// 7
			// MvexTupleTypeLutKind.Int32_4to16
			(byte)MemorySize.Packed128_Int32,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Packed64_Float16,// 3
			(byte)MemorySize.Packed32_UInt8,// 4
			(byte)MemorySize.Packed32_Int8,// 5
			(byte)MemorySize.Packed64_UInt16,// 6
			(byte)MemorySize.Packed64_Int16,// 7
			// MvexTupleTypeLutKind.Int32_1to16_or_elem
			(byte)MemorySize.Int32,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Float16,// 3
			(byte)MemorySize.UInt8,// 4
			(byte)MemorySize.Int8,// 5
			(byte)MemorySize.UInt16,// 6
			(byte)MemorySize.Int16,// 7
			// MvexTupleTypeLutKind.Int64
			(byte)MemorySize.Packed512_Int64,// 0
			(byte)MemorySize.Int64,// 1
			(byte)MemorySize.Packed256_Int64,// 2
			(byte)MemorySize.Packed128_Float16,// 3
			(byte)MemorySize.Packed64_UInt8,// 4
			(byte)MemorySize.Packed64_Int8,// 5
			(byte)MemorySize.Packed128_UInt16,// 6
			(byte)MemorySize.Packed128_Int16,// 7
			// MvexTupleTypeLutKind.Int64_4to8
			(byte)MemorySize.Packed256_Int64,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Packed64_Float16,// 3
			(byte)MemorySize.Packed32_UInt8,// 4
			(byte)MemorySize.Packed32_Int8,// 5
			(byte)MemorySize.Packed64_UInt16,// 6
			(byte)MemorySize.Packed64_Int16,// 7
			// MvexTupleTypeLutKind.Int64_1to8_or_elem
			(byte)MemorySize.Int64,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Float16,// 3
			(byte)MemorySize.UInt8,// 4
			(byte)MemorySize.Int8,// 5
			(byte)MemorySize.UInt16,// 6
			(byte)MemorySize.Int16,// 7
			// MvexTupleTypeLutKind.Float32
			(byte)MemorySize.Packed512_Float32,// 0
			(byte)MemorySize.Float32,// 1
			(byte)MemorySize.Packed128_Float32,// 2
			(byte)MemorySize.Packed256_Float16,// 3
			(byte)MemorySize.Packed128_UInt8,// 4
			(byte)MemorySize.Packed128_Int8,// 5
			(byte)MemorySize.Packed256_UInt16,// 6
			(byte)MemorySize.Packed256_Int16,// 7
			// MvexTupleTypeLutKind.Float32_Half
			(byte)MemorySize.Packed256_Float32,// 0
			(byte)MemorySize.Float32,// 1
			(byte)MemorySize.Packed128_Float32,// 2
			(byte)MemorySize.Packed128_Float16,// 3
			(byte)MemorySize.Packed64_UInt8,// 4
			(byte)MemorySize.Packed64_Int8,// 5
			(byte)MemorySize.Packed128_UInt16,// 6
			(byte)MemorySize.Packed128_Int16,// 7
			// MvexTupleTypeLutKind.Float32_4to16
			(byte)MemorySize.Packed128_Float32,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Packed64_Float16,// 3
			(byte)MemorySize.Packed32_UInt8,// 4
			(byte)MemorySize.Packed32_Int8,// 5
			(byte)MemorySize.Packed64_UInt16,// 6
			(byte)MemorySize.Packed64_Int16,// 7
			// MvexTupleTypeLutKind.Float32_1to16_or_elem
			(byte)MemorySize.Float32,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Float16,// 3
			(byte)MemorySize.UInt8,// 4
			(byte)MemorySize.Int8,// 5
			(byte)MemorySize.UInt16,// 6
			(byte)MemorySize.Int16,// 7
			// MvexTupleTypeLutKind.Float64
			(byte)MemorySize.Packed512_Float64,// 0
			(byte)MemorySize.Float64,// 1
			(byte)MemorySize.Packed256_Float64,// 2
			(byte)MemorySize.Packed128_Float16,// 3
			(byte)MemorySize.Packed64_UInt8,// 4
			(byte)MemorySize.Packed64_Int8,// 5
			(byte)MemorySize.Packed128_UInt16,// 6
			(byte)MemorySize.Packed128_Int16,// 7
			// MvexTupleTypeLutKind.Float64_4to8
			(byte)MemorySize.Packed256_Float64,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Packed64_Float16,// 3
			(byte)MemorySize.Packed32_UInt8,// 4
			(byte)MemorySize.Packed32_Int8,// 5
			(byte)MemorySize.Packed64_UInt16,// 6
			(byte)MemorySize.Packed64_Int16,// 7
			// MvexTupleTypeLutKind.Float64_1to8_or_elem
			(byte)MemorySize.Float64,// 0
			(byte)MemorySize.Unknown,// 1
			(byte)MemorySize.Unknown,// 2
			(byte)MemorySize.Float16,// 3
			(byte)MemorySize.UInt8,// 4
			(byte)MemorySize.Int8,// 5
			(byte)MemorySize.UInt16,// 6
			(byte)MemorySize.Int16,// 7
		};
	}
}
#endif