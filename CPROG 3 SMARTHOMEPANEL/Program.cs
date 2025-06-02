using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SmartControlpanel;

namespace SmartControlpanel
{
    interface ISmartDevice
    {
        //Interfaces(ISP-Interface Segregation Principle) interface ISmartDevice
       
        string SerialNumber { get; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsOn { get; set; }
        void TurnOn();
        void TurnOff();
        void Configure();
    }
}
// Abstract Classes(SRP-Single Responsibility Principle)

abstract class SmartDevice : ISmartDevice
{
    public string SerialNumber { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsOn { get; set; }

    public SmartDevice(string serial, string name, string description)
    {
        SerialNumber = serial; Name = name;
        Description = description;
        IsOn = true;
    }
    public void TurnOn()
    {
        IsOn = true ;
        Console.WriteLine($"{Name} ({SerialNumber})is now ON.");
    }
    public void TurnOff()
    {
        IsOn &= false ;
        Console.WriteLine($"{Name} ({SerialNumber}) is now OFF.");
    }
    public abstract void Configure();
}
// Classes & Inheritance (LSP - Liskov Substitution Principle)

class LightBulb : SmartDevice
{
    public int Brightness { get; set; }
    public LightBulb(string serial, string name, string description, int brightness = 80) : base(serial, name, description)
    {
        Brightness = brightness;
    }
    public override void Configure()
    {
        Console.WriteLine("Enter new brightness (0-100):");
        if (int.TryParse(Console.ReadLine(), out int newBrightness) && newBrightness >= 0 && newBrightness <= 100)
        {
            Brightness = newBrightness;
            Console.WriteLine($"Brightness set to {Brightness}.");

        }
        else
        {
            Console.WriteLine("Invalid brightness value.");
        }
    }      
}    
class Fan : SmartDevice
{
    public int Speed {  get; set; }
    public Fan(string serial, string name, string description, int speed = 3) : base(serial, name, description)
    {
        Speed = speed;  
    }
    public override void Configure()
    {
        Console.WriteLine("Enter new speed (1-5):");
        if (int.TryParse(Console.ReadLine(),out int newSpeed) && newSpeed >= 1 && newSpeed <= 5)
        {
            Speed = newSpeed;
            Console.WriteLine($"Fan speed set to {Speed}.");
        }
        else
        {
            Console.WriteLine("Invalid speed value.");
        }
    }
}
// Other device classes

class Oven : SmartDevice
{
    public int Temperature {  get; set; }
    public Oven(string name, string serial, string description, int temperature = 180) : base(serial, name, description)
    {
        Temperature = temperature;
    }
    public override void Configure()
    {
        Console.WriteLine("Enter new oven temperature (50-250)C):");
        if (int.TryParse(Console.ReadLine(), out int newTemperature) && newTemperature >= 50 && newTemperature<=250)
        {
            Temperature = newTemperature;
            Console.WriteLine($"Oven temperature set to{Temperature}C.");
        }
        else
        {
            Console.WriteLine("Invalid temperature value.");
        }
    }
}
class DoorLock : SmartDevice
{
    public bool isLocked{ get; private set; }
    public DoorLock(string name, string serial, string description) : base(serial, name, description) 
    {
        isLocked = false;
    }
    public override void Configure()
    {
        Console.WriteLine("Lock or unlock the door? (lock/unlock):");
        string input = Console.ReadLine()?.ToLower();
        if (input == "lock")
        {
            isLocked = true;
            Console.WriteLine($"{Name} is now locked.");
        }
        else if (input == "unlock") ;
        {
            isLocked = false ;
            Console.WriteLine("Invalid choice.");
        }
    }
}
class Sprinkler : SmartDevice
{
    public int WaterFlow { get;set; }
    public Sprinkler(string serial, string name, string description, int waterFlow = 50) : base(serial, name, description)
    {
        WaterFlow = waterFlow;
    }
    public override void Configure()
    {
        Console.WriteLine("Enter new water flow rate (0-100):");
        if (int.TryParse(Console.ReadLine(), out int newFlow) && newFlow >= 0 && newFlow <= 10)
        {
            WaterFlow = newFlow;
            Console.WriteLine($"WaterFlow set to{WaterFlow}.");
        }
        else
        {
            Console.WriteLine("Invalid WaterFlow rate.");
        }
    }
}
class Tv : SmartDevice
{
    public int Volume { get; set; }
    public Tv(string serial, string name, string description, int volume = 20) : base(serial, name, description)
    {
        Volume = volume;    

    }
    public override void Configure()
    {
        Console.WriteLine("Enter new volume(0-100):");
        if(int.TryParse(Console.RedLine(), out int newVolume) && newVolume >= 0 && Volume <= 100)
        {
            Volume = newVolume;
            Console.WriteLine($"TV volume set to {Volume}.");
        }
        else
        {
            Console.WriteLine("Invalid volume value.");
        }
    }
}
class Speaker : SmartDevice
{
    public int Volume { get; set; }
    public Speaker( string serial, string name, string description, int volume = 50) : base(serial, name, description)
    {
        Volume = volume;
    }
    public override void Configure()
    {
        Console.WriteLine("Enter new speaker volume(0-100):");
        if(int.TryParse(Console.RedLine(),out int newVolume) && newVolume =>=0 newVolume<=100)
        {
            Volume = newVolume
            Console.WriteLine($"Speaker volume set to {Volume}.");
        }
        else
        {
            Console.WriteLine("Invalid volume value.");
        }
        
    }
}
class Room
{
    public string Name { get; set; }
    public List<ISmartDevice>Devices { get; set; } = new List<ISmartDevice> ();
    public  Room(string name)
    {
        Name = name;    
    }
    public void AddDevice(ISmartDevice device)
    {
        Devices.Add(device);
        Console.WriteLine($"Added{device.Name}({device.SerialNumber})to{Name}.");
    }
    public void ControlAll(bool turnOn)
    {
        foreach (var device in Devices)
        {
            if (turnOn) device.TurnOn();
            else device.TurnOff();
        }
    }
}
class Thermostat : SmartDevice
{
    public int Temperature {  get; set; }
    public Thermostat(string serial, string name, string description, int temperature = 22) : base(serial, name, description)
    {
        Temperature = temperature;
    }
    public override void Configure()
    {
        Console.WriteLine("Enter new thermostat temperature(16-30C):");
        if (int.TryParse(Console.ReadLine(), out int newTemp) && newTemp>=16 && newTemp<=30)
        {
            Temperature = newTemp;
            Console.WriteLine($"Thermostat temperature set to{Temperature}C.");
        }
        else
        {
            Console.WriteLine("Invalid temperature value.");
        }
    }
}
// Static Classes (DIP - Depencency Inversion Principle)

static class DeviceFactory
{
    private static int serialCounter = 1000;
    public static string GenerateSerial()=>$"DEV-{serialCounter++}";
    public static ISmartDevice CreateDevice(string type, string name, string description)
    {
        switch (type.ToLower())
        {
            case "lightbulb": return new LightBulb(GenerateSerial(), name, description);
            case "fan": return new Fan(GenerateSerial(), name, description);
            case "oven": return new Oven(GenerateSerial(), name, description);
            case "doorlock": return new DoorLock(GenerateSerial(), name, description);
            case "sprinkle": return new Sprinkler(GenerateSerial(), name, description);
            case "tv": return new Tv(GenerateSerial(), name, description);
            case "speaker": return new Speaker(GenerateSerial(), name, description);
            case "thermostat": return new Thermostat(GenerateSerial(), name, description);
            default: return null;
        }
    }
}
// Generics (OCP- Open/Closed Principle)

class SmartHome<T>where T : ISmartDevice
{
    public List<T>Devices { get; set; }= new List<T>();
    public void AddDevice(T device)=> Devices.Remove(device);
    public void RemoveDevice(T device) => Devices.Remove(device);
}
class Program
{
    static void Main(string[] args)
    {
        var home = new List<Room>
        {
            new Room("Living Room"),
            new Room("Bedroom"),
            new Room("Kitchen")
        };
    }
}
home[0].AddDevice(DeviceFactory.CreateDevice("lightbulb", "Ceiling Light", "Main light in living room"));
home[0].AddDevice(DeviceFactory.CreateDevice("fan", "Ceiling Fan", "Fan in living room"));
home[0].AddDevice(DeviceFactory.CreateDevice("thermostast", "Bedroom Thermoststat", "Temperature controller"));
home[0].AddDevice(DeviceFactory.CreateDevice("tv", "Living Room Tv", "Smart TV with voice control"));
home[1].AddDevice(DeviceFactory.CreateDevice("speaker", "Bedroom Speaker", "Smart speaker with Bluetooth"));
home[2].AddDevice(DeviceFactory.CreateDevice("oven", "Kitchen Oven", "Electric oven with temperature control"));
home[2].AddDevice(DeviceFactory.CreateDevice("doolock", "Main Doorlock", "Smart lock with passcode"));
home[2].AddDevice(DeviceFactory.CreateDevice("sprinkler", "Garden Sprinkler", "Automated water sprinkler"));
while (true)
{
    Console.WriteLine("\nSmart Home Control Panel");
    Console.WriteLine("1. View Devices");
    Console.WriteLine("2. Control Device");
    Console.WriteLine("3. Configure Device");
    Console.WriteLine("4. Exit)";
    Console.WriteLine("Choose an option");
    switch (Console.ReadLine())
    {
        case "1":
            foreach (var room in home)
            {
                Console.WriteLie($"\n{room.Name}:");
                foreach (var device in room.Devices)
                {
                    Console.WriteLine($"{device.Name}({device.SerialNumber}) -{(device.IsOn ? "ON" : "OFF")}");
                    if (device is LightBulb lb) Console.WriteLine($",Brightness:{lb.Brightness}");
                    else if (device is Fan f) Console.WriteLine($",Speed:{f.Speed}");
                    else if (device is Oven o) Console.WriteLine($",Temperature:{o.Temperature}");
                    else if (device is DoorLock dl) Console.WriteLine($",Locked:{dl.isLocked}");
                    else if (device is Sprinkler s) Console.WriteLine($",Water Flow:{s.WaterFlow}");
                    else if (device is Tv tv) Console.WriteLine($",Volume:{tv.Volume}");
                    else if (device is Speaker sp) Console.WriteLine($",Volume:{sp.Volume}");
                    else if (device is Thermostat t) Console.WriteLine($",Temperature:{t.Tempearture}C");
                    else Console.WriteLine();
                }
            }
            break;
        case "2":Console.Write("Enter room name:");
            string roomName = Console.ReadLine();
            Room selectedRoom = SmartHome.FirstOrDefault(r=>r.Name.Equals(roomName, StringComparison.OrdinalignoreCase));
            if (selectedRoom != null)
            {
                Console.Write("Turn devices ON or OFF?(on/off)");
                bool turnOn = Console.ReadLine().ToLower() == "on"; selectedRoom.ControlAll(turnOn);
            }
            else Console.Write.Line("Room not found.");
            break;
        case "3": Console.Write("Enter device name:");
            string deviceName=Console.ReadLine();
            foreach (var room in home)
            {
                var device= room.Devices.FirstOrDefault(d=>d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
                if (device != null) { device.Configure();break; }
            }
            break ;
        case "4":return;
            default:Console.WriteLine("Invalid choice.Try again.");break;
        

    }
}