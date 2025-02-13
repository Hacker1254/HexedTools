using Iced.Intel;
using System;
using System.Collections.Generic;

namespace Il2CppInterop.Internal.XrefScans;

public static class XrefScannerLowLevel
{
    public static IEnumerable<IntPtr> JumpTargets(IntPtr codeStart) => JumpTargetsImpl(XrefScanner.DecoderForAddress(codeStart));

    private static IEnumerable<IntPtr> JumpTargetsImpl(Decoder myDecoder)
    {
        var firstFlowControl = true;

        while (true)
        {
            myDecoder.Decode(out var instruction);
            if (myDecoder.LastError == DecoderError.NoMoreBytes) yield break;

            // 0xcc - padding after most functions
            if (instruction.Mnemonic == Mnemonic.Int3)
                yield break;

            if (instruction.FlowControl == FlowControl.Return)
                yield break;

            if (instruction.FlowControl == FlowControl.UnconditionalBranch ||
                instruction.FlowControl == FlowControl.Call)
            {
                // We hope and pray that the compiler didn't use short jumps for any function calls
                if (!instruction.IsJmpShort)
                {
                    yield return (IntPtr)ExtractTargetAddress(in instruction);
                    if (firstFlowControl && instruction.FlowControl == FlowControl.UnconditionalBranch) yield break;
                }
            }

            if (instruction.FlowControl != FlowControl.Next)
            {
                firstFlowControl = false;
            }
        }
    }

    public static IEnumerable<IntPtr> CallAndIndirectTargets(IntPtr pointer)
    {
        return CallAndIndirectTargetsImpl(XrefScanner.DecoderForAddress(pointer, 1024 * 1024));
    }

    private static IEnumerable<IntPtr> CallAndIndirectTargetsImpl(Decoder decoder)
    {
        while (true)
        {
            decoder.Decode(out var instruction);
            if (decoder.LastError == DecoderError.NoMoreBytes) yield break;

            if (instruction.FlowControl == FlowControl.Return)
                yield break;

            if (instruction.Mnemonic == Mnemonic.Int || instruction.Mnemonic == Mnemonic.Int1)
                yield break;

            if (instruction.Mnemonic == Mnemonic.Call || instruction.Mnemonic == Mnemonic.Jmp)
            {
                var targetAddress = XrefScanner.ExtractTargetAddress(instruction);
                if (targetAddress != 0)
                    yield return (IntPtr)targetAddress;
                continue;
            }

            if (instruction.Mnemonic == Mnemonic.Lea)
                if (instruction.MemoryBase == Register.RIP)
                {
                    var targetAddress = instruction.IPRelativeMemoryAddress;
                    if (targetAddress != 0)
                        yield return (IntPtr)targetAddress;
                }
        }
    }

    private static ulong ExtractTargetAddress(in Instruction instruction)
    {
        switch (instruction.Op0Kind)
        {
            case OpKind.NearBranch16:
                return instruction.NearBranch16;
            case OpKind.NearBranch32:
                return instruction.NearBranch32;
            case OpKind.NearBranch64:
                return instruction.NearBranch64;
            case OpKind.FarBranch16:
                return instruction.FarBranch16;
            case OpKind.FarBranch32:
                return instruction.FarBranch32;
            default:
                //Console.WriteLine(Enum.GetName(typeof(OpKind), instruction.Op0Kind)); // its OpKind.Register
                return 0;
        }
    }
}
