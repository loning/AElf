﻿using AElf.Common;
namespace AElf.Kernel
{
    /// <summary>
    /// a running chain context
    /// </summary>
    public interface IChainContext
    {
        int ChainId { get; set; }
        ulong BlockHeight { get; set; }
        Hash BlockHash { get; set; }
    }
}