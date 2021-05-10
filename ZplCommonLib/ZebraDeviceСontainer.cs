using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightServices.Common.Zpl
{
    public class ZebraDeviceСontainer
    {

        private static readonly Lazy<ZebraDeviceСontainer> _instance = new Lazy<ZebraDeviceСontainer>(() => new ZebraDeviceСontainer());
        public static ZebraDeviceСontainer Instance => _instance.Value;

        private Dictionary<Guid, ZebraDeviceEntity> zebraDevices = null;

        private ZebraDeviceСontainer()
        {
            zebraDevices = new Dictionary<Guid, ZebraDeviceEntity>();
        }


        public Guid AddDevice (string _ip, int _port)
        {
            Guid id = Guid.NewGuid();

            WeightServices.Common.Zpl.DeviceSocketTcp zplDeviceSocket =
                new WeightServices.Common.Zpl.DeviceSocketTcp(_ip, _port);

            zebraDevices.Add(id, new ZebraDeviceEntity(zplDeviceSocket, id));
            return id;
        }


        public void CheckDeviceStatusOn()
        {
            foreach (KeyValuePair<Guid, ZebraDeviceEntity> device in zebraDevices)
            {
                device.Value.CheckDeviceStatusOn();
            }
        }
        public void CheckDeviceStatusOff()
        {
            foreach (KeyValuePair<Guid, ZebraDeviceEntity> device in zebraDevices)
            {
                device.Value.CheckDeviceStatusOff();
            }

        }

        public void Send(Guid id, string template, string content)
        {
            ZebraDeviceEntity curZebraDevice = null;
            if (zebraDevices.TryGetValue(id, out curZebraDevice) ) {
                curZebraDevice.SendAsync(template, content);
            };
        }

        public void Send(Guid id, string content)
        {
            ZebraDeviceEntity curZebraDevice = null;
            if (zebraDevices.TryGetValue(id, out curZebraDevice))
            {
                curZebraDevice.SendAsync(content);
            };
        }

        public void SendFirstOrDefault(string template, string content)
        {
            KeyValuePair<Guid, ZebraDeviceEntity> curZebraDevicePair = zebraDevices.FirstOrDefault();
            if (curZebraDevicePair.Value != null) {
                curZebraDevicePair.Value.SendAsync(template, content);
            }
        }

    }
}
