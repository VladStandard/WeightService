// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public class LocalePing
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocalePing _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocalePing Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string StatusUnknown => Lang == Lang.English ? "Unknown" : "Нет данных";
    public string StatusSuccess => Lang == Lang.English ? "Success access" : "Успешный доступ";
    public string StatusDestinationNetworkUnreachable => Lang == Lang.English ? "Destination network unreachable" : "Сеть назначения недоступна";
    public string StatusDestinationHostUnreachable => Lang == Lang.English ? "Destination host unreachable" : "Хост назначения недоступен";
    public string StatusDestinationProtocolUnreachable => Lang == Lang.English ? "Destination protocol unreachable" : "Протокол назначения недоступен";
    public string StatusDestinationPortUnreachable => Lang == Lang.English ? "Destination port unreachable" : "Порт назначения недоступен";
    public string StatusNoResources => Lang == Lang.English ? "No resources" : "Нет ресурсов";
    public string StatusBadOption => Lang == Lang.English ? "Bad option" : "Плохая опция";
    public string StatusHardwareError => Lang == Lang.English ? "Hardware error" : "Аппаратная ошибка";
    public string StatusPacketTooBig => Lang == Lang.English ? "Packet too big" : "Слишком большой пакет";
    public string StatusTimedOut => Lang == Lang.English ? "Timed out" : "Тайм-аут";
    public string StatusBadRoute => Lang == Lang.English ? "Bad route" : "Плохой маршрут";
    public string StatusTtlExpired => Lang == Lang.English ? "TTL expired" : "Срок действия истек";
    public string StatusTtlReassemblyTimeExceeded => Lang == Lang.English ? "TTL reassembly time exceeded" : "Превышено время повторной сборки TTL";
    public string StatusParameterProblem => Lang == Lang.English ? "Unknown" : "статус";
    public string StatusSourceQuench => Lang == Lang.English ? "Source quench" : "Устранение источника";
    public string StatusBadDestination => Lang == Lang.English ? "Bad destination" : "Плохое направление";
    public string StatusDestinationUnreachable => Lang == Lang.English ? "Destination unreachable" : "Место назначения недостижимо";
    public string StatusTimeExceeded => Lang == Lang.English ? "Time exceeded" : "Превышено время";
    public string StatusBadHeader => Lang == Lang.English ? "Bad header" : "Плохой заголовок";
    public string StatusUnrecognizedNextHeader => Lang == Lang.English ? "Unrecognized next header" : "Нераспознанный следующий заголовок";
    public string StatusIcmpError => Lang == Lang.English ? "ICMP error" : "Ошибка ICMP";
    public string StatusDestinationScopeMismatch => Lang == Lang.English ? "Destination scope mismatch" : "Несоответствие области назначения";
    
    #endregion
}