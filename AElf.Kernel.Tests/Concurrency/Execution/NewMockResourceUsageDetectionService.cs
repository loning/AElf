﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using AElf.Kernel.Managers;
using AElf.Kernel.Concurrency;
using AElf.Types.CSharp;
using AElf.ABI.CSharp;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Xunit;

namespace AElf.Kernel.Tests.Concurrency.Scheduling
{
    public class NewMockResourceUsageDetectionService : IResourceUsageDetectionService
    {
        public IEnumerable<Hash> GetResources(ITransaction transaction)
        {
            //var hashes = Parameters.Parser.ParseFrom(transaction.Params).Params.Select(p => p.HashVal);
            List<Hash> hashes = new List<Hash>();
            using (MemoryStream mm = new MemoryStream(transaction.Params.ToByteArray()))
            using (CodedInputStream input = new CodedInputStream(mm))
            {
                uint tag;
                while ((tag = input.ReadTag()) != 0)
                {
                    switch (WireFormat.GetTagWireType(tag))
                    {
                        case WireFormat.WireType.Varint:
                            input.ReadUInt64();
                            break;
                        case WireFormat.WireType.LengthDelimited:
                            var bytes = input.ReadBytes();
                            if (bytes.Length == 34)
                            {
                                var h = new Hash();
                                h.MergeFrom(bytes);
                                hashes.Add(h);
                            }

                            break;
                    }
                }
            }

            hashes.Add(transaction.From);

            return hashes;
        }
    }
}