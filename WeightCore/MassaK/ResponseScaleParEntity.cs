// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using System.Text;

namespace WeightCore.MassaK;

public class ResponseScaleParEntity
{
	#region Public and private fields and properties

	/// <summary>
	/// Максимальная нагрузка, Max.
	/// </summary>
	public string P_Max;
	/// <summary>
	/// Минимальная нагрузка, Min.
	/// </summary>
	public string P_Min;
	/// <summary>
	/// Поверочный интервал весов.
	/// </summary>
	public string P_e;
	/// <summary>
	/// Максимальная масса тары, T.
	/// </summary>
	public string P_T;
	/// <summary>
	/// Параметр фиксации веса.
	/// </summary>
	public string Fix;
	/// <summary>
	/// Код юстировки.
	/// </summary>
	public string Calcode;
	/// <summary>
	/// Версия ПО датчика взвешивания.
	/// </summary>
	public string PO_Ver;
	/// <summary>
	/// Контрольная сумма ПО датчика взвешивания.
	/// </summary>
	public string PO_Summ;

	#endregion

	#region Constructor and destructor

	public ResponseScaleParEntity(byte[] response)
	{
		ASCIIEncoding encoding = new();
		using MemoryStream memStream = new();
		byte pos = 6;

		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref P_Max);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref P_Min);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref P_e);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref P_T);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref Fix);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref Calcode);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref PO_Ver);
		pos = ResponseScaleParGetValue(response, encoding, pos, memStream, ref PO_Summ);
		//ErrorMessage = $"Код ответа CMD_ACK_SCALE_PAR: {Command};\n{P_Max};\n{P_Min};\n{P_e};\n{P_T};\n{Fix};\n{Calcode};\n{PO_Ver};\n{PO_Summ}";
	}

	private byte ResponseScaleParGetValue(byte[] response, ASCIIEncoding encoding, byte pos, MemoryStream memStream, ref string value)
	{
		while (response.Length > pos && response[pos] != 0x0D)
		{
			pos++;
			if (response.Length > pos)
				memStream.WriteByte(response[pos]);
		}
		value = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
		// Skip 0x0D and 0x0A.
		if (response.Length > pos && response[pos] == 0x0D)
			pos++;
		if (response.Length > pos && response[pos] == 0x0A)
			pos++;
		memStream.SetLength(0);
		return pos;
	}

	#endregion
}