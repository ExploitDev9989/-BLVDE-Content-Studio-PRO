#nullable disable
using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Linq;

namespace BLVDEContentStudio
{
    public class NetworkInfoService
    {
        public string GetLocalIP(bool maskForPrivacy = false)
        {
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                var ip = host.AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                
                if (ip == null) return "Not Connected";
                
                string ipAddress = ip.ToString();
                return maskForPrivacy ? MaskIP(ipAddress) : ipAddress;
            }
            catch
            {
                return "Error";
            }
        }

        public string GetPublicIP(bool maskForPrivacy = false)
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    string ip = client.DownloadString("https://api.ipify.org").Trim();
                    return maskForPrivacy ? MaskIP(ip) : ip;
                }
            }
            catch
            {
                return "Error";
            }
        }

        public string GetMACAddress(bool maskForPrivacy = false)
        {
            try
            {
                var mac = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(mac)) return "Not Found";
                
                // Format MAC address
                string formatted = string.Join(":", Enumerable.Range(0, 6).Select(i => mac.Substring(i * 2, 2)));
                return maskForPrivacy ? MaskMAC(formatted) : formatted;
            }
            catch
            {
                return "Error";
            }
        }

        public string GetNetworkSpeed()
        {
            try
            {
                var nic = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

                if (nic == null) return "N/A";

                long speed = nic.Speed / 1_000_000; // Convert to Mbps
                return $"{speed} Mbps";
            }
            catch
            {
                return "N/A";
            }
        }

        public string GetConnectionType()
        {
            try
            {
                var nic = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

                if (nic == null) return "Unknown";

                return nic.NetworkInterfaceType switch
                {
                    NetworkInterfaceType.Wireless80211 => "Wi-Fi",
                    NetworkInterfaceType.Ethernet => "Ethernet",
                    NetworkInterfaceType.Ppp => "PPP",
                    _ => nic.NetworkInterfaceType.ToString()
                };
            }
            catch
            {
                return "Unknown";
            }
        }

        public string GetBytesReceived()
        {
            try
            {
                var nic = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

                if (nic == null) return "N/A";

                long bytes = nic.GetIPv4Statistics().BytesReceived;
                return FormatBytes(bytes);
            }
            catch
            {
                return "N/A";
            }
        }

        public string GetBytesSent()
        {
            try
            {
                var nic = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

                if (nic == null) return "N/A";

                long bytes = nic.GetIPv4Statistics().BytesSent;
                return FormatBytes(bytes);
            }
            catch
            {
                return "N/A";
            }
        }

        private string MaskIP(string ip)
        {
            var parts = ip.Split('.');
            if (parts.Length == 4)
            {
                return $"{parts[0]}.{parts[1]}.•••.•••";
            }
            return "•••.•••.•••.•••";
        }

        private string MaskMAC(string mac)
        {
            var parts = mac.Split(':');
            if (parts.Length == 6)
            {
                return $"{parts[0]}:{parts[1]}:••:••:••:••";
            }
            return "••:••:••:••:••:••";
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
