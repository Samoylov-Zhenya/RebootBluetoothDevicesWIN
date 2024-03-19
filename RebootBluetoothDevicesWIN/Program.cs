using System.Management;

class Program
{
    static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //  Create a WMI request to get the list of Bluetooth devices
            string queryString = "SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%Bluetooth%'";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryString))
            {
                using (ManagementObjectCollection results = searcher.Get())
                {
                    if (results.Count != 1)
                        return;

                    Console.WriteLine("Bluetooth devices:");

                    foreach (ManagementObject mo in results)
                    {
                        Console.WriteLine($"{mo["Name"]} - {mo["Description"]}");
                        //if (mo["Name"] != "Intel(R) Wireless Bluetooth(R)")
                        //{
                        //    DisableDevice(mo);
                        //    EnableDevice(mo);
                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There's been a mistake: {ex.Message}");
        }
        Console.ReadLine();
    }

    private static void EnableDevice(ManagementObject mo)
    {
        var outParams = mo.InvokeMethod("Enable", null, null);
        if (outParams != null)
        {
            // Check the result of Enable method execution
            uint resultCode = (uint)outParams["ReturnValue"];
            if (resultCode == 0)
                Console.WriteLine("The device has been successfully powered on.");
            else
                Console.WriteLine($"Failed to disconnect the device. Error code: {resultCode}");
        }
    }

    private static void DisableDevice(ManagementObject mo)
    {
        var outParams = mo.InvokeMethod("Disable", null, null);
        if (outParams != null)
        {
            // Check the result of Disable method execution
            uint resultCode = (uint)outParams["ReturnValue"];
            if (resultCode == 0)
                Console.WriteLine("The device has been successfully powered foo.");
            else
                Console.WriteLine($"Failed to disconnect the device. Error code: {resultCode}");
        }
    }
}
