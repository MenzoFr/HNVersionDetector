using System.Collections.Generic;

namespace HelloNeighborVersionDetector
{
    public static class VersionDatabase
    {
        public static readonly List<(string Version, string ProcessName, uint Timestamp, uint ModuleSize)> Versions =
            new List<(string, string, uint, uint)>
            {
                ("HN1 Pre Alpha", "HelloNeighbour-Win64-Shipping.exe", 0x562FA119, 0x022C9000),
                ("HN1 Alpha 1", "HelloNeighborReborn-Win64-Shipping.exe", 0x56C8AECF, 0x02692000),
                ("HN1 Alpha 2", "HelloNeighborReborn.exe", 0x582DB167, 0x06147000),
                ("HN1 Alpha 3", "HelloNeighborReborn-Win64-Shipping.exe", 0x585BDAEE, 0x02B29000),
                ("HN1 Alpha 4", "HelloNeighborReborn-Win64-Shipping.exe", 0x590B0614, 0x0320F000),
                ("HN1 Beta 1", "HelloNeighbor-Win64-Shipping.exe", 0x593B1DDE, 0x0360B000),
                ("HN1 Beta 2", "HelloNeighbor-Win64-Shipping.exe", 0x593EBFFB, 0x03618000),
                ("HN1 Beta 3", "HelloNeighbor-Win64-Shipping.exe", 0x598E028B, 0x0362A000),
                ("HN1 Pre Release", "HelloNeighbor-Win64-Shipping.exe", 0x5A01B12F, 0x03D9E000),
                ("HN1 32-bittest", "HelloNeighbor-Win32-Shipping.exe", 0x5A2972EB, 0x02DDA000),
                ("HN1 1.0", "HelloNeighbor-Win64-Shipping.exe", 0x5A29D640, 0x040E2000),
                ("HN1 1.1", "HelloNeighbor-Win64-Shipping.exe", 0x5A2C22B4, 0x04061000),
                ("HN1 1.1.2", "HelloNeighbor-Win64-Shipping.exe", 0x5A2EDEEB, 0x04061000),
                ("HN1 1.1.3", "HelloNeighbor-Win64-Shipping.exe", 0x5A345CAE, 0x04067000),
                ("HN1 1.1.4", "HelloNeighbor-Win64-Shipping.exe", 0x5A38EECD, 0x03190000),
                ("HN1 1.1.5", "HelloNeighbor-Win64-Shipping.exe", 0x5A3CEAFE, 0x03191000),
                ("HN1 1.1.6", "HelloNeighbor-Win64-Shipping.exe", 0x5A41F179, 0x03192000),
                ("HN1 1.1.7", "HelloNeighbor-Win64-Shipping.exe", 0x5A456556, 0x03192000),
                ("HN1 1.1.8", "HelloNeighbor-Win64-Shipping.exe", 0x5A8419B6, 0x030A5000),
                ("HN1 1.1.9", "HelloNeighbor-Win64-Shipping.exe", 0x5A86FFC0, 0x030F8000),
                ("HN1 1.2 Beta", "HelloNeighbor-Win64-Shipping.exe", 0x5B2662DB, 0x032D9000),
                ("HN1 1.2", "HelloNeighbor-Win64-Shipping.exe", 0x5B2A5945, 0x032DA000),
                ("HN1 1.3", "HelloNeighbor-Win64-Shipping.exe", 0x5BFFF72A, 0x03543000),
                ("HN1 1.3.1", "HelloNeighbor-Win64-Shipping.exe", 0x5C1F8F20, 0x03543000),
                ("HN1 1.4", "HelloNeighbor-Win64-Shipping.exe", 0x5DB99258, 0x0351C000),
            };
    }
}
