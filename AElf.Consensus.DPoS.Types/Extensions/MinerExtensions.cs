using System;
using System.Linq;
using AElf.Common;
using Google.Protobuf.WellKnownTypes;

// ReSharper disable once CheckNamespace
namespace AElf.Consensus.DPoS
{
    // ReSharper disable InconsistentNaming
    public static class MinersExtensions
    {
        public static bool IsEmpty(this Miners miners)
        {
            return !miners.PublicKeys.Any();
        }

        public static Hash GetMinersHash(this Miners miners)
        {
            return Hash.FromMessage(miners.PublicKeys.OrderBy(p => p).ToMiners());
        }

        /// <summary>
        /// Get local time
        /// </summary>
        /// <param name="offset">minutes</param>
        /// <returns></returns>
        private static Timestamp GetTimestampOfUtcNow(int offset = 0)
        {
            return Timestamp.FromDateTime(DateTime.UtcNow.AddMilliseconds(offset));
        }
    }
}